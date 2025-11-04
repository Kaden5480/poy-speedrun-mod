using BepInEx.Configuration;
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

/**
Things
- Peak Boundaries
- Windmill Wings
- Event Triggers
    - Cruxes (start/complete)
    - Reset Box
    - RandomParticlePlay
    - WindSoundTrigger

- AudioReverbZone
- Time Attack
- Wind Sectors
- Player Physics
- Player Triggers

- Box Colliders
- Capsule Colliders
- Mesh Colliders
- Sphere Colliders

Summit:
- Summit Level
- Summit Range
- Stamper Range
- Start Range
*/
