using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpeedrunMod {
    /**
     * <summary>
     * Caches useful objects on scene loads.
     * </summary>
     */
    internal static class Cache {
        internal static Scene scene        { get; private set; }
        internal static bool isCustomLevel { get; private set; }

        internal static bool artefactsCollected { get; private set; }
        internal static bool ropesCollected     { get; private set; }

        internal static Footplacement footPlacement { get; private set; }
        internal static Inventory inventory         { get; private set; }
        internal static StemFoot stemFoot           { get; private set; }

        /**
         * <summary>
         * Finds objects on scene loads.
         * </summary>
         */
        internal static void FindObjects(Scene scene) {
            Cache.scene = scene;
            isCustomLevel = scene.buildIndex == 69;

            foreach (ArtefactOnPeak artefact in GameObject.FindObjectsOfType<ArtefactOnPeak>()) {
                if (artefact.gameObject.activeInHierarchy == true) {
                    artefactsCollected = false;
                    break;
                }
            }

            foreach (RopeCollectable rope in GameObject.FindObjectsOfType<RopeCollectable>()) {
                if (rope.gameObject.activeInHierarchy == true) {
                    ropesCollected = false;
                    break;
                }
            }

            footPlacement = GameObject.FindObjectOfType<Footplacement>();
            inventory = GameObject.FindObjectOfType<Inventory>();
            stemFoot = GameObject.FindObjectOfType<StemFoot>();
        }

        /**
         * <summary>
         * Clears the cache on scene unloads.
         * </summary>
         */
        internal static void Clear() {
            artefactsCollected = true;
            ropesCollected = true;
            isCustomLevel = false;
            footPlacement = null;
            inventory = null;
            stemFoot = null;
        }
    }
}
