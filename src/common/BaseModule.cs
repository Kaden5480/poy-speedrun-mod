using System;

using BepInEx.Configuration;
using HarmonyLib;

namespace SpeedrunMod.Common {
    public class BaseModule : BaseLoggable {
        // The config file and display name
        protected ConfigFile configFile;
        public string name { get; private set;   }

        // Whether this module is enabled
        private ConfigEntry<bool> _enabled;
        public bool enabled {
            get => _enabled.Value;
            set => _enabled.Value = value;
        }

        // The UI for configuration, can be null
        public BaseUI ui { get; protected set; }

        /**
         * <summary>
         * Initializes a BaseModule.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         * <param name="name">The display name of this module</param>
         */
        public BaseModule(ConfigFile configFile, string name) {
            this.configFile = configFile;
            this.name = name;

            _enabled = configFile.Bind(
                name, "enabled", false,
                $"Whether {name} is enabled"
            );
        }

        /**
         * <summary>
         * Applies early patches.
         * </summary>
         * <param name="patches">The patches to apply</param>
         */
        protected void PatchEarly(Type[] patches) {
            foreach (Type patch in patches) {
                Harmony.CreateAndPatchAll(patch);
                LogDebug($"Applied patch: {patch}");
            }
        }
    }
}
