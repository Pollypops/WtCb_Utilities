using Wetcardboard_Utilities_Api_Services.Interfaces;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities_Api_Services.Implementations
{
    public class Wetcardboard_Utilities_ApiService_TokenService :
        Wetcardboard_Utilities_ApiService_Base,
        IWetcardboard_Utilities_ApiService_TokenService
    {
        #region Fields & Properties
        #region Fields
        private readonly IHttpClientFactory _httpClientFactory;

        private const string _apiTokenPath = "Token";
        #endregion \ Fields

        #region Properties
        private string ApiTokenBasePath
        {
            get
            {
                return $"{ApiBasePath}/{_apiTokenPath}";
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Wetcardboard_Utilities_ApiService_TokenService(
                IHttpClientFactory httpClientFactory,
                Wetcardboard_Utilities_Fe_Appsettings appSettings
            ) : base(appSettings)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion \ Constructor


        #region Interface Implementations
        #region IApiService_TokenService Implementation
        public async Task<bool> CreateUserJwtTokenAsync(string userGuid)
        {
            var url = $"{ApiTokenBasePath}/{userGuid}";

            var reqMsg = new HttpRequestMessage(HttpMethod.Put, url);

            var http = _httpClientFactory.CreateClient();
            var respMsg = await http.SendAsync(reqMsg);
            if (respMsg.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }
        #endregion \ IApiService_TokenService
        #endregion \ Interface Implementations
    }
}
