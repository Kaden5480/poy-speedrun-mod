using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;

namespace SpeedrunMod.Modules.Misc.Patches {
    /**
     * <summary>
     * Permits using 10 point crampons on every builtin level.
     * </summary>
     */
    internal static class TenPointsEverywhere {
        private static bool boulderingInject = false;

        /**
         * <summary>
         * Whether this patch can be used.
         * </summary>
         */
        private static bool CanUse() {
            if (Config.tenPointsEverywhere.Value == false) {
                return false;
            }

            if (Cache.isCustomLevel == true) {
                return false;
            }

            if (GameManager.control.permaDeathEnabled == true
                || GameManager.control.freesoloEnabled == true
            ) {
                return false;
            }

            if (Cache.artefactsCollected == false
                || Cache.ropesCollected == false
            ) {
                return false;
            }

            return true;
        }

        /**
         * <summary>
         * Determines whether this patch should apply.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoutingFlag), "CheckPeaksCompleted")]
        internal static void ApplyPatch() {
            // Sensible default states
            boulderingInject = false;

            if (Cache.inventory != null) {
                boulderingInject = Cache.inventory.isBouldering;
            }

            // Check whether to enable this patch
            if (CanUse() == false) {
                return;
            }

            // Enable the patch
            boulderingInject = false;

            if (Cache.stemFoot != null) {
                Cache.stemFoot.isAlpDam = false;
                Cache.stemFoot.limitCrampon6Point = false;
            }

            if (Cache.footPlacement != null) {
                Cache.footPlacement.isAlpDam = false;
            }
        }

        /**
         * <summary>
         * Prevents stamping when this patch is enabled, because it
         * could be used to cheat.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoutingFlag), "Update")]
        private static void DisableStamper(StamperPeakSummit ___summitbox) {
            if (CanUse() == true) {
                ___summitbox.gameObject.SetActive(false);
            }
        }

        /**
         * <summary>
         * Injects a custom field in place of Inventory.isBouldering to configure
         * only the crampons.
         *
         * Modifying Inventory.isBouldering directly changes whether ropes and chalk
         * can be used on a peak, which is undesirable.
         *
         * That's why this field is injected in place instead.
         * </summary>
         */
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Footplacement), "ShowLegsTorsoAndItems")]
        [HarmonyPatch(typeof(StemFoot), "Update")]
        private static IEnumerable<CodeInstruction> BoulderingInjectMethod(
            IEnumerable<CodeInstruction> insts
        ) {
            FieldInfo isBoulderingInfo = AccessTools.Field(
                typeof(Inventory), nameof(Inventory.isBouldering)
            );

            FieldInfo boulderingInjectInfo = AccessTools.Field(
                typeof(TenPointsEverywhere), nameof(boulderingInject)
            );

            return Helper.Replace(insts,
                new[] {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, null),
                    new CodeInstruction(OpCodes.Ldfld, isBoulderingInfo),
                },
                new[] {
                    new CodeInstruction(OpCodes.Ldsfld, boulderingInjectInfo),
                }
            );
        }
    }
}
