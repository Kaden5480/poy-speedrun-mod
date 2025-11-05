using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod {
    [BepInPlugin("com.github.Kaden5480.poy-speedrun-mod", "Speedrun Mod", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        /**
         * <summary>
         * Uses a better entry point to detect when
         * custom levels have been fully loaded.
         * </summary>
         */
        [HarmonyPatch(typeof(CustomLevel_DistanceActivator), "InitializeObjects")]
        protected static class PatchCustomSceneLoad {
            public static void Postfix() {
                Scene scene = SceneManager.GetActiveScene();
                // Make sure this only runs in a custom level
                if (scene.buildIndex == 69) {
                    Plugin.instance.OnAnySceneLoad(scene);
                }
            }
        }

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

            SceneManager.sceneLoaded += OnSceneLoad;
            SceneManager.sceneUnloaded += OnSceneUnload;

            Harmony.CreateAndPatchAll(typeof(PatchCustomSceneLoad));
        }

        /**
         * <summary>
         * Executes when the plugin is being destroyed.
         * </summary>
         */
        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoad;
            SceneManager.sceneUnloaded -= OnSceneUnload;
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

        /**
         * <summary>
         * Executes on each scene load.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        private void OnAnySceneLoad(Scene scene) {
            // Find objects before doing anything else
            Cache.FindObjects();
            LogDebug("Cached scene objects");

            foreach (BaseModule module in modules) {
                module.OnSceneLoad(scene);
            }
        }

        /**
         * <summary>
         * Executes on each scene load.
         * This will only do anything for non-custom levels.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         * <param name="mode">The mode the scene was loaded with</param>
         */
        protected void OnSceneLoad(Scene scene, LoadSceneMode mode) {
            // Only run on non-custom levels
            if (scene.buildIndex != 69) {
                OnAnySceneLoad(scene);
            }
        }

        /**
         * <summary>
         * Executes on scene unloads.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        private void OnSceneUnload(Scene scene) {
            foreach (BaseModule module in modules) {
                module.OnSceneUnload(scene);
            }

            // Clear the cache last
            Cache.Clear();
            LogDebug("Cleared cache");
        }

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
}
