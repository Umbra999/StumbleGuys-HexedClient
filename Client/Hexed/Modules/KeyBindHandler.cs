using Hexed.Core;
using System;
using UnityEngine;

namespace Hexed.Modules
{
    internal class KeyBindHandler : MonoBehaviour
    {
        public KeyBindHandler(IntPtr ptr) : base(ptr) { }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) InternalSettings.GUIEnabled = !InternalSettings.GUIEnabled;
        }
    }
}
