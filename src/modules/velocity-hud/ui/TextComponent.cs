using UnityEngine;
using UnityEngine.UI;

namespace SpeedrunMod.VelocityHUD {
    public class TextComponent {
        public static float offset = 0f;
        private const int fontSize = 25;
        private const float offsetIncrement = 160f;

        private GameObject obj;
        public RectTransform transform { get; private set; }
        private Text text;

        /**
         * <summary>
         * Constructs a new TextComponent.
         * </summary>
         * <param name="parent">The parent object to use</param>
         * <param name="name">The name of this object</param>
         */
        public TextComponent(GameObject parent, string name) {
            obj = new GameObject(name);

            transform = obj.AddComponent<RectTransform>();
            transform.SetParent(parent.transform);
            transform.localPosition = new Vector3(offset, 0f, 0f);
            offset += offsetIncrement;
            transform.sizeDelta = new Vector2(200f, 70f);

            text = obj.AddComponent<Text>();
            text.font = Cache.timeAttackUI.font;
            text.fontSize = fontSize;

            Outline outline = obj.AddComponent<Outline>();
            outline.effectColor = Cache.timeAttackUI.outline.effectColor;
            outline.effectDistance = Cache.timeAttackUI.outline.effectDistance;

        }

        /**
         * <summary>
         * Changes whether this component is enabled/disabled.
         * </summary>
         * <param name="enabled">Whether to enable/disable this component</param>
         */
        public void SetEnabled(bool enabled) {
            obj.SetActive(enabled);
        }

        /**
         * <summary>
         * Allows changing the text this component displays.
         * </summary>
         * <param name="text">The text to set</param>
         */
        public void SetText(string text) {
            this.text.text = text;
        }
    }
}
