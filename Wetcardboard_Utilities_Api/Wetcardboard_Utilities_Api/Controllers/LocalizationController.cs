using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wetcardboard_Utilities_Api.Services.Interfaces;
using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class LocalizationController : Controller
    {
        #region Fields & Properties
        #region Fields
        private ILocalizationService _localizationService;
        #endregion \ Fields
        #endregion \  Fields & Properties


        #region Constructor
        public LocalizationController(ILocalizationService localizationService)
        { 
            _localizationService = localizationService;
        }
        #endregion \ Constructor


        #region Endpoints
        [HttpGet]
        [Authorize]
        [Route("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            var res = _localizationService.GetLocalizationCountries();
            if (res is null)
            {
                res = new List<Wetcardboard_Utilities_LocalizationCountry>();
            }
            return Ok(res);
        }
        #endregion \ Endpoints
    }
}
