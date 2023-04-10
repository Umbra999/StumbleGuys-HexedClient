using ExitGames.Client.Photon;
using Newtonsoft.Json;
using Photon.Realtime;
using System;
using Formatting = Newtonsoft.Json.Formatting;

namespace Hexed.Wrappers
{
    internal class Logger
    {
        public enum LogsType
        {
            Clean,
            Info,
            Protection,
            API,
        }

        public static void Log(object obj, LogsType Type = LogsType.Clean)
        {
            string log = obj.ToString().Replace("\a", "a").Replace("\u001B[", "u001B[");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            if (Type != LogsType.Clean) Console.Write($"] [{Type}] {log}\n");
            else Console.Write($"] {log}\n");
        }

        public static void LogError(object obj)
        {
            string log = obj.ToString().Replace("\a", "");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"] ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{log}\n");
        }

        public static void LogWarning(object obj)
        {
            string log = obj.ToString().Replace("\a", "");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{log}\n");
        }

        public static void LogDebug(object obj)
        {
            string log = obj.ToString().Replace("\a", "");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{log}\n");
        }

        public static void LogOpRaise(byte Code, Il2CppSystem.Object Object, RaiseEventOptions RaiseOptions, SendOptions SendOptions)
        {
            try
            {
                object CustomData = Object == null ? "NULL" : IL2CPPSerializer.IL2CPPToManaged.Serialize(Object);
                string TargetActors = RaiseOptions.TargetActors != null ? string.Join(", ", RaiseOptions.TargetActors) : "NULL";

                string IfBytes = "";
                if (CustomData.GetType() == typeof(byte[]))
                {
                    IfBytes = "  |  ";
                    byte[] data = (byte[])CustomData;
                    if (Code == 8)
                    {
                        int index = 0;
                        while (index < data.Length)
                        {
                            int ViewID = BitConverter.ToInt32(data, index);
                            index += 4;
                            byte UpdateFreq = data[index++];
                            byte VoiceQuality = data[index++];
                            IfBytes += $"\nViewID:{ViewID} Freq:{UpdateFreq} Quality:{VoiceQuality}";
                        }
                    }
                    else
                    {
                        IfBytes += string.Join(", ", data);
                        IfBytes += $" [L: {data.Length}]";
                    }
                }

                Log(string.Concat(new object[]
                {
                    Environment.NewLine,
                    $"======= OPRAISEEVENT {Code} =======", Environment.NewLine,
                    $"TYPE: {CustomData}", Environment.NewLine,
                    $"DATA SERIALIZED: {JsonConvert.SerializeObject(CustomData, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}{IfBytes}", Environment.NewLine,
                    $"TARGET ACTORS: {TargetActors}", Environment.NewLine,
                    $"EVENT CACHING: {RaiseOptions.CachingOption}", Environment.NewLine,
                    $"INTEREST GROUP: {RaiseOptions.InterestGroup}", Environment.NewLine,
                    $"SEQUENCE CHANNEL: {RaiseOptions.SequenceChannel}", Environment.NewLine,
                    $"RECEIVER GROUP: {RaiseOptions.Receivers}", Environment.NewLine,
                    $"CHANNEL: {SendOptions.Channel}",Environment.NewLine,
                    $"DELIVERY MODE: {SendOptions.DeliveryMode}",Environment.NewLine,
                    $"ENCRYPT: {SendOptions.Encrypt}",Environment.NewLine,
                    "======= END =======",
                }), LogsType.Clean);
            }
            catch (Exception e)
            {
                LogError($"Failed to Log Event with code {Code} with Exception: {e}");
            }
        }

        public static void LogEventData(EventData EventData)
        {
            try
            {
                object CustomData = EventData.CustomData == null ? "NULL" : IL2CPPSerializer.IL2CPPToManaged.Serialize(EventData.CustomData);

                Player PhotonPlayer = null;
                string SenderNAME = "NULL";

                if (EventData.Sender > 0) PhotonPlayer = PhotonHelper.GetPhotonPlayer(EventData.Sender);
                if (PhotonPlayer != null) SenderNAME = PhotonPlayer.GetDisplayName();

                string IfBytes = "";
                if (CustomData.GetType() == typeof(byte[]))
                {
                    IfBytes = "  |  ";
                    byte[] arr = (byte[])CustomData;
                    IfBytes += string.Join(", ", arr);
                    IfBytes += $" [L: {arr.Length}]";
                }

                Log(string.Concat(new object[]
                {
                    Environment.NewLine,
                    $"======= ONEVENT {EventData.Code} =======", Environment.NewLine,
                    $"ACTOR NUMBER: {EventData.Sender}", Environment.NewLine,
                    $"SENDER NAME: {SenderNAME}", Environment.NewLine,
                    $"TYPE: {CustomData}", Environment.NewLine,
                    $"DATA SERIALIZED: {JsonConvert.SerializeObject(CustomData, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}{IfBytes}", Environment.NewLine,
                    $"PARAMETER: {JsonConvert.SerializeObject(IL2CPPSerializer.IL2CPPToManaged.Serialize(EventData.Parameters))}", Environment.NewLine,
                    "======= END =======",
                }), LogsType.Clean);
            }
            catch (Exception e)
            {
                LogError($"Failed to Log Event with code {EventData.Code} with Exception: {e}");
            }
        }

        public static void LogOperation(byte Code, Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> Params, SendOptions Options)
        {
            try
            {
                if (Code == 253) return;

                object Data = IL2CPPSerializer.IL2CPPToManaged.Serialize(Params);
                Log($"[Operation] {Code}", LogsType.Clean);
                Log($"[Dictionary] \n{JsonConvert.SerializeObject(Data, Formatting.Indented)}", LogsType.Clean);
                Log($"[SendOptions] Channel: {Options.Channel} Mode: {Options.DeliveryMode} Encrypt: {Options.Encrypt}", LogsType.Clean);
            }
            catch
            {
                LogError($"Failed to Log Operation with code {Code}");
            }
        }
    }
}
