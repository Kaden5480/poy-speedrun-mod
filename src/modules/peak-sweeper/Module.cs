using BepInEx.Configuration;
using UnityEngine.SceneManagement;

using SpeedrunMod.Common;

namespace SpeedrunMod.PeakSweeper {
    public class Module : BaseModule {
        /**
         * <summary>
         * Initializes this module.
         * </summary>
         * <param name="configFile">The config file to bind configs to</param>
         */
        public Module(ConfigFile config) : base(config, "Peak Sweeper") {
        }

        /**
         * <summary>
         * Executes on each scene load.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         */
        public override void OnSceneLoad(Scene scene) {
            if (enabled == false) {
                return;
            }

            LogDebug($"Scene loaded: {scene.name}");
        }
    }
}
