using System;

using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine.SceneManagement;

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

        /**
         * <summary>
         * Executes each frame.
         * </summary>
         */
        public virtual void Update() {}

        /**
         * <summary>
         * Renders the UI.
         * </summary>
         */
        public virtual void OnGUI() {}

        /**
         * <summary>
         * Executes on each scene load.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        public virtual void OnSceneLoad(Scene scene) {}

        /**
         * <summary>
         * Executes on scene unloads.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        public virtual void OnSceneUnload(Scene scene) {}
    }
}
