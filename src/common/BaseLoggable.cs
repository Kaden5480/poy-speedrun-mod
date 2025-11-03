namespace SpeedrunMod.Common {
    /**
     * <summary>
     * A class for helping with creating
     * more consistent and informational logging.
     * </summary>
     */
    public abstract class BaseLoggable {
        protected virtual void LogDebug(string message) {
            Plugin.LogDebug($"[{GetType()}] {message}");
        }

        protected virtual void LogInfo(string message) {
            Plugin.LogInfo($"[{GetType()}] {message}");
        }

        protected virtual void LogError(string message) {
            Plugin.LogError($"[{GetType()}] {message}");
        }
    }
}
