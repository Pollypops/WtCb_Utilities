using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Api_Services.Interfaces
{
    public interface IWetcardboard_Utilities_ApiService_LocalizationService
    {
        Task<IEnumerable<Wetcardboard_Utilities_LocalizationCountry>> GetLocalizationCountriesAsync();
    }
}
