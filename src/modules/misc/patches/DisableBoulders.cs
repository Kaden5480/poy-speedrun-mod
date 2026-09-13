using HarmonyLib;

namespace SpeedrunMod.Modules.Misc.Patches {
    /**
     * <summary>
     * Prevents boulders from respawning
     * on Ugsome Stórr while this module
     * is enabled.
     * </summary>
     */
    [HarmonyPatch(typeof(FallingRock), "InitialiseRock")]
    internal static class DisableBoulders {
        private static bool Prefix() {
            // Only run on Ugsome Stórr
            if ("Peak_18_FallingBoulders".Equals(Cache.scene.name) == false) {
                return true;
            }

            // Only run if enabled
            if (Config.noBoulders.Value == false) {
                return true;
            }

            // Bypass initializing boulders
            return false;
        }
    }
}
