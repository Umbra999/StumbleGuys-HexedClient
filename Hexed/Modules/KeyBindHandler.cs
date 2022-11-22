using Hexed.Core;
using UnityEngine;

namespace Hexed.Modules
{
    internal class KeyBindHandler
    {
        public static void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) InternalSettings.GUIEnabled = !InternalSettings.GUIEnabled;
        }
    }
}
