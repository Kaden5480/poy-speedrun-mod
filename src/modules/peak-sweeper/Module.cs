using BepInEx.Configuration;
using UILib;
using UILib.Patches;
using UnityEngine;

namespace SpeedrunMod.Modules.PeakSweeper {
    /**
     * <summary>
     * The main module for Peak Sweeper.
     * </summary>
     */
    internal static class Module {
        private static Logger logger = new Logger(typeof(Module));

        /**
         * <summary>
         * The name of this module.
         * </summary>
         */
        internal const string name = "Peak Sweeper";

        /**
         * <summary>
         * Initializes this module, binding its config to the
         * provided config file.
         * </summary>
         * <param name="configFile">The config file to bind to</param>
         */
        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);

            SceneLoads.AddLoadListener(delegate {
                SweepAll();
            }, SceneType.BuiltIn | SceneType.Custom | SceneType.QuickPlaytest);
        }

        private static bool ShouldSweep(GameObject obj) {
            BrickHold brickHold = obj.GetComponent<BrickHold>();
            if (brickHold != null && brickHold.popoutInstantly == true) {
                return true;
            }

            BrittleIce brittleIce = obj.GetComponent<BrittleIce>();
            if (brittleIce != null && brittleIce.setCustomHp == false) {
                return true;
            }

            Shrubbery shrubbery = obj.GetComponent<Shrubbery>();
            if (shrubbery != null) {
                return true;
            }

            SnowOnIce snow = obj.GetComponent<SnowOnIce>();
            if (snow != null) {
                return true;
            }

            return false;
        }

        private static void Sweep(GameObject obj) {
            obj.SetActive(false);

            foreach (Collider collider in obj.GetComponentsInChildren<Collider>()) {
                collider.enabled = false;
            }

            foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>()) {
                renderer.enabled = false;
            }

            logger.LogDebug($"Sweeped {obj}");
        }

        private static void SweepAll() {
            if (Config.enabled.Value == false) {
                return;
            }

            logger.LogDebug("Sweeping...");

            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>()) {
                if (ShouldSweep(obj) == false) {
                    continue;
                }

                Sweep(obj);
            }

            logger.LogDebug("Finished sweeping");
        }
    }
}
