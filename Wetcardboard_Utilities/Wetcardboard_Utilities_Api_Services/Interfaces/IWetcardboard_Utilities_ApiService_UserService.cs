using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities_Api_Services.Interfaces
{
    public interface IWetcardboard_Utilities_ApiService_UserService
    {
        Task<bool> SaveFeSettingsPageSettings(Wetcardboard_Utilities_Fe_SettingsPageSettings settings);
        Task<Wetcardboard_Utilities_Fe_SettingsPageSettings> GetFeSettingsPageSettings();
    }
}
