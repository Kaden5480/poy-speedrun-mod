using UnityEngine;

namespace SpeedrunMod {
    /**
     * <summary>
     * Caches important common objects in the currently
     * loaded scene.
     * </summary>
     */
    public static class Cache {
        public static LevelEditorManager levelEditorManager { get; private set; }
        public static PlayerMove playerMove                 { get; private set; }
        public static Rigidbody playerRb                    { get; private set; }
        public static RoutingFlag routingFlag               { get; private set; }
        public static TimeAttack timeAttack                 { get; private set; }

        // Internal categories
        public static TimeAttackUI timeAttackUI { get; private set; }

        /**
         * <summary>
         * Caches objects in the current scene.
         * </summary>
         */
        public static void FindObjects() {
            // Try using LevelEditorManager to get PlayerMove
            // as it would be inactive in a custom level normally
            levelEditorManager = GameObject.FindObjectOfType<LevelEditorManager>();
            if (levelEditorManager != null) {
                playerMove = levelEditorManager.playerMove;
            }
            else {
                playerMove = GameObject.FindObjectOfType<PlayerMove>();
            }

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
            levelEditorManager = null;
            playerMove = null;
            playerRb = null;
            routingFlag = null;
            timeAttack = null;
            timeAttackUI = null;
        }
    }
}
