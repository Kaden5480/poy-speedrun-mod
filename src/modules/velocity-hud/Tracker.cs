using UnityEngine;

namespace SpeedrunMod.VelocityHUD {
    public class Tracker {
        public Vector3 current {
            get => Cache.playerRb.velocity;
        }

        private float _max = 0f;
        public float max {
            get => _max;
        }

        public void Update() {
            if (Cache.playerMove.IsGrounded() == true
                && Cache.timeAttack.isInColliderActivationRange == true
            ) {
                _max = 0f;
            }

            float magnitude = current.magnitude;
            if (magnitude > _max) {
                _max = magnitude;
            }
        }
    }

}
