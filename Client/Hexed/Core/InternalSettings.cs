namespace Hexed.Core
{
    internal class InternalSettings
    {
        public static FrameAndPingMode FrameSpoof = FrameAndPingMode.None;
        public static FrameAndPingMode PingSpoof = FrameAndPingMode.None;

        public enum FrameAndPingMode
        {
            None = 0,
            Custom = 1,
            Realistic = 2
        }

        public static LatencyMode LatencySpoof = LatencyMode.None;
        public enum LatencyMode
        {
            None = 0,
            Custom = 1,
            Low = 2,
            High = 3
        }

        public static short FakePingValue = 0;
        public static float FakeFrameValue = 1000;
        public static int FakeLatencyValue = 69;
        public static bool PhotonNameSpoof = false;
        public static string PhotonNameSpoofName = "Hexed";
        public static bool AltUser = true;
        public static bool NoProperties = false;
        public static bool GUIEnabled = true;
        public static bool DebugLogOpRaise = false;
        public static bool DebugLogOnEvent = false;
        public static bool DebugLogSendOp = false;
    }
}
