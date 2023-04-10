namespace Hexed.Loader
{
    public class RemoteLoader
    {
        private static void OnApplicationStart()
        {
            Load.OnApplicationStart();
        }

        private static void OnUpdate()
        {
            Load.OnUpdate();
        }

        private static void OnGUI()
        {
            Load.OnGUI();
        }

        private static void OnApplicationQuit()
        {
            Load.OnApplicationQuit();
        }
    }
}
