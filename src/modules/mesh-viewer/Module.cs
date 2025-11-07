using System;

using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod.MeshViewer {
    public class Module : BaseModule {
        /**
         * <summary>
         * Initializes this module.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         */
        public Module(ConfigFile configFile) : base(configFile, "Mesh Viewer") {
        }

        /**
         * <summary>
         * Iterates over objects in the scene and categorises them.
         * </summary>
         */
        private void Categorise() {
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>()) {
                Categories categories = CategoriesExt.From(obj);
                if (categories == Categories.None) {
                    continue;
                }

                LogDebug($"===== {obj.name} =====");
                foreach (Categories cat in Enum.GetValues(typeof(Categories))) {
                    if (categories.HasFlag(cat) == true) {
                        LogDebug($"{cat}");
                    }
                }
            }
        }

        /**
         * <summary>
         * Executes when the module gets enabled.
         * </summary>
         */
        protected override void OnModuleEnabled() {
            Categorise();
        }

        /**
         * <summary>
         * Executes when the module gets disabled.
         * </summary>
         */
        protected override void OnModuleDisabled() {
        }

        /**
         * <summary>
         * Executes on each scene load.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        public override void OnSceneLoad(Scene scene) {
            Categorise();
        }

        /**
         * <summary>
         * Executes on scene unloads.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        public override void OnSceneUnload(Scene scene) {
        }
    }
}
