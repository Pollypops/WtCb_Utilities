using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wetcardboard_Authentication.Authenticator;
using Wetcardboard_Authentication.Authenticator.Azure_AD_OAuth2;
using Wetcardboard_Shared.Navigation;
using Wetcardboard_Shared.Security.Jwt;
using Wetcardboard_Utilities.Database.Interfaces;
using Wetcardboard_Utilities_Api_Services.Interfaces;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AdLoginController : Controller
    {
        #region Fields & Properties
        #region Fields
        private IAuthenticator _authenticator;
        private IDbConn_Wetcardboard_Utilities_Fe _dbConn;
        private IJwtFunctions _jwtFunctions;
        private IWetcardboard_Utilities_ApiService_TokenService _tokenService;
        private UrlFactory _urlFactory;
        #endregion \ Fields

        #region Properties
        private Auth_Azure_AD_OAuth2? AzureAdAuthenticator
        {
            get
            {
                if (_authenticator is null) return null;
                return (Auth_Azure_AD_OAuth2)_authenticator;
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public AdLoginController(
            IAuthenticator authenticator,
            IDbConn_Wetcardboard_Utilities_Fe dbConn,
            IJwtFunctions jwtFunctions,
            IWetcardboard_Utilities_ApiService_TokenService tokenService,
            UrlFactory urlFactory
            )
        {
            _authenticator = authenticator;
            _dbConn = dbConn;
            _jwtFunctions = jwtFunctions;
            _tokenService = tokenService;
            _urlFactory = urlFactory;
        }
        #endregion \ Constructor


        #region Endpoints
        [HttpPost]
        [AllowAnonymous]
        [Route("authenticated")]
        public async Task<IActionResult> Authenticated()
        {
            var authCode = Request.Form["code"].ToString();
            if (AzureAdAuthenticator is null)
            {
                return StatusCode(500);
            }
            var accessTokenObj = await AzureAdAuthenticator.GetIdToken(authCode);
            if (accessTokenObj is null || string.IsNullOrEmpty(accessTokenObj.access_token))
            {
                // TODO: Implement error handling!
                return NotFound();
            }
            var graphData = await AzureAdAuthenticator.GetGraphData(accessTokenObj.access_token);
            if (graphData is null)
            {
                return NotFound();
            }

            Wetcardboard_Utilities_Fe_User? user = null;

            var userMails = graphData.GetMails();
            foreach (var mail in userMails)
            {
                user = _dbConn.GetUserByEmail(mail);
                if (user is null)
                {
                    continue;
                }

                // TODO: Implement User Data
                break;
            }

            if (user is null)
            {
                return NotFound("User not registered in system.");
            }

            var tokenCreated = await _tokenService.CreateUserJwtTokenAsync(user.Guid);
            if (!tokenCreated)
            {
                return StatusCode(500, "Unable to create api token");
            }

            var apiToken = _dbConn.GetLatestActiveUserTokenByUserGuid(user.Guid);
            if (apiToken is null)
            {
                return StatusCode(500, "Api token could not be retrieved");
            }

            var id = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Login));
            id.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            id.AddClaim(new Claim(ClaimTypes.Role, user.UserRole));
            id.AddClaim(new Claim("api_token", apiToken.Token));
            id.AddClaim(new Claim("guid", user.Guid));

            var principal = new ClaimsPrincipal(id);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(5)
                });

            var resPath = _urlFactory.CreateUrl_Relative("home");
            var res = LocalRedirect(resPath);
            return res;
        }

        [HttpGet]
        [Authorize]
        [Route("sign_out")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            var resPath = _urlFactory.CreateUrl_Relative("/");
            var res = LocalRedirect(resPath);
            return res;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("id_token")]
        public async Task<IActionResult> IdToken()
        {
            var v = this.User;
            return Ok("bonk");
        }
#endregion \ Endpoints

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Tester result yo");
        }
    }
}
