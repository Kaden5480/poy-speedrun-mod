using System.Collections.Generic;

using UnityEngine;

namespace SpeedrunMod.MeshViewer {
    public class TrackedObject {
        private GameObject obj;
        private ObjectTypes objectTypes;
        private List<TrackedCollider> colliders;

        public TrackedObject(GameObject obj) {
            this.obj = obj;
            this.objectTypes = ObjectTypesExt.From(obj);
            this.colliders = new List<TrackedCollider>();

            foreach (Collider collider in obj.GetComponents<Collider>()) {
                this.colliders.Add(new TrackedCollider(collider));
            }
        }
    }
}
