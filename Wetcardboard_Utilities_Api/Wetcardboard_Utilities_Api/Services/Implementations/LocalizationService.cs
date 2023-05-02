using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Api.Services.Interfaces;
using Wetcardboard_Utilities_Database.Connector;
using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Api.Services.Implementations
{
    public class LocalizationService : ServiceBase, ILocalizationService
    {
        #region Constructor
        public LocalizationService(
            IDbConn_Wetcardboard_Utilities dbConn_Wetcardboard_Utilities,
            IWtCbLogger wtCbLogger
            ) : base(dbConn_Wetcardboard_Utilities, wtCbLogger) { }
        #endregion \ Constructor


        #region ILocalizationService Implementation
        public IEnumerable<Wetcardboard_Utilities_LocalizationCountry> GetLocalizationCountries()
        {
            var res = _dbConn_Wetcardboard_Utilities.GetLocalizationCountries();
            if (res is null)
            {
                res = new List<Wetcardboard_Utilities_LocalizationCountry>();
            }
            return res;
        }
        #endregion \ ILocalizationService Implementation
    }
}
