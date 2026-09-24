using BepInEx.Configuration;
using HarmonyLib;

namespace SpeedrunMod.Modules.Misc {
    /**
     * <summary>
     * The misc module.
     * </summary>
     */
    internal static class Module {
        internal const string name = "Misc";

        internal static void Init(ConfigFile configFile) {
            Config.Init(configFile);
            Patcher.Patch(typeof(Patches.DisableBoulders));
            Patcher.Patch(typeof(Patches.DisableKnockouts));
            Patcher.Patch(typeof(Patches.TenPointsEverywhere));
        }

        internal static void SceneLoad() {
            Patches.TenPointsEverywhere.SceneLoad();
        }
    }
}
