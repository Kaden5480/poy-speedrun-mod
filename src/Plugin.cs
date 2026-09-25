using System;
using System.Linq;

using BepInEx;
using HarmonyLib;
using ModMenu;
using UILib.Patches;
using UnityEngine.SceneManagement;

namespace SpeedrunMod {
    [BepInDependency("com.github.Kaden5480.poy-ui-lib")]
    [BepInDependency(
        "com.github.Kaden5480.poy-mod-menu",
        BepInDependency.DependencyFlags.SoftDependency
    )]
    [BepInPlugin("com.github.Kaden5480.poy-speedrun-mod", "Speedrun Mod", PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin {
        private static Plugin instance;

        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        private void Awake() {
            instance = this;

            SceneLoads.AddLoadListener((Scene scene) => {
                Cache.FindObjects(scene);
                Modules.PeakSweeper.Module.SceneLoad();
                Modules.VelocityHUD.Module.SceneLoad();
            });

            SceneLoads.AddUnloadListener(delegate {
                Modules.PeakSweeper.Module.SceneUnload();
                Modules.VelocityHUD.Module.SceneUnload();
                Cache.Clear();
            });

            // The cache needs to have patches applied first
            Patcher.Patch(typeof(Cache));

            Modules.Misc.Module.Init(this.Config);
            Modules.PeakSweeper.Module.Init(this.Config);
            Modules.VelocityHUD.Module.Init(this.Config);

            // Register with Mod Menu as an optional dependency
            if (AccessTools.AllAssemblies().FirstOrDefault(
                    a => a.GetName().Name == "ModMenu"
                ) != null
            ) {
                Register();
            }
        }

        /**
         * <summary>
         * Executes each frame.
         * </summary>
         */
        private void Update() {
            Modules.VelocityHUD.Module.Update();
        }

        /**
         * <summary>
         * Registers with Mod Menu.
         * </summary>
         */
        private void Register() {
            ModInfo info = ModManager.Register(this);
            info.license = "GPL-3.0";

            info.Add(typeof(Modules.Misc.Config));
            info.Add(typeof(Modules.PeakSweeper.Config));
            info.Add(typeof(Modules.VelocityHUD.Config));
        }

        /**
         * <summary>
         * Logs a debug message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        internal static void LogDebug(string message) {
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
        internal static void LogInfo(string message) {
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
        internal static void LogError(string message) {
            if (instance == null) {
                Console.WriteLine($"[Error] SpeedrunMod: {message}");
                return;
            }
            instance.Logger.LogError(message);
        }
    }
}
