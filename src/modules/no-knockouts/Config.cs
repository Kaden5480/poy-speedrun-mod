using BepInEx.Configuration;
using ModMenu.Config;

namespace SpeedrunMod.Modules.NoKnockouts {
    /**
     * <summary>
     * The config for this module.
     * </summary>
     */
    internal static class Config {
        [Field("Enabled")]
        internal static ConfigEntry<bool> enabled;

        /**
         * <summary>
         * Initializes the config, binding to the provided
         * config file.
         * </summary>
         * <param name="configFile">The config file to bind to</param>
         */
        internal static void Init(ConfigFile configFile) {
            enabled = configFile.Bind(
                Module.name, "enabled", true,
                $"Whether {Module.name} is enabled"
            );
        }
    }
}
