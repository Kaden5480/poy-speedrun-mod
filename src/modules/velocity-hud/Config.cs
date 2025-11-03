using BepInEx.Configuration;
using UnityEngine;

namespace SpeedrunMod.VelocityHUD {
    public class Config {
        public ConfigEntry<KeyCode> toggleKeybind;
        public ConfigEntry<bool> showUI;
        public ConfigEntry<bool> showExtended;
    }
}
