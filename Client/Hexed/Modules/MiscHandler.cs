using Hexed.Wrappers;
using System;
using System.Collections;
using UnityEngine;

namespace Hexed.Modules
{
    internal class MiscHandler : MonoBehaviour
    {
        public MiscHandler(IntPtr ptr) : base(ptr) { }

        public void Start()
        {
            DisableUnityAnalytics();
            DestroyAnalytics().Start();
        }

        private static IEnumerator DestroyAnalytics()
        {
            while (GameObject.Find("GameAnalytics") == null) yield return null;
            DestroyImmediate(GameObject.Find("GameAnalytics"));

            while (GameObject.Find("AnalyticsContainer") == null) yield return null;
            DestroyImmediate(GameObject.Find("AnalyticsContainer"));

            Wrappers.Logger.Log("Destroyed Analytics", Wrappers.Logger.LogsType.Info);
        }

        public static void DisableUnityAnalytics()
        {
            UnityEngine.Analytics.Analytics.enabled = false;
            UnityEngine.Analytics.Analytics.enabledInternal = false;
            UnityEngine.Analytics.Analytics.initializeOnStartup = false;
            UnityEngine.Analytics.Analytics.initializeOnStartupInternal = false;
            UnityEngine.Analytics.Analytics.deviceStatsEnabled = false;
            UnityEngine.Analytics.Analytics.deviceStatsEnabledInternal = false;
            UnityEngine.Analytics.Analytics.limitUserTracking = true;
            UnityEngine.Analytics.Analytics.limitUserTrackingInternal = true;
            UnityEngine.Analytics.PerformanceReporting.enabled = false;
        }
    }
}
