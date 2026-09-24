using BepInEx.Configuration;
using ModMenu.Config;
using UnityEngine;

namespace SpeedrunMod.Modules.VelocityHUD {
    internal static class Config {
        [Field("Enabled")]
        internal static ConfigEntry<bool> enabled;

        [Field("Toggle Keybind")]
        internal static ConfigEntry<KeyCode> toggleKeybind;

        internal static void Init(ConfigFile configFile) {
            enabled = configFile.Bind(
                Module.name, "enabled", true,
                $"Whether {Module.name} is enabled"
            );

            toggleKeybind = configFile.Bind(
                Module.name, "toggleKeybind", KeyCode.Mouse2,
                $"Keybind to toggle {Module.name}. Use this keybind while"
                + " you are holding the pocketwatch."
            );
        }
    }
}
