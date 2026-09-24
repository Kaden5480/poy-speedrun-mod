using HarmonyLib;

namespace SpeedrunMod.Modules.Misc.Patches {
    /**
     * <summary>
     * Patches the knockout animation out in normal mode.
     * </summary>
     */
    [HarmonyPatch(typeof(FallingEvent), "FellToDeath")]
    internal static class DisableKnockouts {
        private static bool Prefix(FallingEvent __instance) {
            if (Config.noKnockouts.Value == false) {
                return true;
            }

            if (GameManager.control.permaDeathEnabled == true
                || GameManager.control.freesoloEnabled == true
            ) {
                return true;
            }

            __instance.HurtSound();

            __instance.falls++;
            GameManager.control.fallTimes++;
            GameManager.control.global_stats_falls++;

            FallingEvent.fallenToDeath = false;

            return false;
        }
    }
}
