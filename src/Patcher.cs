using System;

using HarmonyLib;

namespace SpeedrunMod {
    internal static class Patcher {
        private static Logger logger = new Logger(typeof(Patcher));

        /**
         * <summary>
         * Applies a patch.
         * </summary>
         * <param name="patch">The patch to apply</param>
         */
        internal static void Patch(Type patch) {
            Harmony.CreateAndPatchAll(patch);
            logger.LogDebug($"Applied patch: {patch}");
        }
    }
}
