using System;

using HarmonyLib;

namespace SpeedrunMod {
    /**
     * <summary>
     * A class for applying patches and logging patching.
     * </summary>
     */
    internal static class Patcher {
        private static Logger logger = new Logger(typeof(Patcher));

        /**
         * <summary>
         * Applies a patch of a given type.
         * </summary>
         * <param name="type">The patch to apply</param>
         */
        internal static void Patch(Type type) {
            Harmony.CreateAndPatchAll(type);
            logger.LogDebug($"Applied patch: {type}");
        }
    }
}
