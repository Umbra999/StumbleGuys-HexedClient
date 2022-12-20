using HexedLoader.Server;
using HexedLoader.Wrapper;
using MelonLoader;
using System;
using System.Linq;
using System.Reflection;

namespace HexedLoader
{
    internal class Main
    {
        public static async void Init()
        {
            await ServerHandler.Init();

            try
            {
                string RawBytes = await ServerHandler.GetClient();
                if (RawBytes == null)
                {
                    Logger.LogError("Failed to get Client");
                    return;
                }

                byte[] Client = Convert.FromBase64String(RawBytes);

                Assembly MappedAssembly = Assembly.Load(Client);
                if (MappedAssembly == null)
                {
                    Logger.LogError("Failed to Map Assembly");
                    return;
                }

                Type Target = MappedAssembly.GetTypes().Where(x => x.Name == "RemoteLoader")?.FirstOrDefault();
                if (Target == null)
                {
                    Logger.LogError("Failed to find Entry");
                    return;
                }

                ModController.RegisterMod(Target);
                ModController.OnApplicationStart();

                Logger.LogSuccess("Injected Module");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Injection failed: {ex}");
            }
        }
    }
}
