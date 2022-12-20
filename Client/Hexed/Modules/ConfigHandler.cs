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

            LoadConfig();
        }

        public static void LoadConfig()
        {
            Ini = new IniFile("Hexed\\Config.ini");

            InternalSettings.NoProperties = Ini.GetBool("Toggles", "NoProperties");
            InternalSettings.AltUser = Ini.GetBool("Toggles", "AltUser");
            InternalSettings.LatencySpoof = (InternalSettings.LatencyMode)Ini.GetInt("Toggles", "LatencySpoofMode");
            InternalSettings.PingSpoof = (InternalSettings.FrameAndPingMode)Ini.GetInt("Toggles", "PingSpoofMode");
            InternalSettings.FrameSpoof = (InternalSettings.FrameAndPingMode)Ini.GetInt("Toggles", "FrameSpoofMode");
            InternalSettings.PhotonNameSpoof = Ini.GetBool("Toggles", "PhotonNameSpoof");
            InternalSettings.PhotonNameSpoofName = Ini.GetString("Toggles", "PhotonNameSpoofName");
        }

        public static void SaveConfig()
        {
            Ini.SetBool("Toggles", "NoProperties", InternalSettings.NoProperties);
            Ini.SetBool("Toggles", "AltUser", InternalSettings.AltUser);
            Ini.SetInt("Toggles", "LatencySpoofMode", (int)InternalSettings.LatencySpoof);
            Ini.SetInt("Toggles", "PingSpoofMode", (int)InternalSettings.PingSpoof);
            Ini.SetInt("Toggles", "FrameSpoofMode", (int)InternalSettings.FrameSpoof);
            Ini.SetBool("Toggles", "PhotonNameSpoof", InternalSettings.PhotonNameSpoof);
            Ini.SetString("Toggles", "PhotonNameSpoofName", InternalSettings.PhotonNameSpoofName);
        }
    }
}
