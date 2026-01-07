using BepInEx.Configuration;

namespace SpeedrunMod.Modules.NoKnockouts {
    /**
     * <summary>
     * The main module for No Knockouts.
     * </summary>
     */
    internal static class Module {
        /**
         * <summary>
         * The name of this module.
         * </summary>
         */
        internal const string name = "No Knockouts";

        /**
         * <summary>
         * Initializes this module, binding its config to the
         * provided config file.
         * </summary>
         * <param name="configFile">The config file to bind to</param>
         */
        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);
        }
    }
}
