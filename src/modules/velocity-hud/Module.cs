using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod.VelocityHUD {
    public class Module : BaseModule {
        private Config config;
        private HUD hud;

        /**
         * <summary>
         * Initializes this module.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         */
        public Module(ConfigFile configFile) : base(configFile, "Velocity HUD") {
            config = new Config();

            config.toggleKeybind = configFile.Bind(
                name, "toggleKeybind", KeyCode.Mouse2,
                "Keybind to toggle the UI"
            );
            config.showUI = configFile.Bind(
                name, "showUI", true,
                "Whether to show the UI"
            );
            config.showExtended = configFile.Bind(
                name, "showExtended", false,
                "Whether to show extended velocity info"
            );

            hud = new HUD(config);
        }

        /**
         * <summary>
         * Checks if everything required to run is available.
         * </summary>
         */
        private bool CanUse() {
            return Cache.playerMove != null
                && Cache.playerRb != null
                && Cache.routingFlag != null
                && Cache.timeAttack != null
                && Cache.timeAttackUI.IsComplete();
        }

        /**
         * <summary>
         * Plays the pocketwatch animation.
         * </summary>
         */
        private void PlayAnimation() {
            TimeAttack timeAttack = Cache.timeAttack;

            timeAttack.pocketwatchSound.volume = 0.48f;
            timeAttack.pocketwatchSound.pitch = 1.1f;
            timeAttack.pocketwatchSound.clip = timeAttack.s_stopTime;
            timeAttack.pocketwatchSound.Play();
            timeAttack.pocketWatchAnim.Play("pocketwatch_click");
        }

        /**
         * <summary>
         * Executes when the module gets enabled.
         * </summary>
         */
        protected override void OnModuleEnabled() {
            if (CanUse() == true) {
                hud.Create();
            }
        }

        /**
         * <summary>
         * Executes when the module gets disabled.
         * </summary>
         */
        protected override void OnModuleDisabled() {
            hud.Destroy();
        }

        /**
         * <summary>
         * Executes on each scene load.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        public override void OnSceneLoad(Scene scene) {
            if (CanUse() == true) {
                hud.Create();
            }
        }

        /**
         * <summary>
         * Executes on each scene unload.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        public override void OnSceneUnload(Scene scene) {
            hud.Destroy();
        }

        /**
         * <summary>
         * Executes each frame.
         * </summary>
         */
        public override void Update() {
            if (enabled == false || CanUse() == false) {
                return;
            }

            if (Cache.timeAttack.isOpenNow == true
                && Input.GetKeyDown(config.toggleKeybind.Value) == true
            ) {
                config.showUI.Value = !config.showUI.Value;
                PlayAnimation();
            }

            hud.Update();
        }
    }
}
