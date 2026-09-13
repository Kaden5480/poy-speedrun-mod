using BepInEx.Configuration;
using ModMenu.Config;

namespace SpeedrunMod.Modules.NoKnockouts {
    internal static class Config {
        [Field("Enabled")]
        internal static ConfigEntry<bool> enabled;

        internal static void Init(ConfigFile configFile) {
            enabled = configFile.Bind(
                Module.name, "enabled", true,
                $"Whether {Module.name} is enabled."
            );
        }
    }
}
