using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpeedrunMod {
    /**
     * <summary>
     * Caches useful objects on scene loads.
     * </summary>
     */
    internal static class Cache {
        internal static Scene scene        { get; private set; }
        internal static bool isCustomLevel { get; private set; }

        internal static bool artefactsCollected { get; private set; }
        internal static bool ropesCollected     { get; private set; }

        internal static Footplacement footPlacement { get; private set; }
        internal static Inventory inventory         { get; private set; }
        internal static PlayerMove playerMove       { get; private set; }
        internal static Rigidbody playerRb          { get; private set; }
        internal static RoutingFlag routingFlag     { get; private set; }
        internal static TimeAttack timeAttack       { get; private set; }
        internal static StemFoot stemFoot           { get; private set; }

        internal static GameObject timeAttackUI      { get; private set; }
        internal static GameObject timeAttackHoldsUI { get; private set; }

        /**
         * <summary>
         * Finds objects on scene loads.
         * </summary>
         */
        internal static void FindObjects(Scene scene) {
            Cache.scene = scene;
            isCustomLevel = scene.buildIndex == 69;

            foreach (ArtefactOnPeak artefact in GameObject.FindObjectsOfType<ArtefactOnPeak>()) {
                if (artefact.gameObject.activeInHierarchy == true) {
                    artefactsCollected = false;
                    break;
                }
            }

            foreach (RopeCollectable rope in GameObject.FindObjectsOfType<RopeCollectable>()) {
                if (rope.gameObject.activeInHierarchy == true) {
                    ropesCollected = false;
                    break;
                }
            }

            footPlacement = GameObject.FindObjectOfType<Footplacement>();
            inventory = GameObject.FindObjectOfType<Inventory>();

            playerMove = GameObject.FindObjectOfType<PlayerMove>();
            if (playerMove != null) {
                playerRb = playerMove.rigid;
            }

            routingFlag = GameObject.FindObjectOfType<RoutingFlag>();
            stemFoot = GameObject.FindObjectOfType<StemFoot>();
            timeAttack = GameObject.FindObjectOfType<TimeAttack>();

            timeAttackUI = GameObject.Find("TimeAttackText");
            if (timeAttackUI != null) {
                Transform t = timeAttackUI.transform.Find("holds_image");
                if (t != null) {
                    timeAttackHoldsUI = t.gameObject;
                }
            }
        }

        /**
         * <summary>
         * Clears the cache on scene unloads.
         * </summary>
         */
        internal static void Clear() {
            isCustomLevel = false;

            artefactsCollected = true;
            ropesCollected = true;

            footPlacement = null;
            inventory = null;
            playerMove = null;
            playerRb = null;
            routingFlag = null;
            stemFoot = null;

            timeAttackUI = null;
            timeAttackHoldsUI = null;
        }
    }
}
