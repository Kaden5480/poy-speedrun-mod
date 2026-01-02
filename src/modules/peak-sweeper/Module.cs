using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod.PeakSweeper {
    public class Module : BaseModule {
        private Config config;

        /**
         * <summary>
         * Initializes this module.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         */
        public Module(ConfigFile configFile) : base(configFile, "Peak Sweeper") {
            config = new Config();

            config.autoSweep = configFile.Bind(
                name, "autoSweep", true,
                "Whether to automatically run on scene load"
            );
            config.instantBricks = configFile.Bind(
                name, "instantBricks", true,
                "Whether to clean bricks that instantly pop out"
            );
            config.instantIce = configFile.Bind(
                name, "instantIce", true,
                "Whether to clean ice that instantly breaks"
            );
            config.shrubbery = configFile.Bind(
                name, "shrubbery", true,
                "Whether to clean shrubbery"
            );
            config.snow = configFile.Bind(
                name, "snow", true,
                "Whether to clean snow"
            );
        }

        /**
         * <summary>
         * Sweeps a single object.
         * </summary>
         * <param name="obj">The object to sweep</param>
         */
        private void Sweep(GameObject obj) {
            // Brick holds
            BrickHold brickHold = obj.GetComponent<BrickHold>();
            if (config.instantBricks.Value == true
                && brickHold != null
                && brickHold.popoutInstantly == true
            ) {
                LogDebug($"Destroyed brick: {obj.name}");
                GameObject.DestroyImmediate(obj);
                return;
            }

            // Brittle ice
            BrittleIce brittleIce = obj.GetComponent<BrittleIce>();
            if (config.instantIce.Value == true
                && brittleIce != null
                && brittleIce.setCustomHp == false
            ) {
                LogDebug($"Destroyed instantly breakable ice: {obj.name}");
                GameObject.DestroyImmediate(obj);
                return;
            }

            // Shrubbery
            Shrubbery shrubbery = obj.GetComponent<Shrubbery>();
            if (config.shrubbery.Value == true
                && shrubbery != null
            ) {
                LogDebug($"Destroyed shrubbery: {obj.name}");
                GameObject.DestroyImmediate(obj);
                return;
            }

            // Snow
            SnowOnIce snowOnIce = obj.GetComponent<SnowOnIce>();
            if (config.snow.Value == true
                && snowOnIce != null
            ) {
                LogDebug($"Destroyed snow: {obj.name}");
                GameObject.DestroyImmediate(obj);
                return;
            }
        }


        /**
         * <summary>
         * Sweeps the current scene.
         * </summary>
         */
        private void SweepAll() {
            // If this module is disabled, do nothing
            if (enabled == false) {
                return;
            }

            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>()) {
                Sweep(obj);
            }
        }

        /**
         * <summary>
         * Executes when the module gets enabled.
         * </summary>
         */
        protected override void OnModuleEnabled() {
            // Do nothing if auto sweeping is disabled
            if (config.autoSweep.Value == false) {
                return;
            }

            SweepAll();
        }

        /**
         * <summary>
         * Executes on each scene load.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        public override void OnSceneLoad(Scene scene) {
            // Do nothing if auto sweeping is disabled
            if (config.autoSweep.Value == false) {
                return;
            }

            SweepAll();
        }
    }
}
