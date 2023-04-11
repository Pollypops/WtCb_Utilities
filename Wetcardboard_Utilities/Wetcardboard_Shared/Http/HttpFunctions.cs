namespace Wetcardboard_Shared.Http
{
    public class HttpFunctions : IHttpFunctions
    {
        #region Properties
        private IHttpClientFactory HttpClientFactory { get; set; }
        #endregion \ Properties


        #region Constructor
        public HttpFunctions(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }
        #endregion \ Constructor


        #region Interface Implementation IHttpFunction
        public HttpClient GetClientWithBearerToken(string token)
        {
            var http = HttpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return http;
        }
        #endregion \ Interface Implementation IHttpFunction
    }
}
