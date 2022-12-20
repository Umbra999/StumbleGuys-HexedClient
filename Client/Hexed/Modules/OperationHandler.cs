using ExitGames.Client.Photon;
using Hexed.Core;
using UnhollowerBaseLib;

namespace Hexed.Modules
{
    internal class OperationHandler
    {
        public static void ChangeName226(Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> param)
        {
            if (!param.ContainsKey(249)) return;

            Hashtable hashtable = param[249].TryCast<Hashtable>();

            if (hashtable == null) return;

            Il2CppSystem.Object ILByte = new Il2CppSystem.Byte() { m_value = byte.MaxValue }.BoxIl2CppObject();
            hashtable[ILByte] = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(InternalSettings.PhotonNameSpoofName));
        }

        public static void ChangeName252(Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> param)
        {
            if (!param.ContainsKey(251)) return;

            Hashtable hashtable = param[251].TryCast<Hashtable>();

            if (hashtable == null) return;

            Il2CppSystem.Object ILByte = new Il2CppSystem.Byte() { m_value = byte.MaxValue }.BoxIl2CppObject();
            hashtable[ILByte] = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(InternalSettings.PhotonNameSpoofName));
        }

        public static void DisableProperties226And252(Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> param)
        {
            if (!param.ContainsKey(250)) return;

            var Key = new Il2CppSystem.Byte() { m_value = 250}.BoxIl2CppObject();
            var Value = new Il2CppSystem.Boolean() { m_value = false }.BoxIl2CppObject();
            param.System_Collections_IDictionary_set_Item(Key, Value);
        }

        public static void ForceMaster226(Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> param)
        {
            // Hashtable 251 with byte or int 248 with actor id
        }
    }
}
