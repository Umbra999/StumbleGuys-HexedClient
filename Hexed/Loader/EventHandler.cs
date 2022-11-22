using Hexed.Core;
using Hexed.Modules;
using Hexed.Wrappers;
using MelonLoader;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Hexed.Loader
{
    internal class EventHandler
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCP(uint wCodePageID);

        public static void OnApplicationStart()
        {
            Console.Title = $"Hexed by Umbra | CONSOLE | {Utils.RandomString(20)}";
            SetConsoleOutputCP(65001);
            SetConsoleCP(65001);
            Console.OutputEncoding = Encoding.GetEncoding(65001);
            Console.InputEncoding = Encoding.GetEncoding(65001);

            HWIDSpoof.Init();
            Patching.ApplyPatches();

            ConfigHandler.Init();

            OnMenuInitialized().Start();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"
          
 ▄▀▀▄ ▄▄   ▄▀▀█▄▄▄▄  ▄▀▀▄  ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀█▄▄  
█  █   ▄▀ ▐  ▄▀   ▐ █    █   █ ▐  ▄▀   ▐ █ ▄▀   █ 
▐  █▄▄▄█    █▄▄▄▄▄  ▐     ▀▄▀    █▄▄▄▄▄  ▐ █    █ 
   █   █    █    ▌       ▄▀ █    █    ▌    █    █ 
  ▄▀  ▄▀   ▄▀▄▄▄▄       █  ▄▀   ▄▀▄▄▄▄    ▄▀▄▄▄▄▀ 
 █   █     █    ▐     ▄▀  ▄▀    █    ▐   █     ▐  
 ▐   ▐     ▐         █    ▐     ▐        ▐        
      _
     |u|
   __|m|__
   \+-b-+/       Beautiful girls, dead feelings...
    ~|r|~        one day the sun is gonna explode and all this was for nothing.
     |a|
      \|
");
        }

        public static void OnUpdate()
        {
            KeyBindHandler.Update();
        }

        public static void OnGUI()
        {
            GUIHandler.Update();
        }

        public static void OnApplicationQuit()
        {
            MelonPreferences.Save();
            ConfigHandler.SaveConfig();

            Process.GetCurrentProcess().Kill();
        }

        private static IEnumerator OnMenuInitialized()
        {
            while (GameObject.Find("MusicManager") == null) yield return new WaitForEndOfFrame();

            MusicHandler.OverrideMusic().Start();
        }
    }
}
