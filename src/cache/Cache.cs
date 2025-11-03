using UnityEngine;

namespace SpeedrunMod {
    /**
     * <summary>
     * Caches important common objects in the currently
     * loaded scene.
     * </summary>
     */
    public static class Cache {
        public static PlayerMove playerMove   { get; private set; }
        public static Rigidbody playerRb      { get; private set; }
        public static RoutingFlag routingFlag { get; private set; }
        public static TimeAttack timeAttack   { get; private set; }

        // Internal categories
        public static TimeAttackUI timeAttackUI { get; private set; }

        /**
         * <summary>
         * Caches objects in the current scene.
         * </summary>
         */
        public static void FindObjects() {
            playerMove = GameObject.FindObjectOfType<PlayerMove>();
            if (playerMove != null) {
                playerRb = playerMove.rigid;
            }

            routingFlag = GameObject.FindObjectOfType<RoutingFlag>();
            timeAttack = GameObject.FindObjectOfType<TimeAttack>();

            timeAttackUI = new TimeAttackUI();
        }

        /**
         * <summary>
         * Wipes the cache.
         * </summary>
         */
        public static void Clear() {
            playerMove = null;
            playerRb = null;
            routingFlag = null;
            timeAttack = null;
            timeAttackUI = null;
        }
    }
}
