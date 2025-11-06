using System;
using System.Collections.Generic;

using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod {
    [BepInPlugin("com.github.Kaden5480.poy-speedrun-mod", "Speedrun Mod", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin instance { get; private set; }

        private List<BaseModule> modules;
        private UI ui;

        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        private void Awake() {
            if (instance != null) {
                return;
            }

            instance = this;

            modules = new List<BaseModule>();
            modules.Add(new MeshViewer.Module(Config));
            modules.Add(new NoBoulders.Module(Config));
            modules.Add(new NoKnockouts.Module(Config));
            modules.Add(new PeakSweeper.Module(Config));
            modules.Add(new VelocityHUD.Module(Config));

            ui = new UI(modules);

            SceneManager.sceneLoaded += UEOnSceneLoad;
            SceneManager.sceneUnloaded += UEOnSceneUnload;

            Harmony.CreateAndPatchAll(typeof(PatchCustomSceneLoad));
            Harmony.CreateAndPatchAll(typeof(PatchCustomQuickTest));
        }

        /**
         * <summary>
         * Executes when the plugin is being destroyed.
         * </summary>
         */
        private void OnDestroy() {
            SceneManager.sceneLoaded -= UEOnSceneLoad;
            SceneManager.sceneUnloaded -= UEOnSceneUnload;
        }

        /**
         * <summary>
         * Executes each frame.
         * </summary>
         */
        private void Update() {
            foreach (BaseModule module in modules) {
                module.Update();
            }
        }

        /**
         * <summary>
         * Renders the UI.
         * </summary>
         */
        private void OnGUI() {
            ui.Render();

            foreach (BaseModule module in modules) {
                module.OnGUI();
            }
        }

#region Scene Loading/Unloading

        /**
         * <summary>
         * Dispatches scene loads to modules.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        private void DispatchSceneLoad(Scene scene) {
            // Find objects before doing anything else
            Cache.FindObjects();
            LogDebug("Cached scene objects");

            foreach (BaseModule module in modules) {
                module.OnSceneLoad(scene);
            }
        }

        /**
         * <summary>
         * Dispatches scene unloads to modules.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        private void DispatchSceneUnload(Scene scene) {
            foreach (BaseModule module in modules) {
                module.OnSceneUnload(scene);
            }

            // Clear the cache last
            Cache.Clear();
            LogDebug("Cleared cache");
        }

        /**
         * <summary>
         * Dispatches scene load calls when a custom level (in normal play mode)
         * has been fully loaded.
         * </summary>
         */
        [HarmonyPatch(typeof(CustomLevel_DistanceActivator), "InitializeObjects")]
        protected static class PatchCustomSceneLoad {
            public static void Postfix() {
                LogDebug("Custom level (normal play) dispatched");
                Plugin.instance.DispatchSceneLoad(SceneManager.GetActiveScene());
            }
        }

        /**
         * <summary>
         * Dispatches scene load/unload calls when quick playtest mode
         * is activated/deactivated.
         * </summary>
         */
        [HarmonyPatch(typeof(LevelEditorManager), "SetPlaymodeObjects")]
        protected static class PatchCustomQuickTest {
            public static void Postfix(bool isPlaymode) {
                // If not quicktest, just ignore
                if (isPlaymode == true) {
                    LogDebug("Custom level (quick playtest) dispatched");
                    Plugin.instance.DispatchSceneLoad(SceneManager.GetActiveScene());
                }
                else {
                    LogDebug("Custom level (exit quick playtest) dispatched");
                    Plugin.instance.DispatchSceneUnload(SceneManager.GetActiveScene());
                }
            }
        }

        /**
         * <summary>
         * Executes on each scene load.
         * This will only do anything for non-custom levels
         * as custom levels need special entry points.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         * <param name="mode">The mode the scene was loaded with</param>
         */
        protected void UEOnSceneLoad(Scene scene, LoadSceneMode mode) {
            // Only run on non-custom levels
            if (scene.buildIndex != 69) {
                LogDebug("Unity scene load dispatched");
                DispatchSceneLoad(scene);
            }
        }

        /**
         * <summary>
         * Executes on scene unloads.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        private void UEOnSceneUnload(Scene scene) {
            LogDebug("Unity scene unload dispatched");
            DispatchSceneUnload(scene);
        }

#endregion

#region Logging

        /**
         * <summary>
         * Logs a debug message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        public static void LogDebug(string message) {
#if DEBUG
            if (instance == null) {
                Console.WriteLine($"[Debug] SpeedrunMod: {message}");
                return;
            }

            instance.Logger.LogInfo(message);
#else
            if (instance != null) {
                instance.Logger.LogDebug(message);
            }
#endif
        }

        /**
         * <summary>
         * Logs an informational message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        public static void LogInfo(string message) {
            if (instance == null) {
                Console.WriteLine($"[Info] SpeedrunMod: {message}");
                return;
            }
            instance.Logger.LogInfo(message);
        }

        /**
         * <summary>
         * Logs an error message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        public static void LogError(string message) {
            if (instance == null) {
                Console.WriteLine($"[Error] SpeedrunMod: {message}");
                return;
            }
            instance.Logger.LogError(message);
        }
    }

#endregion

}
