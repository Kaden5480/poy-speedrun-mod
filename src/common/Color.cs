using System;
using UEColor = UnityEngine.Color;

namespace SpeedrunMod.Common {
    public static class Color {
        /**
         * <summary>
         * Function for helping with hsl conversions.
         *
         * See:
         * https://en.wikipedia.org/wiki/HSL_and_HSV#Color_conversion_formulae
         * </summary>
         */
        private static float HSLF(float h, float s, float l, int n) {
            float a = s * Math.Min(l, 1 - l);
            float k = (n + h/30) % 12;

            return l - a * Math.Max(-1,
                Math.Min(Math.Min(k-3, 9-k), 1)
            );
        }

        /**
         * <summary>
         * Creates a UnityEngine.Color from hue, saturation, lightness, and alpha.
         * </summary>
         * <param name="h">A hue between 0-360</param>
         * <param name="s">A saturation between 0-1</param>
         * <param name="l">A lightness between 0-1</param>
         * <param name="a">An alpha between 0-1</param>
         * <returns>The hsl values as a Color</returns>
         */
        public static UEColor FromHSLA(float h, float s, float l, float a) {
            float r = HSLF(h, s, l, 0);
            float g = HSLF(h, s, l, 8);
            float b = HSLF(h, s, l, 4);

            return new UEColor(r, g, b, a);
        }

        /**
         * <summary>
         * Creates a UnityEngine.Color from hue, saturation, and lightness.
         * </summary>
         * <param name="h">A hue between 0-360</param>
         * <param name="s">A saturation between 0-1</param>
         * <param name="l">A lightness between 0-1</param>
         * <returns>The hsl values as a Color</returns>
         */
        public static UEColor FromHSL(float h, float s, float l) {
            return FromHSLA(h, s, l, 1.0f);
        }
    }
}
