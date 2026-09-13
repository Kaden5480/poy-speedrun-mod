using BepInEx.Configuration;
using HarmonyLib;

namespace SpeedrunMod.Modules.NoBoulders {
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
            if (Config.enabled.Value == false) {
                return true;
            }

            // Bypass initializing boulders
            return false;
        }
    }

    /**
     * <summary>
     * The main module for No Boulders.
     * </summary>
     */
    internal static class Module {
        /**
         * <summary>
         * The name of this module.
         * </summary>
         */
        internal const string name = "No Boulders";

        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);
            Patcher.Patch(typeof(DisableBoulders));
        }
    }
}
