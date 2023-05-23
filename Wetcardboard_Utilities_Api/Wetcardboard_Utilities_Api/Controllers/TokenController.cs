using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Api.Services.Interfaces;

namespace Wetcardboard_Utilities_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class TokenController : Controller
    {
        #region Fields & Properties
        #region Fields
        private ITokenService _tokenService;
        private IWtCbLogger _logger;
        #endregion \ Fields
        #endregion \ Fields & Properties


        #region Constructor
        public TokenController(ITokenService tokenService, IWtCbLogger logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }
        #endregion \ Constructor


        #region Endpoints
        [HttpPut]
        [AllowAnonymous]
        [Route("{guid}")]
        public IActionResult Put(string guid)
        {
            try
            {
                var res = _tokenService.CreateUserJwtToken(guid);
                if (!res)
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                var logMsg = "Error ocurred during JWT token creation.";
                _logger.Log(logMsg, LogLevel.Error, exception: ex);
                return StatusCode(500, "Something went wrong.");
            }
            return Ok();
        }
        #endregion \ Endpoints
    }
}
