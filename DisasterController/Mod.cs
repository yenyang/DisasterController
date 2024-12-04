using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using DisasterController.Settings;
using DisasterController.Systems;
using Game;
using Game.Modding;
using Game.SceneFlow;
using System.Reflection;
using Unity.Entities;

namespace DisasterController
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(DisasterController)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public static Setting m_Setting;

        /// <summary>
        /// Gets the version of the mod.
        /// </summary>
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            m_Setting = new Setting(this);
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));


            AssetDatabase.global.LoadSettings(nameof(DisasterController), m_Setting, new Setting(this));

            World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<ModifyEventPrefabsSystem>();
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
