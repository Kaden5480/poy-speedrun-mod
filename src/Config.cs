using BepInEx.Configuration;
using ModMenu.Config;
using UnityEngine;

namespace SpeedrunMod {
    /**
     * <summary>
     * The overall config for Speedrun Mod.
     * </summary>
     */
    internal static class Config {
        [Field("Toggle Keybind")]
        internal static ConfigEntry<KeyCode> toggleKeybind;

        /**
         * <summary>
         * Initializes the config, binding to the provided
         * config file.
         * </summary>
         * <param name="configFile">The config file to bind to</param>
         */
        internal static void Init(ConfigFile configFile) {
            toggleKeybind = configFile.Bind(
                "General", "toggleKeybind", KeyCode.PageUp,
                "The keybind to toggle the main Speedrun Mod UI."
            );
        }
    }
}
