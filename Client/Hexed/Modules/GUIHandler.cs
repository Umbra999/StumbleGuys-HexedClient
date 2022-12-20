using Hexed.Core;
using Hexed.Wrappers;
using UnityEngine;

namespace Hexed.Modules
{
    internal class GUIHandler
    {
        public static void Update()
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

            //InternalSettings.FakePingValue = (short)GUI.HorizontalSlider(new Rect(30, 145, 150, 30), InternalSettings.FakePingValue, 0, short.MaxValue);

            //InternalSettings.FakeFrameValue = GUI.HorizontalSlider(new Rect(30, 160, 150, 30), InternalSettings.FakeFrameValue, 0, 1000);
        }
    }
}
