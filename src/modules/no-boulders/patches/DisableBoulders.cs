using HarmonyLib;

namespace SpeedrunMod.NoBoulders.Patches {
    /**
     * <summary>
     * Allows enabling/disabling boulders at runtime.
     * </summary>
     */
    [HarmonyPatch(typeof(FallingRock), "InitialiseRock")]
    public static class DisableBoulders {
        private static void LogDebug(string message) {
            Plugin.LogDebug($"[{typeof(DisableBoulders)}] {message}");
        }

        public static bool Prefix() {
            if (Module.instance.enabled == true) {
                LogDebug("Bypassing initialising boulders");
                return false;
            }

            return true;
        }
    }
}
