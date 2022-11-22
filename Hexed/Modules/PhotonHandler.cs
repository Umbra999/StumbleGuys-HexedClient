using ExitGames.Client.Photon;
using Hexed.Wrappers;
using Photon.Realtime;

namespace Hexed.Modules
{
    internal class PhotonHandler
    {
        public static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            OpRaiseEvent(code, IL2CPPSerializer.ManagedToIL2CPP.Serialize(customObject), RaiseEventOptions, sendOptions);
        }

        public static void OpRaiseEvent(byte code, Il2CppSystem.Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            MultiplayerRoomManager.photonClient.OpRaiseEvent(code, customObject, RaiseEventOptions, sendOptions);
        }
    }
}
