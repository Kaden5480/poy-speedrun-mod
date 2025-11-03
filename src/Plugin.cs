using System;
using System.Collections.Generic;

using BepInEx;
using HarmonyLib;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod {
    [BepInPlugin("com.github.Kaden5480.poy-speedrun-mod", "Speedrun Mod", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        /**
         * <summary>
         * Uses a better entry point to detect when scenes
         * have been fully loaded.
         * </summary>
         */
        protected static class PatchSceneLoad {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(EnterPeakScene), "Start")]
            [HarmonyPatch(typeof(EnterRoomSegmentScene), "Start")]
            static void Postfix() {
                Plugin.instance.OnSceneLoad(SceneManager.GetActiveScene());
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
            modules.Add(new NoBoulders.Module(Config));
            modules.Add(new NoKnockouts.Module(Config));
            modules.Add(new PeakSweeper.Module(Config));
            modules.Add(new VelocityHUD.Module(Config));

            ui = new UI(modules);

            SceneManager.sceneUnloaded += OnSceneUnload;

            Harmony.CreateAndPatchAll(typeof(PatchSceneLoad));
        }

        /**
         * <summary>
         * Executes when the plugin is being destroyed.
         * </summary>
         */
        private void OnDestroy() {
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
        protected void OnSceneLoad(Scene scene) {
            // Find objects before doing anything else
            Cache.FindObjects();
            LogDebug("Cached scene objects");

            foreach (BaseModule module in modules) {
                module.OnSceneLoad(scene);
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
