using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpeedrunMod {
    /**
     * <summary>
     * Caches useful objects on scene loads.
     * </summary>
     */
    internal static class Cache {
        internal static Scene scene { get; private set; }

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
            footPlacement = null;
            inventory = null;
            stemFoot = null;
        }
    }
}
