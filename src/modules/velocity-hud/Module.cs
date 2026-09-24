using BepInEx.Configuration;
using HarmonyLib;
using UILib;
using UILib.Patches;
using UnityEngine;

namespace SpeedrunMod.Modules.VelocityHUD {
    /**
     * <summary>
     * The velocity HUD module.
     * </summary>
     */
    internal static class Module {
        internal const string name = "Velocity HUD";

        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);
        }

        /**
         * <summary>
         * Plays the pocketwatch clicking animation and sound effect.
         * </summary>
         */
        private static void PlayAnimation() {
            TimeAttack timeAttack = Cache.timeAttack;

            timeAttack.pocketwatchSound.volume = 0.48f;
            timeAttack.pocketwatchSound.pitch = 1.1f;
            timeAttack.pocketwatchSound.clip = timeAttack.s_stopTime;
            timeAttack.pocketwatchSound.Play();

            timeAttack.pocketWatchAnim.Play("pocketwatch_click");
        }

        internal static void SceneLoad() {
            UI.Create();
        }

        internal static void SceneUnload() {
            UI.Destroy();
        }

        /**
         * <summary>
         * Runs every frame to:
         * - Check if the toggle keybind was activated
         * - Update tracked velocity info
         * - Update the UI
         * </summary>
         */
        internal static void Update() {
            if (Cache.playerMove == null
                || Cache.timeAttack == null
            ) {
                return;
            }

            // Update velocity info
            Tracker.Update();

            // Update UI
            UI.Update();

            // Check toggle keybind
            if (Cache.timeAttack.isOpenNow == true
                && Shortcut.canRun == true
                && LockHandler.isPaused == false
                && LockHandler.isCursorFree == false
                && LockHandler.isNavigationLocked == false
                && Input.GetKeyDown(Config.toggleKeybind.Value)
            ) {
                Config.enabled.Value = !Config.enabled.Value;
                PlayAnimation();
            }
        }
    }
}
