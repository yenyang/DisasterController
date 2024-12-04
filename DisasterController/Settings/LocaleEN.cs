using Colossal;
using System.Collections.Generic;

namespace DisasterController.Settings
{
    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Disaster Controller" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ForestFireToggle)), "Toggles Forest Fires" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BuildingFireToggle)), "Toggles Building Fires" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ForestFireToggle)), "Forest Fires" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BuildingFireToggle)), "Building Fires" },

                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LoseControlAccidentToggle)), "Toggles probability of vehicle accidents causing Fires." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LightningStrikeToggle)), "Toggles probability of Lightning Strikes causing Fires." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LoseControlAccidentToggle)), "Vehicle Accident Fires" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LightningStrikeToggle)), "Lightning Strike Fires" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Version)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Version)), $"Version number for the Disaster Controller mod installed." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetGeneralModSettings)), "Reset Disaster Controller General Settings" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetGeneralModSettings)), "Upon confirmation this will reset the general settings for Disaster Controller mod." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Setting.ResetGeneralModSettings)), "Reset Disaster Controller General Settings?" },
            };
        }

        public void Unload()
        {

        }
    }
}
