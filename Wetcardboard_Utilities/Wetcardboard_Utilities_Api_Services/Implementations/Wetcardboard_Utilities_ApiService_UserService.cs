using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Api_Services.Interfaces;
using Wetcardboard_Utilities_Models.Database;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities_Api_Services.Implementations
{
    public class Wetcardboard_Utilities_ApiService_UserService :
        Wetcardboard_Utilities_ApiService_Base,
        IWetcardboard_Utilities_ApiService_UserService
    {
        #region Fields & Properties
        #region Fields
        private const string _apiUserPath = "User";
        private const string _apiUserSettingsPath = $"{_apiUserPath}/UserSettings";
        #endregion \ Fields

        #region Properties
        private string ApiUserBasePath
        {
            get
            {
                return $"{ApiBasePath}/{_apiUserPath}";
            }
        }
        private string ApiUserSettingsBasePath
        {
            get
            {
                return $"{ApiBasePath}/{_apiUserSettingsPath}";
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Wetcardboard_Utilities_ApiService_UserService(
                IHttpClientFactory httpClientFactory,
                IHttpContextAccessor httpContextAccessor,
                IWtCbLogger logger,
                Wetcardboard_Utilities_Fe_Appsettings appSettings
            ) : base(httpClientFactory, httpContextAccessor, logger, appSettings) { }
        #endregion \ Constructor


        #region IWetcardboard_Utilities_ApiService_UserService Implementation
        public async Task<bool> SaveFeSettingsPageSettings(Wetcardboard_Utilities_Fe_SettingsPageSettings settingsPageSettings)
        {
            var settings = settingsPageSettings.GetUserSettings();

            var userGuid = GetUserGuid();
            var url = $"{ApiUserSettingsBasePath}/{userGuid}";

            var jsonContent = JsonConvert.SerializeObject(settings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var reqMsg = GetHttpRequestMessage_Bearer(HttpMethod.Put, url);
            reqMsg.Content = content;

            var http = _httpClientFactory.CreateClient();
            var respMsg = await http.SendAsync(reqMsg);
            if (respMsg.StatusCode != HttpStatusCode.OK)
            {
                var logMsg = $"Error saving user settings - Status code: {respMsg.StatusCode}, Msg: {respMsg.RequestMessage}";
                _logger.Log(logMsg, LogLevel.Error);
                return false;
            }

            return true;
        }
        public async Task<Wetcardboard_Utilities_Fe_SettingsPageSettings> GetFeSettingsPageSettings()
        {
            var res = new Wetcardboard_Utilities_Fe_SettingsPageSettings();

            var userGuid = GetUserGuid();
            var url = $"{ApiUserSettingsBasePath}/{userGuid}";

            var reqMsg = GetHttpRequestMessage_Bearer(HttpMethod.Get, url);

            var http = _httpClientFactory.CreateClient();
            var respMsg = await http.SendAsync(reqMsg);
            if (respMsg.StatusCode != HttpStatusCode.OK)
            {
                var logMsg = $"Error retrieving user settings - Status code: {respMsg.StatusCode}, Msg: {respMsg.RequestMessage}";
                _logger.Log(logMsg, LogLevel.Error);
                return res;
            }

            var respContent = await respMsg.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(respContent))
            {
                var logMsg = $"Error retrieving user settings - Response contains no data";
                _logger.Log(logMsg, LogLevel.Warning);
                return res;
            }

            var settingsList = JsonConvert.DeserializeObject<IEnumerable<Wetcardboard_Utilities_UserSettings>>(respContent);
            if (settingsList is null)
            { // Should not happen
                return res;
            }

            res.AddSettings(settingsList);
            return res;
        }
        #endregion \ IWetcardboard_Utilities_ApiService_UserService Implementation
    }
}
