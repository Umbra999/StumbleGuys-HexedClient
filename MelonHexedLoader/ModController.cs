using System;
using System.Reflection;

namespace HexedLoader
{
    internal class ModController
    {
        public static void RegisterMod(Type mod)
        {
            foreach (MethodInfo methodInfo in mod.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                switch (methodInfo.Name)
                {
                    case "OnApplicationStart":
                        if (methodInfo.GetParameters().Length == 0) onApplicationStartMethod = methodInfo;
                        break;

                    case "OnApplicationQuit":
                        if (methodInfo.GetParameters().Length == 0) onApplicationQuitMethod = methodInfo;
                        break;

                    case "OnLevelWasLoaded":
                        if (methodInfo.GetParameters().Length == 1 && methodInfo.GetParameters()[0].ParameterType == typeof(int)) onLevelWasLoadedMethod = methodInfo;
                        break;

                    case "OnLevelWasInitialized":
                        if (methodInfo.GetParameters().Length == 1 && methodInfo.GetParameters()[0].ParameterType == typeof(int)) onLevelWasInitializedMethod = methodInfo;
                        break;

                    case "OnUpdate":
                        if (methodInfo.GetParameters().Length == 0) onUpdateMethod = methodInfo;
                        break;

                    case "OnFixedUpdate":
                        if (methodInfo.GetParameters().Length == 0) onFixedUpdateMethod = methodInfo;
                        break;

                    case "OnLateUpdate":
                        if (methodInfo.GetParameters().Length == 0) onLateUpdateMethod = methodInfo;
                        break;

                    case "OnGUI":
                        if (methodInfo.GetParameters().Length == 0) onGUIMethod = methodInfo;
                        break;

                    case "OnModSettingsApplied":
                        if (methodInfo.GetParameters().Length == 0) onModSettingsApplied = methodInfo;
                        break;
                }
            }
        }

        public static void OnApplicationStart()
        {
            onApplicationStartMethod?.Invoke(null, new object[0]);
        }

        public static void OnApplicationQuit()
        {
            onApplicationQuitMethod?.Invoke(null, new object[0]);
        }

        public static void OnLevelWasLoaded(int level)
        {
            onLevelWasLoadedMethod?.Invoke(null, new object[] { level });
        }

        public static void OnLevelWasInitialized(int level)
        {
            onLevelWasInitializedMethod?.Invoke(null, new object[] { level });
        }

        public static void OnUpdate()
        {
            onUpdateMethod?.Invoke(null, new object[0]);
        }

        public static void OnFixedUpdate()
        {
            onFixedUpdateMethod?.Invoke(null, new object[0]);
        }

        public static void OnLateUpdate()
        {
            onLateUpdateMethod?.Invoke(null, new object[0]);
        }

        public static void OnGUI()
        {
            onGUIMethod?.Invoke(null, new object[0]);
        }

        public static void OnModSettingsApplied()
        {
            onModSettingsApplied?.Invoke(null, new object[0]);
        }

        private static MethodInfo onApplicationStartMethod;

        private static MethodInfo onApplicationQuitMethod;

        private static MethodInfo onLevelWasLoadedMethod;

        private static MethodInfo onLevelWasInitializedMethod;

        private static MethodInfo onUpdateMethod;

        private static MethodInfo onFixedUpdateMethod;

        private static MethodInfo onLateUpdateMethod;

        private static MethodInfo onGUIMethod;

        private static MethodInfo onModSettingsApplied;
    }
}