using Microsoft.AspNetCore.Components;
using Wetcardboard_Utilities_Models.Database;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities.Components
{
    public partial class SettingsPageContent
    {
        #region Fields & Properties
        #region Properties
        private bool HasSettingsChanges { get; set; } = false;
        private IEnumerable<Wetcardboard_Utilities_LocalizationCountry> Languages { get; set; } = new List<Wetcardboard_Utilities_LocalizationCountry>();
        private string HeaderTextFontSize { get; set; } = "16px";
        private Wetcardboard_Utilities_Fe_SettingsPageSettings Settings { get; set; } = new Wetcardboard_Utilities_Fe_SettingsPageSettings();
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Methods
        #region Event Handlers
        private async Task CreateToastMessageOnClick(EventArgs e)
        {
            _toastService.CreateToastMessage($"Test Message: {DateTime.Now:HH:mm:ss}");
        }
        private async Task LanguageSearchInputOnInput(ChangeEventArgs e)
        {
            var input = e.Value as string;
            var selection = Languages.FirstOrDefault(
                x =>
                    string.Equals(x.CultureInfoCode, input, StringComparison.OrdinalIgnoreCase)
            );

            if (selection is not null)
            {
                Settings.LocalizationCountry = selection.CultureInfoCode;
            }
            else
            {
                Settings.LocalizationCountry = string.Empty;
            }

            CheckSettings();
            StateHasChanged();
        }
        private async Task SaveOnClick(EventArgs e)
        {
            var saveOk = await _userService.SaveFeSettingsPageSettings(Settings);
            if (!saveOk)
            {
                _toastService.CreateToastMessage("An error ocurred.");
            }
            var userSettings = await _userService.GetFeSettingsPageSettings();
            Settings = userSettings;
            CheckSettings();
        }
        #endregion \ Event Handlers

        #region Private Methods
        private void CheckSettings()
        {
            HasSettingsChanges = Settings.HasChanges();
        }
        #endregion \ Private Methods
        #endregion \ Methods


        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            var languages = await _localizationService.GetLocalizationCountriesAsync();
            var userSettings = await _userService.GetFeSettingsPageSettings();
            Languages = languages;
            Settings = userSettings;
            await base.OnInitializedAsync();
        }
        #endregion \ Overrides
    }
}
