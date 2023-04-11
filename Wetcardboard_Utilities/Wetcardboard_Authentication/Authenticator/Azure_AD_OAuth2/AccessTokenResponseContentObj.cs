namespace Wetcardboard_Authentication.Authenticator.Azure_AD_OAuth2
{
    public class AccessTokenResponseContentObj
    {
        #region Fields & Properties
        public int expires_in { get; set; } = 0;
        public int ext_expires_in { get; set; } = 0;

        public string? access_token { get; set; }
        public string? id_token { get; set; }
        public string? scope { get; set; }
        public string? token_type { get; set; }
        #endregion \ Fields & Properties
    }
}
