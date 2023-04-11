using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Wetcardboard_Shared.Security.Jwt;
using Wetcardboard_Utilities_Api.Services.Interfaces;
using Wetcardboard_Utilities_Database.Connector;

namespace Wetcardboard_Utilities_Api.Services.Implementations
{
    public class TokenService : ITokenService
    {
        #region Fields & Properties
        private IDbConn_Wetcardboard_Utilities _dbConn_Wetcardboard_Utilities;
        private IJwtFunctions _jwtFunctions;
        #endregion \ Fields & Properties


        #region Constructor
        public TokenService(
            IDbConn_Wetcardboard_Utilities dbConn_Wetcardboard_Utilities,
            IJwtFunctions jwtFunctions)
        {
            _dbConn_Wetcardboard_Utilities = dbConn_Wetcardboard_Utilities;
            _jwtFunctions = jwtFunctions;
        }
        #endregion \ Constructor


        #region Interface Implementations
        #region ITokenService Implementation
        public bool CreateUserJwtToken(string userGuid)
        {
            var user = _dbConn_Wetcardboard_Utilities.GetUserByGuid(userGuid);
            if (user is null)
            {
                // TODO: Implement Logging
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.UserRole)
            };

            var id = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Login));
            id.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            id.AddClaim(new Claim(ClaimTypes.Role, user.UserRole));

            var expires = DateTime.UtcNow.AddHours(5);
            var jwtToken = _jwtFunctions.GenerateJwtToken(user.Id, user.Login, claims, expires);
            var res = _dbConn_Wetcardboard_Utilities.AddUserToken(user.Id, jwtToken, expires);
            return res;
        }
        #endregion \ ITokenService Implementation
        #endregion \ Interface Implementations
    }
}
