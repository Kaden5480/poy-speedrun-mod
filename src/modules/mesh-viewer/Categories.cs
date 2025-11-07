using System;

using UnityEngine;

namespace SpeedrunMod.MeshViewer {
    [Flags]
    public enum Categories {
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
    }

    public static class CategoriesExt {
        /**
         * <summary>
         * Categorises a given object by the hold type
         * for holds which use Components.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The hold types of this object</returns>
         */
        private static Categories ComponentHoldsFrom(GameObject obj) {
            Categories categories = Categories.None;

            // Bricks
            BrickHold brickHold = obj.GetComponent<BrickHold>();
            if (brickHold != null) {
                categories |= Categories.BrickHold;
                if (brickHold.popoutInstantly == true) {
                    categories |= Categories.InstantBrickHold;
                }
            }

            // Crumbling
            if (obj.GetComponent<CrumblingHold>() != null) {
                categories |= Categories.CrumblingHold;
            }

            // Ice
            if ("Ice".Equals(obj.tag) == true) {
                categories |= Categories.IceHold;

                // Check brittle ice by parent
                if (obj.transform.parent != null) {
                    BrittleIce brittleIce = obj.transform.parent.GetComponent<BrittleIce>();
                    if (brittleIce != null) {
                        categories |= Categories.BrittleIceHold;
                        if (brittleIce.setCustomHp == false) {
                            categories |= Categories.InstantIceHold;
                        }
                    }
                }
            }

            return categories;
        }

        /**
         * <summary>
         * Categorises a given object by the hold type
         * for holds recognised by other features.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The hold types of this object</returns>
         */
        private static Categories HoldsFrom(GameObject obj) {
            Categories categories = Categories.None;

            if ("Crack".Equals(obj.tag) == true) {
                categories |= Categories.CrackHold;
            }
            else if ("ClimbableMicroHold".Equals(obj.tag) == true) {
                categories |= Categories.CrimpHold;
                if ("ExtremeHold".Equals(obj.name) == true) {
                    categories |= Categories.ExtremeCrimpHold;
                }
            }
            else if ("PinchHold".Equals(obj.tag) == true) {
                categories |= Categories.PinchHold;
            }
            else if ("ClimbablePitch".Equals(obj.tag) == true) {
                categories |= Categories.PitchHold;
            }
            else if ("Volume".Equals(obj.tag) == true) {
                categories |= Categories.VolumeHold;
                if ("Volume".Equals(obj.name) == true
                    || obj.name.StartsWith("e_Volume(") == true) {
                    categories |= Categories.OneHandVolumeHold;
                }
            }

            if (obj.name.Contains("ClimbableSloper") == true) {
                categories |= Categories.SloperHold;
            }

            if (categories == Categories.None) {
                if ("Climbable".Equals(obj.tag) == true
                    || "ClimbableRigidbody".Equals(obj.tag) == true
                ) {
                    categories = Categories.RegularHold;
                }
            }

            return categories;
        }

        /**
         * <summary>
         * Categorises a given object by zones.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The zone types of this object</returns>
         */
        private static Categories ZonesFrom(GameObject obj) {
            Categories categories = Categories.None;

            if (obj.layer == LayerMask.NameToLayer("EventTrigger")) {
                categories |= Categories.EventTrigger;
            }
            if (obj.GetComponent<Crux>() != null
                || obj.GetComponent<CruxComplete>() != null
            ) {
                categories |= Categories.CruxZone;
            }
            if (obj.GetComponent<E_LoseToolZone>() != null) {
                categories |= Categories.LoseToolZone;
            }
            if (obj.GetComponent<ResetPosition>() != null) {
                categories |= Categories.ResetZone;
            }
            if (obj.GetComponent<AudioReverbZone>() != null) {
                categories |= Categories.ReverbZone;
            }
            if (obj.GetComponent<ParticleSystemRenderer>() != null) {
                categories |= Categories.ParticleZone;
            }
            if (obj.GetComponent<TimeAttackZone>() != null) {
                categories |= Categories.TimeAttackZone;
            }
            if (obj.GetComponent<PeakWindSolemnTempest>() != null) {
                categories |= Categories.WindForceZone;
            }

            return categories;
        }

        /**
         * <summary>
         * Categorises a given object.
         * </summary>
         * <param name="obj">The object to categorise</param>
         * <returns>The Categories the object is in</returns>
         */
        public static Categories From(GameObject obj) {
            return ComponentHoldsFrom(obj)
                | HoldsFrom(obj)
                | ZonesFrom(obj);
        }
    }
}
