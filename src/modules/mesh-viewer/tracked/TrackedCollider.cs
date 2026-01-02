using UnityEngine;

using SpeedrunMod.Common;

namespace SpeedrunMod.MeshViewer {
    public class TrackedCollider : BaseLoggable {
        private ObjectTypes type;

        public TrackedCollider(Collider collider) {
            switch (collider) {
                case BoxCollider box:
                    type = ObjectTypes.BoxCollider;
                    break;
                case CapsuleCollider capsule:
                    type = ObjectTypes.CapsuleCollider;
                    break;
                case MeshCollider mesh:
                    type = ObjectTypes.MeshCollider;
                    break;
                case SphereCollider sphere:
                    type = ObjectTypes.SphereCollider;
                    break;
                default:
                    LogDebug($"Unexpected collider type: {collider.GetType()}");
                    break;
            }
        }
    }
}
