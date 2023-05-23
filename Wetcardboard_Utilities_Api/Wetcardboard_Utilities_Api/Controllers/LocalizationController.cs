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
        [Route("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            var localizationCountries = _localizationService.GetLocalizationCountries();
            if (localizationCountries is null)
            {
                localizationCountries = new List<Wetcardboard_Utilities_LocalizationCountry>();
            }
            var res = JsonConvert.SerializeObject(localizationCountries);
            return Ok(res);
        }
        #endregion \ Endpoints
    }
}
