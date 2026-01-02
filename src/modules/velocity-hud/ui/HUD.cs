using System;

using UnityEngine;
using UnityEngine.UI;

namespace SpeedrunMod.VelocityHUD {
    public class HUD {
        private Vector3 normalPosition = new Vector3(-50f, -10f, 0f);
        private Vector3 routingPosition = new Vector3(-110f, -10f, 0f);

        private GameObject rootObj;
        private RectTransform rootTransform;
        private TextComponent compMax;
        private TextComponent compCurrent;
        private TextComponent compExtended;

        private Config config;
        private Tracker tracker;

        public HUD(Config config) {
            this.config = config;
            tracker = new Tracker();
        }

        /**
         * <summary>
         * Makes all necessary UI objects.
         * </summary>
         */
        public void Create() {
            TextComponent.offset = 0;

            // Root object
            rootObj = new GameObject("Velocity HUD");
            rootTransform = rootObj.AddComponent<RectTransform>();
            rootTransform.SetParent(Cache.timeAttackUI.obj.transform);
            rootTransform.localPosition = normalPosition;
            rootTransform.sizeDelta = new Vector2(600f, 70f);

            // Current and max objects
            compMax = new TextComponent(rootObj, "Max Velocity");
            compCurrent = new TextComponent(rootObj, "Current Velocity");

            // Extended velocity
            compExtended = new TextComponent(rootObj, "Current Extended");
            RectTransform transform = compExtended.transform;

            Vector2 oldDelta = transform.sizeDelta;
            Vector3 oldPosition = transform.localPosition;

            transform.sizeDelta = new Vector2(
                2 * oldDelta.x,
                oldDelta.y
            );
            transform.localPosition =  new Vector3(
                oldPosition.x + 140,
                oldPosition.y,
                oldPosition.z
            );

            // Hide by default
            rootObj.SetActive(false);
        }

        /**
         * <summary>
         * Destroys UI objects.
         * </summary>
         */
        public void Destroy() {
            GameObject.DestroyImmediate(rootObj);
            rootObj = null;
            TextComponent.offset = 0;
        }

        /**
         * <summary>
         * Formats the velocity into a more user-friendly string.
         * </summary>
         * <param name="velocity">The velocity to format</param>
         * <returns>The formatted velocity</returns>
         */
        private string FormatVelocity(float velocity) {
            return String.Format("{0:0,0.0000}", velocity);
        }

        /**
         * <summary>
         * Whether the UI should be shown.
         * </summary>
         * <returns>Whether the UI should be shown</returns>
         */
        private bool ShouldShow() {
            if (config.showUI.Value == false
                || Cache.timeAttack.watchReady == false
            ) {
                return false;
            }

            return Cache.routingFlag.currentlyUsingFlag == true
                || Cache.timeAttackUI.holdsObj.activeSelf == true;
        }

        /**
         * <summary>
         * Executes each frame to update the state of the UI.
         * </summary>
         */
        public void Update() {
            if (rootObj == null) {
                return;
            }

            rootObj.SetActive(ShouldShow());
            if (ShouldShow() == false) {
                return;
            }

            tracker.Update();

            if (Cache.routingFlag.currentlyUsingFlag == true) {
                rootTransform.localPosition = routingPosition;
            }
            else {
                rootTransform.localPosition = normalPosition;
            }

            rootTransform.localScale = Vector2.one;

            compMax.SetEnabled(true);
            compCurrent.SetEnabled(!TimeAttack.receivingScore);
            compExtended.SetEnabled(!TimeAttack.receivingScore && config.showExtended.Value == true);

            compMax.SetText($"Max: {FormatVelocity(tracker.max)}");
            compCurrent.SetText($"Current: {FormatVelocity(tracker.current.magnitude)}");

            if (config.showExtended.Value == true) {
                string x = FormatVelocity(tracker.current.x);
                string y = FormatVelocity(tracker.current.y);
                string z = FormatVelocity(tracker.current.z);
                compExtended.SetText($"Extended: ({x}, {y}, {z})");
            }
        }
    }
}
