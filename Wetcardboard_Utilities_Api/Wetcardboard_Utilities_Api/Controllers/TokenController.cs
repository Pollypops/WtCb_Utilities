using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        #endregion \ Fields
        #endregion \ Fields & Properties


        #region Constructor
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
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
                var v = "";
            }
            return Ok();
        }
        #endregion \ Endpoints
    }
}
