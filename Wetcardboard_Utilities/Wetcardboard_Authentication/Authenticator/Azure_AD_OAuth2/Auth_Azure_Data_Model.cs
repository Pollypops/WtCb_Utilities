using Wetcardboard_Authentication.Model;

namespace Wetcardboard_Authentication.Authenticator.Azure_AD_OAuth2
{
    public class Auth_Azure_Data_Model : AuthenticatorDataModelBase
    {
        #region Fields & Properties
        #region Properties
        public string? userPrincipalName { get; set; }
        public string[]? otherMails { get; set; }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Auth_Azure_Data_Model(string mail, string userPrincipalName, string[] otherMails)
        {
            this.mail = mail;
            this.userPrincipalName = userPrincipalName;
            this.otherMails = otherMails;
        }
        #endregion \ Constructor


        #region Functions
        #region Public Functions
        public IEnumerable<string> GetMails()
        {
            var res = new List<string>();

            if (!string.IsNullOrEmpty(mail))
            {
                res.Add(mail);
            }

            if (otherMails is not null && otherMails.Length > 0)
            {
                foreach(var oMail in otherMails)
                {
                    res.Add(oMail);
                }
            }

            if (!string.IsNullOrEmpty(userPrincipalName))
            {
                res.Add(userPrincipalName);
            }

            return res;
        }
        #endregion \ Public Functions
        #endregion \ Functions
    }
}
