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
    }
}
