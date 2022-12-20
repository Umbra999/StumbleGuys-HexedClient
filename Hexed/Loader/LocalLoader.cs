using MelonLoader;

namespace Hexed
{
    public class LocalLoader : MelonMod
    {
        public override void OnApplicationStart()
        {
            Loader.EventHandler.OnApplicationStart();
        }

        public override void OnUpdate()
        {
            Loader.EventHandler.OnUpdate();
        }

        public override void OnGUI()
        {
            Loader.EventHandler.OnGUI();
        }

        public override void OnApplicationQuit()
        {
            Loader.EventHandler.OnApplicationQuit();
        }
    }
}
