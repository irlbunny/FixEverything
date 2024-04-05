using HarmonyLib;

namespace FixEverything.HarmonyPatches
{
    /// <summary>
    /// Fixes an issue where the game is injecting default basic lighting events even though basic lighting events already exist in the beatmap.
    /// </summary>
    [HarmonyPatch(typeof(DefaultEnvironmentEventsFactory), "InsertDefaultEvents")]
    internal class DefaultEnvironmentEventsFactoryInsertDefaultEvents
    {
        private static bool Prefix(BeatmapData beatmapData)
        {
            if (beatmapData.GetBeatmapDataItemsCount<BasicBeatmapEventData>(0) == 0)
            {
                beatmapData.InsertBeatmapEventData(new BasicBeatmapEventData(0f, BasicBeatmapEventType.Event0, 1, 1f));
                beatmapData.InsertBeatmapEventData(new BasicBeatmapEventData(0f, BasicBeatmapEventType.Event4, 1, 1f));
            }
            return false;
        }
    }
}
