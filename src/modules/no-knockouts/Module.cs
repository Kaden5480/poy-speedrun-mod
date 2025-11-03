using BepInEx.Configuration;
using SpeedrunMod.Common;

namespace SpeedrunMod.NoKnockouts {
    public class Module : BaseModule {
        public static Module instance;

        /**
         * <summary>
         * Initializes this module.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         */
        public Module(ConfigFile config) : base(config, "No Boulders") {
            instance = this;

            // Apply early patches
            PatchEarly(new[] {
                typeof(Patches.DisableKnockouts),
            });
        }
    }
}
