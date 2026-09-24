using System;

using UILib;
using UILib.Components;
using UILib.Layouts;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedrunMod.Modules.VelocityHUD {
    internal static class UI {
        private static Area area;
        private static Label labelMax;
        private static Label labelCurrent;

        /**
         * <summary>
         * Creates a label with an extra outline effect suitable
         * for displaying velocity info.
         * </summary>
         */
        private static Label MakeLabel(Outline outline) {
            Label label = new Label(25);
            label.SetSize(200f, 30f);
            label.text.alignByGeometry = false;

            Outline newOutline = label.gameObject.AddComponent<Outline>();
            newOutline.effectColor = outline.effectColor;
            newOutline.effectDistance = outline.effectDistance;

            return label;
        }

        /**
         * <summary>
         * Creates the entire velocity HUD UI.
         * </summary>
         */
        internal static void Create() {
            if (Cache.timeAttackUI == null) {
                return;
            }

            Outline outline = Cache.timeAttackUI.GetComponent<Outline>();

            area = new Area();
            area.gameObject.name = "Velocity HUD";
            area.rectTransform.SetParent(Cache.timeAttackUI.transform);
            area.SetSize(600f, 30f);
            area.SetAnchor(AnchorType.TopMiddle);
            area.SetOffset(0f, 0f);

            labelMax = MakeLabel(outline);
            area.Add(labelMax);

            labelCurrent = MakeLabel(outline);
            area.Add(labelCurrent);

            Theme theme = Theme.GetTheme();
            theme.foreground = Color.white;
            area.SetTheme(theme);
        }

        /**
         * <summary>
         * Destroys the velocity HUD UI.
         * </summary>
         */
        internal static void Destroy() {
            if (area != null) {
                area.Destroy();
            }

            area = null;
            labelMax = null;
            labelCurrent = null;
        }

        /**
         * <summary>
         * Formats a float for displaying in the UI.
         * </summary>
         */
        private static string FormatFloat(float n) {
            return String.Format("{0:0,0.0000}", n);
        }

        /**
         * <summary>
         * Determines whether velocity HUD can be shown.
         * </summary>
         */
        private static bool CanShow() {
            if (Config.enabled.Value == false
                || Cache.timeAttack.watchReady == false
            ) {
                return false;
            }

            return Cache.routingFlag.currentlyUsingFlag == true
                || Cache.timeAttackHoldsUI.activeSelf == true;
        }

        /**
         * <summary>
         * Updates the state of the UI.
         * </summary>
         */
        internal static void Update() {
            area.gameObject.SetActive(CanShow());
            if (CanShow() == false) {
                return;
            }

            labelMax.gameObject.SetActive(true);
            labelCurrent.gameObject.SetActive(!TimeAttack.receivingScore);

            labelMax.SetText($"Max: {FormatFloat(Tracker.max)}");
            labelCurrent.SetText($"Current: {FormatFloat(Tracker.current.magnitude)}");
        }
    }
}
