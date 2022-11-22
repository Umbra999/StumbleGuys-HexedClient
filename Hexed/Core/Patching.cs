using ExitGames.Client.Photon;
using HarmonyLib;
using Hexed.Modules;
using Hexed.Wrappers;
using MelonLoader;
using Photon.Realtime;
using System;
using System.Reflection;
using TJR;
using UnhollowerBaseLib;
using UnityEngine;

namespace Hexed.Core
{
    internal static class Patching
    {
        private static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("Hexed");

        private static HarmonyMethod GetPatch(string name)
        {
            return new HarmonyMethod(typeof(Patching).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
        }

        private static MethodInfo GetFromMethod(this Type Type, string name)
        {
            return Type.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        private static void CreatePatch(MethodBase TargetMethod, HarmonyMethod Before = null, HarmonyMethod After = null)
        {
            try
            {
                Instance.Patch(TargetMethod, Before, After);
            }
            catch (Exception e)
            {
                Wrappers.Logger.LogError($"Failed to Patch {TargetMethod.Name} \n{e}");
            }
        }

        public static unsafe void SpoofHWID()
        {
            IntPtr intPtr1 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceUniqueIdentifier");
            MelonUtils.NativeHookAttach((IntPtr)(&intPtr1), AccessTools.Method(typeof(Patching), nameof(FakeHWID)).MethodHandle.GetFunctionPointer());
            IntPtr intPtr2 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceModel");
            MelonUtils.NativeHookAttach((IntPtr)(&intPtr2), AccessTools.Method(typeof(Patching), nameof(FakeModel)).MethodHandle.GetFunctionPointer());
            IntPtr intPtr3 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceName");
            MelonUtils.NativeHookAttach((IntPtr)(&intPtr3), AccessTools.Method(typeof(Patching), nameof(FakeName)).MethodHandle.GetFunctionPointer());
            IntPtr intPtr4 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceName");
            MelonUtils.NativeHookAttach((IntPtr)(&intPtr4), AccessTools.Method(typeof(Patching), nameof(FakeGPU)).MethodHandle.GetFunctionPointer());
            IntPtr intPtr5 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetProcessorType");
            MelonUtils.NativeHookAttach((IntPtr)(&intPtr5), AccessTools.Method(typeof(Patching), nameof(FakeCPU)).MethodHandle.GetFunctionPointer());
        }

        public static IntPtr FakeModel()
        {
            return new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(Utils.RandomString(Utils.Random.Next(13, 16)))).Pointer;
        }

        public static IntPtr FakeName()
        {
            return new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp("DESKTOP-" + Utils.RandomString(Utils.Random.Next(7, 9)))).Pointer;
        }

        public static IntPtr FakeGPU()
        {
            return new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(Utils.RandomString(Utils.Random.Next(14, 17)))).Pointer;
        }

        public static IntPtr FakeCPU()
        {
            return new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(Utils.RandomString(Utils.Random.Next(15, 19)))).Pointer;
        }

        public static IntPtr FakeHWID()
        {
            return new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(HWIDSpoof.FakeHWID)).Pointer;
        }

        public static unsafe void ApplyPatches()
        {
            CreatePatch(AccessTools.Property(typeof(PhotonPeer), "RoundTripTime").GetMethod, null, GetPatch(nameof(RoundTripTimePrefix)));
            CreatePatch(AccessTools.Property(typeof(PhotonPeer), "RoundTripTimeVariance").GetMethod, null, GetPatch(nameof(RoundTripTimeVariancePrefix)));
            CreatePatch(AccessTools.Property(typeof(Time), "smoothDeltaTime").GetMethod, null, GetPatch(nameof(smoothDeltaTimePrefix)));
            CreatePatch(AccessTools.Property(typeof(SteamHandler), "UserIDString").GetMethod, null, GetPatch(nameof(SteamIdPrefix)));
            CreatePatch(AccessTools.Property(typeof(SteamHandler), "UserNickname").GetMethod, null, GetPatch(nameof(SteamIdPrefix)));
            CreatePatch(typeof(LoadBalancingPeer).GetFromMethod(nameof(LoadBalancingPeer.OpRaiseEvent)), GetPatch(nameof(OpRaiseEventPrefix)));
            CreatePatch(typeof(QuantumLoadBalancingClient).GetFromMethod(nameof(QuantumLoadBalancingClient.OnEvent)), GetPatch(nameof(OnEventPrefix)));
            CreatePatch(typeof(PhotonPeer).GetFromMethod(nameof(PhotonPeer.SendOperation)), GetPatch(nameof(SendOperationPrefix)));
            CreatePatch(typeof(FirebaseAnalyticsSender).GetFromMethod(nameof(FirebaseAnalyticsSender.Initialize)), GetPatch(nameof(ReturnFalsePrefix)));
            CreatePatch(typeof(InRoomCallbacksContainer).GetFromMethod(nameof(InRoomCallbacksContainer.OnMasterClientSwitched)), GetPatch(nameof(OnMasterClientSwitchedPrefix)));
            CreatePatch(typeof(InRoomCallbacksContainer).GetFromMethod(nameof(InRoomCallbacksContainer.OnPlayerEnteredRoom)), GetPatch(nameof(OnPlayerEnteredRoomPrefix)));
            CreatePatch(typeof(InRoomCallbacksContainer).GetFromMethod(nameof(InRoomCallbacksContainer.OnPlayerLeftRoom)), GetPatch(nameof(OnPlayerLeftRoomPrefix)));
            CreatePatch(typeof(InRoomCallbacksContainer).GetFromMethod(nameof(InRoomCallbacksContainer.OnPlayerPropertiesUpdate)), GetPatch(nameof(OnPlayerPropertiesUpdatePrefix)));
            CreatePatch(typeof(InRoomCallbacksContainer).GetFromMethod(nameof(InRoomCallbacksContainer.OnRoomPropertiesUpdate)), GetPatch(nameof(OnRoomPropertiesUpdatePrefix)));
            CreatePatch(typeof(Backend).GetFromMethod(nameof(Backend.CheatingDetected)), GetPatch(nameof(ReturnFalsePrefix)));
        }

        private static void AllowBoolPostfix(ref bool __result)
        {
            __result = true;
        }

        private static void DisallowBoolPostfix(ref bool __result)
        {
            __result = false;
        }

        private static bool ReturnFalsePrefix()
        {
            return false;
        }

        private static void SteamIdPrefix(ref string __result)
        {
            if (InternalSettings.AltUser) __result = HWIDSpoof.FakeHWID;
        }

        private static void RoundTripTimePrefix(ref int __result)
        {
            switch (InternalSettings.PingSpoof)
            {
                case InternalSettings.FrameAndPingMode.Custom:
                    __result = InternalSettings.FakePingValue;
                    break;

                case InternalSettings.FrameAndPingMode.Realistic:
                    int Random = Utils.Random.Next(6, 17);
                    __result = Convert.ToInt16(Random);
                    break;
            }
        }

        private static void RoundTripTimeVariancePrefix(ref int __result)
        {
            switch (InternalSettings.LatencySpoof)
            {
                case InternalSettings.LatencyMode.Custom:
                    __result = InternalSettings.FakeLatencyValue;
                    break;

                case InternalSettings.LatencyMode.Low:
                    __result = byte.MinValue;
                    break;

                case InternalSettings.LatencyMode.High:
                    __result = byte.MaxValue;
                    break;
            }
        }

        private static void smoothDeltaTimePrefix(ref float __result)
        {
            switch (InternalSettings.FrameSpoof)
            {
                case InternalSettings.FrameAndPingMode.Custom:
                    __result = InternalSettings.FakeFrameValue;
                    break;

                case InternalSettings.FrameAndPingMode.Realistic:
                    int Random = Utils.Random.Next(100, 170);
                    __result = (float)1 / Random;
                    break;
            }
        }

        private static void OnMasterClientSwitchedPrefix(Player __0)
        {
            Wrappers.Logger.Log($"{__0.GetDisplayName()} is now Master", Wrappers.Logger.LogsType.Info);
        }

        private static void OnPlayerEnteredRoomPrefix(Player __0)
        {
            Wrappers.Logger.Log($"[ + ] {__0.GetDisplayName()}", Wrappers.Logger.LogsType.Clean);
        }

        private static void OnPlayerLeftRoomPrefix(Player __0)
        {
            Wrappers.Logger.Log($"[ - ] {__0.GetDisplayName()}", Wrappers.Logger.LogsType.Clean);
        }

        private static void OnPlayerPropertiesUpdatePrefix(Player __0, Hashtable __1)
        {

        }

        private static void OnRoomPropertiesUpdatePrefix(Hashtable __0)
        {

        }

        private static bool SendOperationPrefix(byte __0, Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> __1, SendOptions __2)
        {
            if (InternalSettings.DebugLogSendOp) Wrappers.Logger.LogOperation(__0, __1, __2);

            switch (__0)
            {
                case 253: // OpRaise
                    break;

                case 230: // Auth
                    break;

                case 226: // OpJoinRoom
                    if (InternalSettings.PhotonNameSpoof) OperationHandler.ChangeName226(__1);
                    if (InternalSettings.NoProperties) OperationHandler.DisableProperties226And252(__1);
                    break;

                case 252: // Set Props of Actor & Room
                    if (InternalSettings.PhotonNameSpoof) OperationHandler.ChangeName252(__1);
                    if (InternalSettings.NoProperties) OperationHandler.DisableProperties226And252(__1);
                    break;
            }

            return true;
        }

        private static bool OpRaiseEventPrefix(byte __0, ref Il2CppSystem.Object __1, RaiseEventOptions __2, SendOptions __3)
        {
            if (InternalSettings.DebugLogOpRaise) Wrappers.Logger.LogOpRaise(__0, __1, __2, __3);

            switch (__0)
            {
                case 101:
                    break;

                case 102:
                    break;
            }

            return true;
        }

        private static bool OnEventPrefix(EventData __0)
        {
            if (InternalSettings.DebugLogOnEvent) Wrappers.Logger.LogEventData(__0);

            switch (__0.Code)
            {
                case 102:
                    break;

                case 253:
                    return Modules.EventHandler.PropertiesChangedEvent(__0);
            }

            return true;
        }
    }
}
