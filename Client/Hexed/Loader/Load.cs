using Hexed.Core;
using Hexed.HexedServer;
using Hexed.Modules;
using Hexed.Wrappers;
using MelonLoader;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace Hexed.Loader
{
    internal class Load
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCP(uint wCodePageID);

        public static void OnApplicationStart()
        {
            Console.Title = Encryption.RandomString(30);
            SetConsoleOutputCP(65001);
            SetConsoleCP(65001);
            Console.OutputEncoding = Encoding.GetEncoding(65001);
            Console.InputEncoding = Encoding.GetEncoding(65001);

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

            HWIDSpoof.Init();
            Patching.ApplyPatches();
            ConfigHandler.Init();
            InjectClasses();
            LoadEarlyModules();

            WaitForUI().Start();
        }

        public static void OnUpdate()
        {
            
        }

        public static void OnGUI()
        {
            
        }

        public static void OnApplicationQuit()
        {
            Process.GetCurrentProcess().Kill();
        }

        private static IEnumerator WaitForUI()
        {
            while (GameObject.Find("MusicManager") == null) yield return new WaitForEndOfFrame();

            LoadClasses();
            LoadModules();
        }

        private static void InjectClasses()
        {
            ClassInjector.RegisterTypeInIl2Cpp<MiscHandler>();
            ClassInjector.RegisterTypeInIl2Cpp<KeyBindHandler>();
            ClassInjector.RegisterTypeInIl2Cpp<GUIHandler>();
            ClassInjector.RegisterTypeInIl2Cpp<MovementHandler>();
        }

        private static void LoadClasses()
        {
            GameObject Hexed = new GameObject()
            {
                name = "Hexed",
            };
            UnityEngine.Object.DontDestroyOnLoad(Hexed);

            Hexed.AddComponent<MiscHandler>();
            Hexed.AddComponent<KeyBindHandler>();
            Hexed.AddComponent<GUIHandler>();
            Hexed.AddComponent<MovementHandler>();
        }

        private static void LoadModules()
        {
            MusicHandler.OverrideMusic().Start();
        }

        private static void LoadEarlyModules()
        {

        }
    }
}
