namespace SpeedrunMod {
    /**
     * <summary>
     * Caches the time attack UI.
     * </summary>
     */
    public class TimeAttackUI {
        public GameObject obj  { get; private set; }
        public Font font       { get; private set; }
        public Outline outline { get; private set; }
        public Text text       { get; private set; }

        public GameObject holdsObj { get; private set; }

        /**
         * <summary>
         * Caches objects in the current scene.
         * </summary>
         */
        public TimeAttackUI() {
            obj = GameObject.Find("TimeAttackText");
            if (obj == null) {
                return;
            }

            Transform holdsTransform = obj.transform.Find("holds_image");
            if (holdsTransform != null) {
                holdsObj = holdsTransform.gameObject;
            }

            text = obj.GetComponent<Text>();
            outline = obj.GetComponent<Outline>();

            if (text != null) {
                font = text.font;
            }
        }
    }
}
