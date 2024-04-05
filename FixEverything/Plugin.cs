using HarmonyLib;
using IPA;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace FixEverything
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal const string HARMONYID = "com.github.ItsKaitlyn03.FixEverything";
        internal static Harmony HarmonyInstance { get; private set; } = new(HARMONYID);
        internal static IPALogger Log { get; private set; }

        [Init]
        public Plugin(IPALogger logger)
        {
            Log = logger;
        }

        [OnEnable]
        public void OnEnable()
        {
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void OnDisable()
        {
            HarmonyInstance.UnpatchSelf();
        }
    }
}
