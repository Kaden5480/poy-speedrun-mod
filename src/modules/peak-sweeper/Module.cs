using System.Collections.Generic;

using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace SpeedrunMod.Modules.PeakSweeper {
    /**
     * <summary>
     * The peak sweeper module.
     * </summary>
     */
    internal static class Module {
        internal const string name = "Peak Sweeper";

        /**
         * <summary>
         * Objects which peak sweeper has currently swept.
         *
         * This is required to track which objects the distance activator
         * should leave as is, otherwise it will just re-enable things
         * which have been swept (which is undesirable).
         * </summary>
         */
        internal static HashSet<GameObject> sweptObjects
            = new HashSet<GameObject>();

        /**
         * <summary>
         * Patches the distance activator in custom levels to prevent it
         * from re-enabling objects which have been swept by peak sweeper.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CustomLevel_DistanceActivator), "HandleObjectActivation")]
        private static void EnsureSwept(GameObject go) {
            if (sweptObjects.Contains(go) == false) {
                return;
            }

            go.SetActive(false);
        }

        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);
            Patcher.Patch(typeof(Module));
        }

        /**
         * <summary>
         * Determines whether the provided object is legible
         * for being swept off the peak.
         * </summary>
         */
        private static bool ShouldCache(GameObject obj) {
            // Snow and shrubbery
            if (obj.layer == 22 || obj.tag == "Snow") {
                if (obj.name.Contains("Snow") || obj.name.Contains("ShrubberyObstacle")) {
                    return true;
                }
            }

            // Instantly breaking brittle ice
            if (obj.layer == LayerMask.NameToLayer("BrittleIce")) {
                if (obj.transform.parent == null) {
                    return false;
                }

                if (obj.transform.parent.name.Contains("BrittleIceContainer_Destroyable") == false
                    && obj.transform.parent.name.Contains("BrittleIce_Climbable_destroyall") == false
                ) {
                    return false;
                }

                if (obj.name.Contains("brittleice_child")) {
                    return true;
                }
            }

            // Bricks which popout instantly
            if (obj.layer == LayerMask.NameToLayer("Dirt")) {
                BrickHold brickHold = obj.GetComponent<BrickHold>();

                if (brickHold == null || brickHold.popoutInstantly == false) {
                    return false;
                }

                return true;
            }

            return false;
        }

        /**
         * <summary>
         * Sweeps the peak.
         * </summary>
         */
        internal static void SceneLoad() {
            if (Config.enabled.Value == false) {
                return;
            }

            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>()) {
                if (ShouldCache(obj) == false) {
                    continue;
                }

                sweptObjects.Add(obj);
                obj.SetActive(false);
            }
        }

        /**
         * <summary>
         * Clears the object cache.
         * </summary>
         */
        internal static void SceneUnload() {
            sweptObjects.Clear();
        }
    }
}
