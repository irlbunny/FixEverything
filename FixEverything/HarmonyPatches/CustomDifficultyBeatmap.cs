using HarmonyLib;
using System.Threading.Tasks;

namespace FixEverything.HarmonyPatches
{
    [HarmonyPatch(typeof(CustomDifficultyBeatmap), nameof(CustomDifficultyBeatmap.GetBeatmapDataAsync))]
    internal class CustomDifficultyBeatmapGetBeatmapDataAsync
    {
        private static bool Prefix(
            CustomDifficultyBeatmap __instance,
            EnvironmentInfoSO environmentInfo,
            PlayerSpecificSettings playerSpecificSettings,
            ref Task<IReadonlyBeatmapData> __result)
        {
            IReadonlyBeatmapData beatmapData = null;
            Task beatmapDataTask = Task.Run(delegate ()
            {
                var containsRotationEvents = __instance.parentDifficultyBeatmapSet.beatmapCharacteristic.containsRotationEvents;
                var beatmapEnvironmentInfo = !containsRotationEvents ? __instance.level.environmentInfo : __instance.level.allDirectionsEnvironmentInfo;
                var isBeatmapEnvironmentV2 = beatmapEnvironmentInfo.lightGroups.lightGroupSOList.Count == 0;
                var loadingForDesignatedEnvironment = isBeatmapEnvironmentV2 ? true : beatmapEnvironmentInfo.serializedName == environmentInfo.serializedName;
                beatmapData = BeatmapDataLoader.GetBeatmapDataFromSaveData(
                    __instance.beatmapSaveData,
                    __instance.difficulty,
                    __instance.beatsPerMinute,
                    loadingForDesignatedEnvironment,
                    environmentInfo,
                    playerSpecificSettings);
            });
            beatmapDataTask.Wait();
            __result = Task.FromResult(beatmapData);
            return false;
        }
    }
}
