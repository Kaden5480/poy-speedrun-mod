using UnityEngine;

namespace SpeedrunMod.Modules.VelocityHUD {
    internal static class Tracker {
        internal static Vector3 current {
            get => Cache.playerRb.velocity;
        }
        internal static float max { get; private set; } = 0f;

        internal static void Update() {
            if (Config.enabled.Value == false) {
                return;
            }

            if (Cache.playerMove.IsGrounded() == true
                && Cache.timeAttack.isInColliderActivationRange == true
            ) {
                max = 0f;
            }

            max = Mathf.Max(
                max,
                Cache.playerRb.velocity.magnitude
            );
        }
    }
}
