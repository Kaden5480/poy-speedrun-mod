using BepInEx.Configuration;
using SpeedrunMod.Common;

namespace SpeedrunMod.NoBoulders {
    public class Module : BaseModule {
        public static Module instance;

        /**
         * <summary>
         * Initializes this module.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         */
        public Module(ConfigFile configFile) : base(configFile, "No Boulders") {
            instance = this;

            // Apply early patches
            Patch(new[] {
                typeof(Patches.DisableBoulders),
            });
        }
    }
}
