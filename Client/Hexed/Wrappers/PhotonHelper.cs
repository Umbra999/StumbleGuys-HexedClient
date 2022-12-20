using Photon.Realtime;

namespace Hexed.Wrappers
{
    internal static class PhotonHelper
    {
        public static int ActorID(this Player player)
        {
            return player.ActorNumber;
        }

        public static ExitGames.Client.Photon.Hashtable GetHashtable(this Player player)
        {
            return player.CustomProperties;
        }

        public static string GetDisplayName(this Player player)
        {
            return player.nickName;
        }

        public static string GetUserID(this Player player)
        {
            return player.UserId;
        }

        public static bool IsMaster(this Player player)
        {
            return player.IsMasterClient;
        }

        public static Player GetCurrentUser()
        {
            return MultiplayerRoomManager.photonClient.LocalPlayer;
        }

        public static Room GetCurrentRoom()
        {
            return MultiplayerRoomManager.photonClient.CurrentRoom;
        }

        public static Player GetPhotonPlayer(int Actor)
        {
            return GetCurrentRoom().GetPlayer(Actor);
        }
    }
}
