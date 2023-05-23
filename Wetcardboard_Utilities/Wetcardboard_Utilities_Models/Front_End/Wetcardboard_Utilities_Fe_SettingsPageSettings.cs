using Wetcardboard_Utilities_General.Constants;
using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Models.Front_End
{
    public class Wetcardboard_Utilities_Fe_SettingsPageSettings
    {
        #region Fields & Properties
        #region Properties
        private Dictionary<string, Wetcardboard_Utilities_UserSettings> SettingsDict { get; set; }
        private Dictionary<string, Wetcardboard_Utilities_UserSettings> OrgSettingsDict { get; set; }

        #region Localization Country
        public string? LocalizationCountry
        {
            get
            {
                return GetSettingValue(SettingsDict, UserSettingsConstants_Wetcardboard_Utilities.LOCALIZATION_COUNTRY);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                var sett = new Wetcardboard_Utilities_UserSettings
                {
                    SettingName = UserSettingsConstants_Wetcardboard_Utilities.LOCALIZATION_COUNTRY,
                    SettingValue = value
                };

                SetSettingValue(SettingsDict, UserSettingsConstants_Wetcardboard_Utilities.LOCALIZATION_COUNTRY, sett);
            }
        }
        public string? OrgLocalizationCountry
        {
            get
            {
                return GetSettingValue(OrgSettingsDict, UserSettingsConstants_Wetcardboard_Utilities.LOCALIZATION_COUNTRY);
            }
        }
        #endregion \ Localization Country
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Wetcardboard_Utilities_Fe_SettingsPageSettings()
        {
            SettingsDict = new Dictionary<string, Wetcardboard_Utilities_UserSettings>();
            OrgSettingsDict = new Dictionary<string, Wetcardboard_Utilities_UserSettings>();
        }
        #endregion \ Constructor


        #region Methods
        #region Private Methods
        private bool CheckStrings(string? a, string? b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
        private Wetcardboard_Utilities_UserSettings? GetSettingObj(Dictionary<string, Wetcardboard_Utilities_UserSettings> dict, string name)
        {
            if (dict is null || !dict.ContainsKey(name) || dict[name] is null)
            {
                return null;
            }
            return dict[name];
        }
        private string GetSettingValue(Dictionary<string, Wetcardboard_Utilities_UserSettings> dict, string name)
        {
            if (dict is null)
            {
                return string.Empty;
            }

            if (!dict.ContainsKey(name) || dict[name] is null)
            {
                return string.Empty;
            }

            var res = dict[name].SettingValue;
            if (res is null)
            {
                return string.Empty;
            }
            return res;
        }
        private void SetSettingValue(Dictionary<string, Wetcardboard_Utilities_UserSettings> dict, string name, Wetcardboard_Utilities_UserSettings? value)
        {
            if (dict is null || string.IsNullOrEmpty(name) || value is null)
            {
                return;
            }

            if (!dict.ContainsKey(name))
            {
                dict.Add(name, value);
            }
            else
            {
                dict[name] = value;
            }
        }
        private void UpdateOrgValues(string key)
        {
            var settObj = SettingsDict[key];
            var settVal = settObj.SettingValue;
            SetSettingValue(OrgSettingsDict, key, settObj);

            switch (key)
            {
                case UserSettingsConstants_Wetcardboard_Utilities.LOCALIZATION_COUNTRY:
                    LocalizationCountry = settVal;
                    break;
            }
        }
        #endregion \ Private Methods

        #region Public Methods
        public bool HasChanges()
        {
            var res = false;

            foreach (var settKey in SettingsDict.Keys)
            {
                var settObj = SettingsDict[settKey];
                if (!OrgSettingsDict.ContainsKey(settKey))
                {
                    res = true;
                    break;
                }

                var orgSettObj = OrgSettingsDict[settKey];
                if (!CheckStrings(settObj.SettingValue, orgSettObj.SettingValue))
                {
                    res = true;
                    break;
                }
            }

            return res;
        }
        public IEnumerable<Wetcardboard_Utilities_UserSettings> GetUserSettings()
        {
            var res = new List<Wetcardboard_Utilities_UserSettings>();
            foreach(var key in SettingsDict.Keys)
            {
                var setting = SettingsDict[key];
                res.Add(setting);
            }
            return res;
        }
        public IEnumerable<Wetcardboard_Utilities_UserSettings> GetUserSettingsWithChanges()
        {
            var res = new List<Wetcardboard_Utilities_UserSettings>();
            foreach(var key in SettingsDict.Keys)
            {
                var settObj = SettingsDict[key];
                if (!OrgSettingsDict.ContainsKey(key))
                {
                    res.Add(settObj);
                    continue;
                }

                var orgSettObj = OrgSettingsDict[key];
                if (!CheckStrings(settObj.SettingValue, orgSettObj.SettingValue))
                {
                    res.Add(settObj);
                    continue;
                }
            }
            return res;
        }
        public void AddSetting(Wetcardboard_Utilities_UserSettings setting)
        {
            if (SettingsDict is null)
            {
                SettingsDict = new Dictionary<string, Wetcardboard_Utilities_UserSettings>();
            }

            var settKey = setting.SettingName;
            if (setting is null || string.IsNullOrEmpty(settKey))
            {
                return;
            }

            if (!SettingsDict.ContainsKey(settKey))
            {
                SettingsDict.Add(settKey, setting);
            }
            else
            {
                SettingsDict[settKey] = setting;
            }

            UpdateOrgValues(settKey);
        }
        public void AddSettings(IEnumerable<Wetcardboard_Utilities_UserSettings> settings)
        {
            if (settings is null)
            {
                return;
            }

            foreach (var setting in settings)
            {
                AddSetting(setting);
            }
        }
        #endregion \ Public Methods
        #endregion \ Methods
    }
}
