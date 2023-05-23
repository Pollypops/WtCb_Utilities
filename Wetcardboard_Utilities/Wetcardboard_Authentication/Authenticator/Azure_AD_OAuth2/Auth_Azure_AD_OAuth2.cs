using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Wetcardboard_Utilities_General.Extensions;

namespace Wetcardboard_Authentication.Authenticator.Azure_AD_OAuth2
{
    public class Auth_Azure_AD_OAuth2 : IAuthenticator
    {
        #region Fields & Properties
        #region Properties
        private IHttpClientFactory HttpClientFactory { get; }
        private NavigationManager NavigationManager { get; }

        public Auth_DtModelProps_Azure_AD_OAuth2_Auth Props { get; private set; }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Auth_Azure_AD_OAuth2(Auth_DtModelProps_Azure_AD_OAuth2_Auth props, NavigationManager navManager,
            IHttpClientFactory httpClientFactory)
        {
            Props = props;
            NavigationManager = navManager;
            HttpClientFactory = httpClientFactory;
        }
        #endregion \ Contructor


        #region IAuthenticator Implementation
        public void Authenticate()
        {
            var rnd = new Random(int.MaxValue);
            var authUrl = CreateAuthorizeUrl($"{rnd.Next()}");
            NavigationManager.NavigateTo(authUrl);
        }
        public void SignOut()
        {
            var signOutUrl = CreateSignOutUrl();
            NavigationManager.NavigateTo(signOutUrl);
        }
        #endregion \ IAuthenticator Implementation


        #region Private Functions
        private List<KeyValuePair<string, string>> CreateIdTokenContent(string authCode)
        {
            var res = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", Props.ClientId),
                new KeyValuePair<string, string>("scope", Props.Scope),
                new KeyValuePair<string, string>("code", authCode),
                new KeyValuePair<string, string>("redirect_uri", Props.Url_IdToken_RedirectUri),
                new KeyValuePair<string, string>("grant_type", Props.GrantType),
                new KeyValuePair<string, string>("code_verifier", Props.Code_Verifier),

                new KeyValuePair<string, string>("client_secret", Props.ClientSecret)
            };
            return res;
        }
        private string CreateAuthorizeUrl(string state)
        {
            var urlPart1 = Props.Url_AuthCode_Part1;
            var urlPart2 = Props.Url_AuthCode_Part2;

            if (!urlPart1.EndsWith("/"))
            {
                urlPart1 += "/";
            }
            if (!urlPart2.EndsWith("?"))
            {
                urlPart2 += "?";
            }

            var url = $"{urlPart1}{Props.Tenant}/{urlPart2}" +
                $"client_id={Props.ClientId}" +
                $"&response_type={Props.ResponseType}" +
                $"&redirect_uri={Props.Url_AuthCode_RedirectUri}" +
                $"&response_mode={Props.ResponseMode}" +
                $"&scope={Props.Scope}" +
                $"&state={Props.State.Replace("{state}", state)}" +
                $"&nonce={Props.Nonce_Pool.Random(60)}" +
                $"&code_challenge={Props.Code_challenge}" +
                $"&code_challenge_method={Props.Code_challenge_method}";
            return url;
        }
        private string CreateGraphApiUrl()
        {
            var url = Props.Url_GraphApi;
            return url;
        }
        private string CreateIdTokenUrl()
        {
            var urlPart1 = Props.Url_IdToken_Part1;
            var urlPart2 = Props.Url_IdToken_Part2;

            if (!urlPart1.EndsWith("/"))
            {
                urlPart1 += "/";
            }

            var url = $"{urlPart1}{Props.Tenant}/{urlPart2}";
            return url;
        }
        private string CreateSignOutUrl()
        {
            var urlPart1 = Props.Url_SignOut_Part1;
            var urlPart2 = Props.Url_SignOut_Part2;

            if (!urlPart1.EndsWith('/'))
            {
                urlPart1 += "/";
            }
            if (!urlPart2.EndsWith('?'))
            {
                urlPart2 += "?";
            }

            var url = $"{urlPart1}{Props.Tenant}/{urlPart2}" +
                $"post_logout_redirect_uri={Props.Url_SignOut_PostLogoutRedirectUri}";
            return url;
        }
        #endregion \ Private Functions


        #region Public Functions
        public async Task<Auth_Azure_Data_Model?> GetGraphData(string accessToken)
        { // Id token will be used as bearer token for graph api call
            var url = CreateGraphApiUrl();

            var reqMsg = new HttpRequestMessage(HttpMethod.Get, url);
            reqMsg.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");

            var http = HttpClientFactory.CreateClient();
            var respMsg = await http.SendAsync(reqMsg);
            var respCntnt = respMsg.Content.ReadAsStringAsync().Result;
            var respCntntObj = JsonSerializer.Deserialize<Auth_Azure_Data_Model>(respCntnt);
            return respCntntObj;
        }
        public async Task<AccessTokenResponseContentObj?> GetIdToken(string authCode)
        {
            var url = CreateIdTokenUrl();

            var contentObj = CreateIdTokenContent(authCode);

            var reqMsg = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(contentObj),
            };

            var http = HttpClientFactory.CreateClient();
            var respMsg = await http.SendAsync(reqMsg);
            var respCntnt = respMsg.Content.ReadAsStringAsync().Result;
            var respCntntObj = JsonSerializer.Deserialize<AccessTokenResponseContentObj>(respCntnt);
            return respCntntObj;
        }
        #endregion \ Public Functions
    }
}
