using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Api.Services.Interfaces;
using Wetcardboard_Utilities_Database.Connector;
using Wetcardboard_Utilities_General.Security.Jwt;

namespace Wetcardboard_Utilities_Api.Services.Implementations
{
    public class TokenService : ServiceBase, ITokenService
    {
        #region Fields & Properties
        #region Fields
        private readonly IJwtFunctions _jwtFunctions;
        #endregion \ Fields
        #endregion \ Fields & Properties


        #region Constructor
        public TokenService(
            IDbConn_Wetcardboard_Utilities dbConn_Wetcardboard_Utilities,
            IJwtFunctions jwtFunctions,
            IWtCbLogger logger
            ) : base(dbConn_Wetcardboard_Utilities, logger)
        {
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
                _wtCbLogger.Log("No user found.", LogLevel.Error);
                return false;
            }

            var userRoles = _dbConn_Wetcardboard_Utilities.GetUserRolesByUserGuid(userGuid);

            var id = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Login));
            id.AddClaim(new Claim(ClaimTypes.Name, user.Login));

            var claims = new List<Claim>();
            foreach(var role in userRoles)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
                id.AddClaim(claim);
            }

            var expires = DateTime.UtcNow.AddHours(5);
            var jwtToken = _jwtFunctions.GenerateJwtToken(user.Id, user.Login, claims, expires);
            var res = _dbConn_Wetcardboard_Utilities.AddUserToken(user.Id, jwtToken, expires);
            return res;
        }
        #endregion \ ITokenService Implementation
        #endregion \ Interface Implementations
    }
}
