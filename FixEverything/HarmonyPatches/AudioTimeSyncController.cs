using HarmonyLib;

namespace FixEverything.HarmonyPatches
{
    /// <summary>
    /// Fixes an issue where V2 lights won't stop rotating when you pause the game.
    /// </summary>
    [HarmonyPatch(typeof(AudioTimeSyncController), "Update")]
    internal class AudioTimeSyncControllerUpdate
    {
        private static bool Prefix(AudioTimeSyncController.State ____state, ref float ____lastFrameDeltaSongTime)
        {
            if (____state == AudioTimeSyncController.State.Paused)
            {
                ____lastFrameDeltaSongTime = 0f;
                return false;
            }
            return true;
        }
    }
}
