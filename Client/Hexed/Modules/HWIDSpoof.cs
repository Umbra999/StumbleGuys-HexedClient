using Hexed.Core;
using Hexed.HexedServer;
using Hexed.Wrappers;
using MelonLoader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hexed.Modules
{
    internal class HWIDSpoof
    {
        public static string FakeHWID = GenerateHWID();

        public static void Init()
        {
            Patching.SpoofHWID();
            Logger.Log("Hardware Spoofed", Logger.LogsType.Protection);
        }

        public static string GenerateHash(string Text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Text);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            string ComputeHash = string.Join("", from it in hash select it.ToString("x2"));
            return ComputeHash;
        }

        public static string GenerateHWID()
        {
            string OriginalHWID = UnityEngine.SystemInfo.deviceUniqueIdentifier;
            byte[] bytes = new byte[OriginalHWID.Length / 2];
            Encryption.Random.NextBytes(bytes);
            return string.Join("", bytes.Select(it => it.ToString("x2")));
        }
    }
}
