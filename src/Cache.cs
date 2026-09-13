using UnityEngine;

namespace SpeedrunMod {
    /**
     * <summary>
     * Caches useful objects on scene loads.
     * </summary>
     */
    internal static class Cache {
        // The "climbing" object as an example
        internal static Climbing climbing { get; private set; }

        /**
         * <summary>
         * Finds objects on scene loads.
         * </summary>
         */
        internal static void FindObjects() {
            climbing = GameObject.FindObjectOfType<Climbing>();
        }

        /**
         * <summary>
         * Clears the cache on scene unloads.
         * </summary>
         */
        internal static void Clear() {
            climbing = null;
        }
    }
}
