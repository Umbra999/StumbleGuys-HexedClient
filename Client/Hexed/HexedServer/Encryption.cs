using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hexed.HexedServer
{
    internal class Encryption
    {
        public static string FromBase64(string Data)
        {
            var base64EncodedBytes = Convert.FromBase64String(Data);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToBase64(string Data)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(Data);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static Random Random = new Random(Environment.TickCount);
        public static string RandomString(int length)
        {
            char[] array = "abcdefghlijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToArray();
            string text = string.Empty;
            for (int i = 0; i < length; i++)
            {
                text += array[Random.Next(array.Length)].ToString();
            }
            return text;
        }

        public static int RandomNumber(int Lowest, int Highest)
        {
            return Random.Next(Lowest, Highest);
        }

        public static byte RandomByte()
        {
            return (byte)Random.Next(0, 255);
        }

        public static string EncryptAuthKey(string Key, string Timestamp, string ValidationType, string HWID)
        {
            string EncryptedKey = ValidationType;
            EncryptedKey += ":";
            EncryptedKey += ToBase64(Timestamp);
            EncryptedKey += ":";
            EncryptedKey += ToBase64(HWID);
            EncryptedKey += ":";
            EncryptedKey += ToBase64(Key);
            EncryptedKey += "98NXvV3d";
            EncryptedKey += ToBase64("10792dC");

            return ToBase64(EncryptedKey);
        }


        // HWID
        public static string GetHWID()
        {
            string HWID = "";
            HWID += Environment.MachineName;
            HWID += Environment.UserName;
            HWID += Environment.ProcessorCount;
            HWID += GetProcessorID();
            HWID += GetProcessorName();
            HWID += GetBIOSVendor();
            HWID += GetBIOSName();
            HWID += GetDriveName();
            HWID += GetDriveID();
            HWID = GenerateHash(ToBase64(HWID));
            return "H" + HWID + "EX";
        }

        public static string GenerateHash(string Text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Text);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            string ComputeHash = string.Join("", from it in hash select it.ToString("x2"));
            return ComputeHash;
        }

        private static string GetProcessorID()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
            if (key != null)
            {
                return key.GetValue("Identifier").ToString();
            }

            return "";
        }

        private static string GetProcessorName()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
            if (key != null)
            {
                return key.GetValue("ProcessorNameString").ToString();
            }

            return "";
        }

        private static string GetBIOSVendor()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS");
            if (key != null)
            {
                return key.GetValue("BaseBoardManufacturer").ToString();
            }

            return "";
        }

        private static string GetBIOSName()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS");
            if (key != null)
            {
                return key.GetValue("BaseBoardProduct").ToString();
            }

            return "";
        }

        private static string GetDriveName()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 0\Logical Unit Id 0");
            if (key != null)
            {
                return key.GetValue("Identifier").ToString();
            }

            return "";
        }

        private static string GetDriveID()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 0\Logical Unit Id 0");
            if (key != null)
            {
                return key.GetValue("SerialNumber").ToString();
            }

            return "";
        }
    }
}
