using Hexed.Core;
using MelonLoader;
using System.IO;

namespace Hexed.Modules
{
    internal class ConfigHandler
    {
        public static IniFile Ini;
        public static void Init()
        {
            if (!Directory.Exists("Hexed")) Directory.CreateDirectory("Hexed");

            if (!File.Exists("Hexed\\Config.ini")) File.Create("Hexed\\Config.ini");

            SetInternalValuesFromConfig();
        }

        public static void SetInternalValuesFromConfig()
        {
            Ini = new IniFile("Hexed\\Config.ini");

            InternalSettings.NoProperties = Ini.GetBool("Toggles", "NoProperties");
            InternalSettings.AltUser = Ini.GetBool("Toggles", "AltUser");
            InternalSettings.LatencySpoof = (InternalSettings.LatencyMode)Ini.GetInt("Toggles", "LatencySpoofMode");
            InternalSettings.PingSpoof = (InternalSettings.FrameAndPingMode)Ini.GetInt("Toggles", "PingSpoofMode");
            InternalSettings.FrameSpoof = (InternalSettings.FrameAndPingMode)Ini.GetInt("Toggles", "FrameSpoofMode");
            InternalSettings.PhotonNameSpoof = Ini.GetBool("Toggles", "PhotonNameSpoof");
            InternalSettings.PhotonNameSpoofName = Ini.GetString("Toggles", "PhotonNameSpoofName");
            InternalSettings.AntiBotLobby = Ini.GetBool("Toggles", "AntiBotLobby");
        }
    }
}
