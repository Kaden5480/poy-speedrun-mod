namespace SpeedrunMod.Common {
    public abstract class BaseUI : BaseLoggable {
        // Whether the UI is enabled (should be displayed)
        public bool enabled = false;

        /**
         * <summary>
         * Renders the configuration UI.
         * </summary>
         */
        public abstract void Render();
    }
}
