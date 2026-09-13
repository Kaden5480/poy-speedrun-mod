using BepInEx.Configuration;
using ModMenu.Config;

namespace SpeedrunMod.Modules.Misc {
    internal static class Config {
        [Field("Enabled")]
        internal static ConfigEntry<bool> enabled;

        [Field("No Boulders")]
        internal static ConfigEntry<bool> noBoulders;

        [Field("No Knockouts")]
        internal static ConfigEntry<bool> noKnockouts;

        internal static void Init(ConfigFile configFile) {
            enabled = configFile.Bind(
                Module.name, "enabled", true,
                $"Whether to enable the {Module.name} module."
            );

            noBoulders = configFile.Bind(
                Module.name, "noBoulders", true,
                "Whether to prevent boulders from spawning on Ugsome Stórr."
            );

            noKnockouts = configFile.Bind(
                Module.name, "noKnockouts", true,
                "Whether to disable the knockout animation."
            );
        }
    }
}
