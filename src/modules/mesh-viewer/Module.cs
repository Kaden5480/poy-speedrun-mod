using System;
using System.Collections.Generic;

using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod.MeshViewer {
    public class Module : BaseModule {
        private List<TrackedObject> objects;

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
         * Iterates over objects in the scene and tracks them.
         * </summary>
         */
        private void TrackObjects() {
            objects = new List<TrackedObject>();

            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>()) {
                if (ObjectTypesExt.From(obj) == ObjectTypes.None) {
                    continue;
                }

                objects.Add(new TrackedObject(obj));
            }
        }

        /**
         * <summary>
         * Executes when the module gets enabled.
         * </summary>
         */
        protected override void OnModuleEnabled() {
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
