using System.Security.Claims;

namespace Wetcardboard_Utilities_Models.Front_End
{
    public class Wetcardboard_Utilities_Fe_ClaimsPrincipal : ClaimsPrincipal
    {
        #region Fields & Properties
        #region Properties
        public string ApiToken { get; set; } = string.Empty;
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Wetcardboard_Utilities_Fe_ClaimsPrincipal(ClaimsIdentity identity)
            : base(identity) { }
        #endregion \ Constructor
    }
}
