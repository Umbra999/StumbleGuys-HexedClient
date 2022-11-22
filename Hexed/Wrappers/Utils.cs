using MelonLoader;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Hexed.Wrappers
{
    internal static class Utils
    {
        public static IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        public static object Start(this IEnumerator e)
        {
            return MelonCoroutines.Start(e);
        }

        public static System.Random Random = new System.Random(Environment.TickCount);

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

        public static string RandomNumberString(int length)
        {
            string text = string.Empty;
            for (int i = 0; i < length; i++)
            {
                text += Random.Next(0, 9).ToString("X8");
            }
            return text;
        }
    }
}
