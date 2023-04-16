using HarmonyLib;

namespace FixEverything.HarmonyPatches
{
    [HarmonyPatch(typeof(AudioTimeSyncController), nameof(AudioTimeSyncController.Update))]
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
