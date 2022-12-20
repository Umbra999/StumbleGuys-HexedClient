using MelonLoader;

namespace HexedLoader
{
    internal class Load : MelonMod
    {
        public override void OnApplicationStart()
        {
            Main.Init();
        }

        public override void OnLevelWasLoaded(int level)
        {
            ModController.OnLevelWasLoaded(level);
        }

        public override void OnLevelWasInitialized(int level)
        {
            ModController.OnLevelWasInitialized(level);
        }

        public override void OnUpdate()
        {
            ModController.OnUpdate();
        }

        public override void OnFixedUpdate()
        {
            ModController.OnFixedUpdate();
        }

        public override void OnLateUpdate()
        {
            ModController.OnLateUpdate();
        }

        public override void OnGUI()
        {
            ModController.OnGUI();
        }

        public override void OnApplicationQuit()
        {
            ModController.OnApplicationQuit();
        }

        public override void OnModSettingsApplied()
        {
            ModController.OnModSettingsApplied();
        }
    }
}