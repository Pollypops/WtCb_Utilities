using Wetcardboard_Authentication.Model;

namespace Wetcardboard_Authentication.Authenticator.Azure_AD_OAuth2
{
    public class Auth_DtModel_Azure_AD_OAuth2 : AuthenticatorDataModelBase
    {
        #region Fields & Properties
        #region Properties
        public List<string> OtherMails { get; set; } = new List<string>(); 

        public string PrincipalName { get; set; } = string.Empty;
        #endregion \ Properties
        #endregion \ Fields & Properties
    }
}
