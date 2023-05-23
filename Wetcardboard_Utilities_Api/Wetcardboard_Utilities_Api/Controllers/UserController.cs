using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wetcardboard_Utilities_Api.Services.Interfaces;
using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        #region Fields & Properties
        #region Fields
        private IUserService _userService;
        #endregion \ Fields
        #endregion \ Fields & Properties


        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion \ Constructor


        #region Endpoints
        [HttpGet]
        [Authorize]
        [Route("UserSettings/{userGuid}")]
        public IActionResult GetUserSettings(string userGuid)
        {
            var userSettings = _userService.GetUserSettings(userGuid);
            if (userSettings is null)
            {
                return BadRequest();
            }
            var res = JsonConvert.SerializeObject(userSettings);
            return Ok(res);
        }
        [HttpPut]
        [Authorize]
        [Route("UserSettings/{userGuid}")]
        public IActionResult SaveUserSetting([FromBody] object jsonBodyObj, string userGuid)
        {
            var jsonBody = $"{jsonBodyObj}";
            var userSettings = JsonConvert.DeserializeObject<IEnumerable<Wetcardboard_Utilities_UserSettings>>(jsonBody);
            if (userSettings is null)
            {
                return BadRequest("No data retrieved");
            }
            var saveOk = _userService.SaveUserSettings(userGuid, userSettings);
            if (!saveOk)
            {
                return StatusCode(500, "Something went wrong.");
            }
            return Ok();
        }
        #endregion \ Endpoints
    }
}
