using System;

using UnityEngine;

namespace SpeedrunMod.MeshViewer {
    [Flags]
    public enum ObjectTypes {
        None = 1 << 0,

        // Holds
        // Components
        InstantBrickHold = 1 << 1,
        BrickHold        = 1 << 2,
        CrumblingHold    = 1 << 3,
        InstantIceHold   = 1 << 4,
        BrittleIceHold   = 1 << 5,
        IceHold          = 1 << 6,

        CrackHold         = 1 << 7,
        ExtremeCrimpHold  = 1 << 8,
        CrimpHold         = 1 << 9,
        PinchHold         = 1 << 10,
        PitchHold         = 1 << 11,
        SloperHold        = 1 << 12,
        OneHandVolumeHold = 1 << 13,
        VolumeHold        = 1 << 14,
        RegularHold       = 1 << 15,

        // Zones
        CruxZone          = 1 << 16,
        LoseToolZone      = 1 << 17,
        ResetZone         = 1 << 18,
        ReverbZone        = 1 << 19,
        ParticleZone      = 1 << 20,
        TimeAttackZone    = 1 << 21,
        // This could be a pain
        //WindAmbienceZone = 1 << 22,
        WindForceZone     = 1 << 23,
        EventTrigger      = 1 << 24,

        // Other stuff
        PeakBoundary  = 1 << 25,
        PlayerPhysics = 1 << 26,
        PlayerTrigger = 1 << 27,
        WindMillWings = 1 << 28,

        // Colliders
        BoxCollider     = 1 << 29,
        CapsuleCollider = 1 << 30,
        MeshCollider    = 1 << 31,
        SphereCollider  = 1 << 32,
    }

    public static class ObjectTypesExt {
        /**
         * <summary>
         * Categorises a given object by the hold type
         * for holds which use Components.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The hold types of this object</returns>
         */
        private static ObjectTypes ComponentHoldsFrom(GameObject obj) {
            ObjectTypes objectTypes = ObjectTypes.None;

            // Bricks
            BrickHold brickHold = obj.GetComponent<BrickHold>();
            if (brickHold != null) {
                objectTypes |= ObjectTypes.BrickHold;
                if (brickHold.popoutInstantly == true) {
                    objectTypes |= ObjectTypes.InstantBrickHold;
                }
            }

            // Crumbling
            if (obj.GetComponent<CrumblingHold>() != null) {
                objectTypes |= ObjectTypes.CrumblingHold;
            }

            // Ice
            if ("Ice".Equals(obj.tag) == true) {
                objectTypes |= ObjectTypes.IceHold;

                // Check brittle ice by parent
                if (obj.transform.parent != null) {
                    BrittleIce brittleIce = obj.transform.parent.GetComponent<BrittleIce>();
                    if (brittleIce != null) {
                        objectTypes |= ObjectTypes.BrittleIceHold;
                        if (brittleIce.setCustomHp == false) {
                            objectTypes |= ObjectTypes.InstantIceHold;
                        }
                    }
                }
            }

            return objectTypes;
        }

        /**
         * <summary>
         * Categorises a given object by the hold type
         * for holds recognised by other features.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The hold types of this object</returns>
         */
        private static ObjectTypes HoldsFrom(GameObject obj) {
            ObjectTypes objectTypes = ObjectTypes.None;

            if ("Crack".Equals(obj.tag) == true) {
                objectTypes |= ObjectTypes.CrackHold;
            }
            else if ("ClimbableMicroHold".Equals(obj.tag) == true) {
                objectTypes |= ObjectTypes.CrimpHold;
                if ("ExtremeHold".Equals(obj.name) == true) {
                    objectTypes |= ObjectTypes.ExtremeCrimpHold;
                }
            }
            else if ("PinchHold".Equals(obj.tag) == true) {
                objectTypes |= ObjectTypes.PinchHold;
            }
            else if ("ClimbablePitch".Equals(obj.tag) == true) {
                objectTypes |= ObjectTypes.PitchHold;
            }
            else if ("Volume".Equals(obj.tag) == true) {
                objectTypes |= ObjectTypes.VolumeHold;
                if ("Volume".Equals(obj.name) == true
                    || obj.name.StartsWith("e_Volume(") == true) {
                    objectTypes |= ObjectTypes.OneHandVolumeHold;
                }
            }

            if (obj.name.Contains("ClimbableSloper") == true) {
                objectTypes |= ObjectTypes.SloperHold;
            }

            if (objectTypes == ObjectTypes.None) {
                if ("Climbable".Equals(obj.tag) == true
                    || "ClimbableRigidbody".Equals(obj.tag) == true
                ) {
                    objectTypes = ObjectTypes.RegularHold;
                }
            }

            return objectTypes;
        }

        /**
         * <summary>
         * Categorises a given object by zones.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The zone types of this object</returns>
         */
        private static ObjectTypes ZonesFrom(GameObject obj) {
            ObjectTypes objectTypes = ObjectTypes.None;

            if (obj.layer == LayerMask.NameToLayer("EventTrigger")) {
                objectTypes |= ObjectTypes.EventTrigger;
            }
            if (obj.GetComponent<Crux>() != null
                || obj.GetComponent<CruxComplete>() != null
            ) {
                objectTypes |= ObjectTypes.CruxZone;
            }
            if (obj.GetComponent<E_LoseToolZone>() != null) {
                objectTypes |= ObjectTypes.LoseToolZone;
            }
            if (obj.GetComponent<ResetPosition>() != null) {
                objectTypes |= ObjectTypes.ResetZone;
            }
            if (obj.GetComponent<AudioReverbZone>() != null) {
                objectTypes |= ObjectTypes.ReverbZone;
            }
            if (obj.GetComponent<ParticleSystemRenderer>() != null) {
                objectTypes |= ObjectTypes.ParticleZone;
            }
            if (obj.GetComponent<TimeAttackZone>() != null) {
                objectTypes |= ObjectTypes.TimeAttackZone;
            }
            if (obj.GetComponent<PeakWindSolemnTempest>() != null) {
                objectTypes |= ObjectTypes.WindForceZone;
            }

            return objectTypes;
        }

        /**
         * <summary>
         * Categorises a given object.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The ObjectTypes of the object</returns>
         */
        public static ObjectTypes From(GameObject obj) {
            return ComponentHoldsFrom(obj)
                | HoldsFrom(obj)
                | ZonesFrom(obj);
        }
    }
}
