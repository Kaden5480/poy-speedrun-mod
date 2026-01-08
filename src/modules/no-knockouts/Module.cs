using BepInEx.Configuration;
using HarmonyLib;

namespace SpeedrunMod.Modules.NoKnockouts {
    /**
     * <summary>
     * Patches the knockout animation out in normal mode.
     * </summary>
     */
    [HarmonyPatch(typeof(FallingEvent), "FellToDeath")]
    internal static class DisableKnockouts {
        private static bool Prefix(FallingEvent __instance) {
            if (Config.enabled.Value == false) {
                return true;
            }

            if (GameManager.control.permaDeathEnabled || GameManager.control.freesoloEnabled) {
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

    /**
     * <summary>
     * The main module for No Knockouts.
     * </summary>
     */
    internal static class Module {
        /**
         * <summary>
         * The name of this module.
         * </summary>
         */
        internal const string name = "No Knockouts";

        /**
         * <summary>
         * Initializes this module, binding its config to the
         * provided config file.
         * </summary>
         * <param name="configFile">The config file to bind to</param>
         */
        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);
            Patcher.Patch<DisableKnockouts>();
        }
    }
}
