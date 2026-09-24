using System;

using UILib;
using UILib.Components;
using UILib.Layouts;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedrunMod.Modules.VelocityHUD {
    internal static class UI {
        private static Vector3 normalPosition
            = new Vector3(-50f, -10f, 0f);

        private static Vector3 routingPosition
            = new Vector3(-110f, -10f, 0f);

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
            label.SetSize(200f, 70f);
            label.SetAlignment(AnchorType.TopLeft);
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
            area.rectTransform.localPosition = normalPosition;
            area.SetSize(600f, 70f);

            labelMax = MakeLabel(outline);
            area.Add(labelMax);
            labelMax.rectTransform.localPosition
                = Vector3.zero;

            labelCurrent = MakeLabel(outline);
            area.Add(labelCurrent);
            labelCurrent.rectTransform.localPosition
                = new Vector3(160f, 0f, 0f);

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

            if (Cache.routingFlag.currentlyUsingFlag == true) {
                area.rectTransform.localPosition = routingPosition;
            }
            else {
                area.rectTransform.localPosition = normalPosition;
            }

            area.rectTransform.localScale = Vector2.one;

            labelMax.gameObject.SetActive(true);
            labelCurrent.gameObject.SetActive(!TimeAttack.receivingScore);

            labelMax.SetText($"Max: {FormatFloat(Tracker.max)}");
            labelCurrent.SetText($"Current: {FormatFloat(Tracker.current.magnitude)}");
        }
    }
}
