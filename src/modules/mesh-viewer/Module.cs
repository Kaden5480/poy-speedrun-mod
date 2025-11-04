using SpeedrunMod.Common;

namespace SpeedrunMod.MeshViewer {
    public class Module : BaseModule {
        /**
         * <summary>
         * Initializes this module.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         */
        public Module(ConfigFile configFile) : base(configFile, "Mesh Viewer") {
        }
    }
}
