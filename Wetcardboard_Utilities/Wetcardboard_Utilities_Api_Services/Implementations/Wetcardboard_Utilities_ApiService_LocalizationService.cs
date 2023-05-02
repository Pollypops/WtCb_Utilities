using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Api_Services.Interfaces;
using Wetcardboard_Utilities_Models.Database;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities_Api_Services.Implementations
{
    public class Wetcardboard_Utilities_ApiService_LocalizationService :
        Wetcardboard_Utilities_ApiService_Base,
        IWetcardboard_Utilities_ApiService_LocalizationService
    {
        #region Fields & Properties
        #region Fields
        private const string _apiLocalizationPath = "Localization";
        #endregion \ Fields 

        #region Properties
        private string ApiLocalizationBasePath
        {
            get
            {
                return $"{ApiBasePath}/{_apiLocalizationPath}";
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Wetcardboard_Utilities_ApiService_LocalizationService(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IWtCbLogger logger,
            Wetcardboard_Utilities_Fe_Appsettings appSettings
            ) : base(httpClientFactory, httpContextAccessor, logger, appSettings) { }
        #endregion \ Constructor


        #region IWetcardboard_Utilities_ApiService_LocalizationService Implementation
        public async Task<IEnumerable<Wetcardboard_Utilities_LocalizationCountry>> GetLocalizationCountriesAsync()
        {
            var url = $"{ApiLocalizationBasePath}/Countries";

            var reqMsg = GetHttpRequestMessage_Bearer(HttpMethod.Get, url);

            var http = _httpClientFactory.CreateClient();
            var respMsg = await http.SendAsync(reqMsg);
            if (respMsg.StatusCode != HttpStatusCode.OK )
            {
                var logMsg = $"Error retrieving localization countries - Error code: {respMsg.StatusCode}, Msg: {respMsg.RequestMessage}";
                _logger.Log(logMsg, LogLevel.Error);
                return new List<Wetcardboard_Utilities_LocalizationCountry>();
            }

            var respContent = await respMsg.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(respContent))
            {
                var logMsg = $"Error retriveing localization countries - Response contains no data.";
                _logger.Log(logMsg, LogLevel.Warning);
                return new List<Wetcardboard_Utilities_LocalizationCountry>();
            }

            var res = JsonSerializer.Deserialize<List<Wetcardboard_Utilities_LocalizationCountry>>(respContent);
            if (res is null)
            { // Should not happen
                return new List<Wetcardboard_Utilities_LocalizationCountry>();
            }
            return res.OrderBy(x => x.Language);
        }
        #endregion \ IWetcardboard_Utilities_ApiService_LocalizationService Implementation
    }
}
