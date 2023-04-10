using Hexed.Core;
using Hexed.Wrappers;
using System;
using UnityEngine;

namespace Hexed.Modules
{
    internal class GUIHandler : MonoBehaviour
    {
        public GUIHandler(IntPtr ptr) : base(ptr) { }
        public void OnGUI()
        {
            if (!InternalSettings.GUIEnabled) return;

            GUI.Box(new Rect(10, 10, 220, 400), "");
            GUI.Box(new Rect(10, 10, 220, 400), "");
            GUI.Box(new Rect(10, 10, 220, 400), "H E X E D");

            InternalSettings.NoProperties = GUI.Toggle(new Rect(30, 40, 150, 30), InternalSettings.NoProperties, "No Props");

            InternalSettings.AltUser = GUI.Toggle(new Rect(30, 65, 150, 30), InternalSettings.AltUser, "Alt Account");

            InternalSettings.PhotonNameSpoof = GUI.Toggle(new Rect(30, 90, 150, 30), InternalSettings.PhotonNameSpoof, "Name Spoof");

            InternalSettings.PhotonNameSpoofName = GUI.TextField(new Rect(30, 115, 150, 20), InternalSettings.PhotonNameSpoofName);

            if (GUI.Button(new Rect(30, 140, 150, 20), "Force Master")) PhotonHelper.GetCurrentRoom().SetMasterClient(PhotonHelper.GetCurrentUser());

            InternalSettings.DebugLogOpRaise = GUI.Toggle(new Rect(30, 165, 150, 30), InternalSettings.DebugLogOpRaise, "Raise Log");

            InternalSettings.DebugLogSendOp = GUI.Toggle(new Rect(30, 190, 150, 30), InternalSettings.DebugLogSendOp, "Operation Log");

            InternalSettings.DebugLogOnEvent = GUI.Toggle(new Rect(30, 215, 150, 30), InternalSettings.DebugLogOnEvent, "Event Log");

            InternalSettings.AntiBotLobby = GUI.Toggle(new Rect(30, 240, 150, 30), InternalSettings.AntiBotLobby, "Anti Bot");
        }
    }
}
