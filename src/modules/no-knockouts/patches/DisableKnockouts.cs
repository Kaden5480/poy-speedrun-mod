using HarmonyLib;

namespace SpeedrunMod.NoKnockouts.Patches {
    /**
     * <summary>
     * Allows enabling/disabling boulders at runtime.
     * </summary>
     */
    [HarmonyPatch(typeof(FallingEvent), "FellToDeath")]
    public static class DisableKnockouts {
        private static void LogDebug(string message) {
            Plugin.LogDebug($"[{typeof(DisableKnockouts)}] {message}");
        }

        public static bool Prefix(FallingEvent __instance) {
            if (Module.instance.enabled == false) {
                return true;
            }

            if (GameManager.control.permaDeathEnabled == true
                || GameManager.control.freesoloEnabled == true
            ) {
                LogDebug("In yfyd/fs, not bypassing knockout");
                return true;
            }

            LogDebug("Bypassing knockout");

            __instance.HurtSound();
            __instance.falls++;

            GameManager.control.fallTimes++;
            GameManager.control.global_stats_falls++;

            FallingEvent.fallenToDeath = false;

            return false;
        }
    }
}
