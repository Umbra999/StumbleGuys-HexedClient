using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace Hexed.Wrappers
{
    internal class IL2CPPSerializer
    {
        public class BinarySerializer
        {
            private static byte[] IL2CPPObjectToByteArray(Il2CppSystem.Object obj)
            {
                if (obj == null) return null;
                var bf = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                var ms = new Il2CppSystem.IO.MemoryStream();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }

            private static byte[] ManagedObjectToByteArray(object obj)
            {
                if (obj == null) return null;
                var bf = new BinaryFormatter();
                var ms = new MemoryStream();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }

            private static object ManagedObjectFromArray(byte[] data)
            {
                if (data == null) return default;
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(data);
                object obj = bf.Deserialize(ms);
                return obj;
            }

            private static Il2CppSystem.Object IL2CPPObjectFromArray(byte[] data)
            {
                if (data == null) return default;
                var bf = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                var ms = new Il2CppSystem.IO.MemoryStream(data);
                object obj = bf.Deserialize(ms);
                return (Il2CppSystem.Object)obj;
            }

            public static object Serialize(Il2CppSystem.Object obj)
            {
                return ManagedObjectFromArray(IL2CPPObjectToByteArray(obj));
            }

            public static Il2CppSystem.Object Serialize(object obj)
            {
                return IL2CPPObjectFromArray(ManagedObjectToByteArray(obj));
            }
        }

        public class ManagedToIL2CPP
        {
            public static Il2CppSystem.Object Serialize(object Data)
            {
                return BinarySerializer.Serialize(Data);
            }
        }

        public class IL2CPPToManaged
        {
            public static object Serialize(Il2CppSystem.Object Data)
            {
                return BinarySerializer.Serialize(Data);
            }

            private static Dictionary<T, object> ReadDictionary<T>(Il2CppSystem.Object Data)
            {
                var Table = new Dictionary<T, object>();
                var ILTable = Data.Cast<Il2CppSystem.Collections.Generic.Dictionary<T, Il2CppSystem.Object>>();
                foreach (var Pair in ILTable)
                {
                    if (Pair.Key == null) continue;
                    T Key = Pair.Key;
                    object Value = Serialize(Pair.Value);
                    Table.Add(Key, Value);
                }
                return Table;
            }

            private static Dictionary<T, object> ReadDictionary<T, V>(Il2CppSystem.Object Data)
            {
                var Table = new Dictionary<T, object>();
                var ILTable = Data.Cast<Il2CppSystem.Collections.Generic.Dictionary<T, V>>();
                foreach (var Pair in ILTable)
                {
                    if (Pair.Key == null) continue;
                    T Key = Pair.Key;
                    V Value = Pair.Value;
                    Table.Add(Key, Value);
                }
                return Table;
            }

            private static System.Collections.Hashtable ReadHashtable(Il2CppSystem.Object Data)
            {
                var Table = new System.Collections.Hashtable();
                var ILTalbe = Data.Cast<Il2CppSystem.Collections.Hashtable>();

                foreach (var pair in ILTalbe.buckets)
                {
                    if (pair.key == null) continue;
                    var Value = pair.val == null ? null : Serialize(pair.val);
                    Table.Add(Serialize(pair.key), Value);
                }
                return Table;
            }

            private static Dictionary<object, object> ReadPhotonDictionary(Il2CppSystem.Object Data)
            {
                var Table = new Dictionary<object, object>();

                var ILTable = Data.Cast<Il2CppSystem.Collections.Generic.Dictionary<Il2CppSystem.Object, Il2CppSystem.Object>>();
                foreach (var Pair in ILTable)
                {
                    if (Pair.Key == null) continue;

                    object Key = Serialize(Pair.Key);
                    object Value = Serialize(Pair.Value);
                    Table.Add(Key, Value);
                }
                return Table;
            }
        }
    }
}
