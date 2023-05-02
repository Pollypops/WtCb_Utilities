using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Api.Services.Interfaces
{
    public interface ILocalizationService
    {
        IEnumerable<Wetcardboard_Utilities_LocalizationCountry> GetLocalizationCountries();
    }
}
