using MelonLoader;

namespace Hexed
{
    public class LocalLoader : MelonMod
    {
        public override void OnApplicationStart()
        {
            Loader.Load.OnApplicationStart();
        }

        public override void OnUpdate()
        {
            Loader.Load.OnUpdate();
        }

        public override void OnGUI()
        {
            Loader.Load.OnGUI();
        }

        public override void OnApplicationQuit()
        {
            Loader.Load.OnApplicationQuit();
        }
    }
}
