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

        /**
         * <summary>
         * Finds objects on scene loads.
         * </summary>
         */
        internal static void FindObjects(Scene scene) {
            Cache.scene = scene;
        }

        /**
         * <summary>
         * Clears the cache on scene unloads.
         * </summary>
         */
        internal static void Clear() {
        }
    }
}
