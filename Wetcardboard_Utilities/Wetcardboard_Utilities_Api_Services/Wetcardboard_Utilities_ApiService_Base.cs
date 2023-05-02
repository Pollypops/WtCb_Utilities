using Microsoft.AspNetCore.Http;
using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities_Api_Services
{
    public class Wetcardboard_Utilities_ApiService_Base
    {
        #region Fields & Properties
        #region Fields
        private IHttpContextAccessor _httpContextAccessor;
        private Wetcardboard_Utilities_Fe_Appsettings _appSettings;

        protected IHttpClientFactory _httpClientFactory;
        protected IWtCbLogger _logger;
        #endregion \ Fields

        #region Properties
        protected string ApiBasePath
        {
            get
            {
                return $"{_appSettings.ApiBasePath}";
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Wetcardboard_Utilities_ApiService_Base(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IWtCbLogger logger,
            Wetcardboard_Utilities_Fe_Appsettings appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _appSettings = appSettings;
        }
        #endregion \ Constructor


        #region Methods
        protected HttpRequestMessage GetHttpRequestMessage_Bearer(HttpMethod method, string url)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tokenClaim = httpContext.User.Claims.First(x => string.Equals(x.Type, "api_token", StringComparison.OrdinalIgnoreCase));
            if (tokenClaim is null)
            {
                // TODO: Implement error handling!
                throw new NotImplementedException("");
            }

            var res = new HttpRequestMessage(method, url);
            res.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tokenClaim.Value}");
            return res;
        }
        #endregion \ Methods
    }
}
