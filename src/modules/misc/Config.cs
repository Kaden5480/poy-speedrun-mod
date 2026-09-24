using BepInEx.Configuration;
using ModMenu.Config;

namespace SpeedrunMod.Modules.Misc {
    internal static class Config {
        [Field("No Boulders")]
        internal static ConfigEntry<bool> noBoulders;

        [Field("No Knockouts")]
        internal static ConfigEntry<bool> noKnockouts;

        [Field("Ten Points Everywhere")]
        internal static ConfigEntry<bool> tenPointsEverywhere;

        internal static void Init(ConfigFile configFile) {
            noBoulders = configFile.Bind(
                Module.name, "noBoulders", false,
                "Whether to prevent boulders from spawning on Ugsome Stórr."
            );

            noKnockouts = configFile.Bind(
                Module.name, "noKnockouts", false,
                "Whether to disable the knockout animation."
            );

            tenPointsEverywhere = configFile.Bind(
                Module.name, "tenPointsEverywhere", false,
                "Whether to allow 10 point crampons on every level."
                + " Make sure you have all collectibles on the peak for this to unlock."
            );
        }
    }
}
