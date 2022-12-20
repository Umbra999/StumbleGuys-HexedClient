namespace Hexed.Loader
{
    public class RemoteLoader
    {
        private static void OnApplicationStart()
        {
            EventHandler.OnApplicationStart();
        }

        private static void OnUpdate()
        {
            EventHandler.OnUpdate();
        }

        private static void OnGUI()
        {
            EventHandler.OnGUI();
        }

        private static void OnApplicationQuit()
        {
            EventHandler.OnApplicationQuit();
        }
    }
}
