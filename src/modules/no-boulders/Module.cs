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
            // TODO: only run on ugsome
            if (Config.enabled.Value == true) {
                return false;
            }

            return true;
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

        /**
         * <summary>
         * Initializes this module, binding its config to the
         * provided config file.
         * </summary>
         * <param name="configFile">The config file to bind to</param>
         */
        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);
            Patcher.Patch(typeof(DisableBoulders));
        }
    }
}
