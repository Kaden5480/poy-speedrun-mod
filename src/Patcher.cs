namespace SpeedrunMod {
    /**
     * <summary>
     * A class for applying patches and logging patching.
     * </summary>
     */
    internal static class Patcher {
        private static Logger logger = new Logger(typeof(Patcher));

        /**
         * <summary>
         * Applies a patch of type `T` and logs.
         * </summary>
         */
        internal static void Apply<T>() {
            Harmony.CreateAndPatchAll(typeof(T));
            logger.LogDebug($"Applied patch: {T}");
        }
    }
}
