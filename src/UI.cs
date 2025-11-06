using System.Collections.Generic;

using UnityEngine;

using SpeedrunMod.Common;

namespace SpeedrunMod {
    public class UI {
        private bool enabled = false;
        private List<BaseModule> modules;

        private const float width = 180;
        private const float height = 200;

        /**
         * <summary>
         * Initializes the UI.
         * </summary>
         * <param name="modules">All modules available in this mod</param>
         */
        public UI(List<BaseModule> modules) {
            this.modules = modules;
        }

        /**
         * <summary>
         * Renders the main configuration UI.
         * </summary>
         */
        public void RenderMain() {
            float x = (Screen.width - width) / 2;
            float y = (Screen.height - height) / 2;

            GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

            foreach (BaseModule module in modules) {
                // If the module has no UI, then you can't configure it
                if (module.ui == null) {
                    continue;
                }

                // Otherwise, add an button to enter the mod's configuration
                if (GUILayout.Button(module.name) == true) {
                    module.ui.enabled = true;
                }
            }

            GUILayout.FlexibleSpace();

            // Allow closing the main UI from a button
            if (GUILayout.Button("Close") == true) {
                enabled = false;
            }

            GUILayout.EndArea();
        }

        /**
         * <summary>
         * Renders the UI.
         * </summary>
         */
        public void Render() {
            if (enabled == false) {
                return;
            }

            // If a sub-UI is enabled, render it instead
            foreach (BaseModule module in modules) {
                if (module.ui == null) {
                    continue;
                }

                // Don't render if a sub UI is displayed
                if (module.ui.enabled == true) {
                    module.ui.Render();
                    return;
                }
            }

            // Otherwise, render the main UI
            RenderMain();
        }
    }
}

/**

Main Config:
Stores all configs

Main UI:
Iterates over UI classes and renders enabled ones
If all disabled, show default UI to select which sub-UI to render

Fast Reset
    Config
    UI
        enabled

Fast Coffee
    Config
    UI
        enabled
 */
