using ExitGames.Client.Photon;
using Hexed.Wrappers;
using Photon.Realtime;

namespace Hexed.Modules
{
    internal class EventHandler
    {
        public static bool PropertiesChangedEvent(EventData Data)
        {
            if (Data.Parameters != null)
            {
                if (Data.Parameters.ContainsKey(251))
                {
                    if (Data.Parameters[251].ToString() == "(System.String)KICK=(System.Boolean)True" && Data.Sender != PhotonHelper.GetCurrentUser().ActorID())
                    {
                        Player player = PhotonHelper.GetPhotonPlayer(Data.Sender);
                        Logger.Log($"Prevented Kick from {(player == null ? "SERVER" : player.GetDisplayName())} [{Data.Sender}]", Logger.LogsType.Protection);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
