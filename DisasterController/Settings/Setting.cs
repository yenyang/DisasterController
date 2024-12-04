using Colossal.IO.AssetDatabase;
using DisasterController.Systems;
using Game.Modding;
using Game.Prefabs;
using Game.Settings;
using Unity.Entities;
using static Game.Prefabs.CompositionFlags;

namespace DisasterController.Settings
{
    [FileLocation(nameof(DisasterController))]
    [SettingsUIGroupOrder(ToggleGroup, AboutGroup)]
    public class Setting : ModSetting
    {
        public const string ForestFireTab = "Forest Fire";

        public const string ToggleGroup = "Toggle";
        public const string SliderGroup = "Slider";
        public const string AboutGroup = "About";
        public const string ResetGroup = "Reset";

        public Setting(IMod mod) : base(mod)
        {
            SetDefaults();
        }

        [SettingsUISection(ToggleGroup)]
        [SettingsUISetter(typeof(Setting), nameof(ToggleForestFire))]
        public bool ForestFireToggle { get; set; }

        [SettingsUISection(ToggleGroup)]
        [SettingsUISetter(typeof(Setting), nameof(ToggleBuildingFire))]
        public bool BuildingFireToggle { get; set; }

        [SettingsUISection(ToggleGroup)]
        [SettingsUISetter(typeof(Setting), nameof(ToggleLoseControlAccident))]
        public bool LoseControlAccidentToggle { get; set; }


        [SettingsUISection(ToggleGroup)]
        [SettingsUISetter(typeof(Setting), nameof(ToggleLightningStrike))]
        public bool LightningStrikeToggle { get; set; }

        /// <summary>
        /// Sets a value indicating whether: a button for Resetting the general mod settings.
        /// </summary>
        [SettingsUIButton]
        [SettingsUIConfirmation]
        [SettingsUISection(ResetGroup)]
        public bool ResetGeneralModSettings
        {
            set
            {
                SetDefaults();
                ApplyAndSave();
            }
        }

        /// <summary>
        /// Gets a value indicating the version.
        /// </summary>
        [SettingsUISection(AboutGroup)]
        public string Version => Mod.Version;

        // public bool RobberyToggle { get; set; }

        // public bool LoseControlAccidentToggle { get; set; }

        /*

        [SettingsUISlider(min = 0f, max = 2f, step = 0.1f, unit = "floatSingleFraction", scalarMultiplier = 100f)]
        public float ForestFireStartProbability { get; set; }

        [SettingsUISlider(min = 0f, max = 2f, step = 0.1f, unit = "floatSingleFraction")]
        public float ForestFireStartIntensity { get; set; }

        [SettingsUISlider(min = 0f, max = 5f, step = 0.1f, unit = "floatSingleFraction")]
        public float ForestFireEscalationRate { get; set; }

        [SettingsUISlider(min = 0f, max = 10f, step = 0.1f, unit = "floatSingleFraction")]
        public float ForestFireSpreadProbability { get; set; }

        [SettingsUISlider(min = 0f, max = 50f, step = 5f, unit = "floatSingleFraction")]
        public float ForestFireSpreadRange { get; set; } */



        public override void SetDefaults()
        {
            ForestFireToggle = false;
            BuildingFireToggle = false;
            LoseControlAccidentToggle = false;
            LightningStrikeToggle = false;
            // RobberyToggle = true;
            // LoseControlAccidentToggle = true;
        }


        /// <summary>
        /// Toggles the probability of forest fires.
        /// </summary>
        /// <param name="state">Should forest fires occur or not.</param>
        public void ToggleForestFire(bool state)
        {
            ModifyEventPrefabsSystem modifyEventPrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<ModifyEventPrefabsSystem>();
            ToggleFireProbability(modifyEventPrefabSystem.ForestFirePrefabID, state);
        }

        /// <summary>
        /// Toggles the probability of building fires.
        /// </summary>
        /// <param name="state">Should building fires occur or not.</param>
        public void ToggleBuildingFire(bool state)
        {
            ModifyEventPrefabsSystem modifyEventPrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<ModifyEventPrefabsSystem>();
            ToggleFireProbability(modifyEventPrefabSystem.BuildingFirePrefabID, state);
        }

        /// <summary>
        /// Toggles the fire probability of car accidents.
        /// </summary>
        /// <param name="state">Should forest fires occur or not.</param>
        public void ToggleLoseControlAccident(bool state)
        {
            ModifyEventPrefabsSystem modifyEventPrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<ModifyEventPrefabsSystem>();
            ToggleFireProbability(modifyEventPrefabSystem.LoseControlAccidentPrefabID, state);
        }

        /// <summary>
        /// Toggles the fire probablity of lightning strikes.
        /// </summary>
        /// <param name="state">Should forest fires occur or not.</param>
        public void ToggleLightningStrike(bool state)
        {
            ModifyEventPrefabsSystem modifyEventPrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<ModifyEventPrefabsSystem>();
            ToggleFireProbability(modifyEventPrefabSystem.LightningStrikePrefabID, state);
        }

        public void ToggleFireProbability(PrefabID prefabID, bool state)
        {
            ModifyEventPrefabsSystem modifyEventPrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<ModifyEventPrefabsSystem>();
            if (!state)
            {
                modifyEventPrefabSystem.SetFireProbabilityToZero(prefabID);
            }
            else
            {
                modifyEventPrefabSystem.ResetFireProbability(prefabID);
            }
        }
    }

    
}
