using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace HexedInstaller
{
    class MainLoader
    {
        public static void Init()
        {
            Console.WriteLine("");
            Console.WriteLine("Select your Stumble Guys.exe");

            Task.Run(() => PathInstalling());
            Thread.Sleep(-1);
        }

        public static async Task PathInstalling()
        {
            string path = OpenFileDialog.OpenfileDialog();
            if (path.Contains("Stumble Guys.exe"))
            {
                path = path.Replace("Stumble Guys.exe", "");

                await HexedServer.ServerHandler.Init(path);

                Console.WriteLine("");
                Console.WriteLine("Installing Hexed...");

                if (!Directory.Exists(path + "\\Mods")) Directory.CreateDirectory(path + "\\Mods");
                else if (File.Exists(path + "\\Mods\\HexedLoader.dll")) File.Delete(path + "\\Mods\\HexedLoader.dll");

                if (File.Exists(path + "\\MelonLoader")) File.Delete(path + "\\MelonLoader");

                if (File.Exists(path + "\\TempMelon.zip")) File.Delete(path + "\\TempMelon.zip");

                string LoaderEncrypted = await HexedServer.ServerHandler.GetLoader();
                if (LoaderEncrypted != null) File.WriteAllBytes(path + "\\Mods\\HexedLoader.dll", Convert.FromBase64String(LoaderEncrypted));

                string MelonEncrypted = await HexedServer.ServerHandler.GetMelon();
                if (MelonEncrypted != null) File.WriteAllBytes(path + "\\TempMelon.zip", Convert.FromBase64String(MelonEncrypted));

                try
                {
                    ZipFile.ExtractToDirectory(path + "\\TempMelon.zip", path);
                }
                catch { }

                File.Delete(path + "\\TempMelon.zip");

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Hexed Installed");
                await Task.Delay(5000);
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Failed to find Stumble Guys.exe File");
                await PathInstalling();
            }
        }
    }
}