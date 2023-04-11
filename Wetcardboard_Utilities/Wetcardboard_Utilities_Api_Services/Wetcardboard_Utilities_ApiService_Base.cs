using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities_Api_Services
{
    public class Wetcardboard_Utilities_ApiService_Base
    {
        #region Fields & Properties
        #region Fields
        private Wetcardboard_Utilities_Fe_Appsettings _appSettings;
        #endregion \ Fields

        #region Properties
        protected string ApiBasePath
        {
            get
            {
                return $"{_appSettings.ApiBasePath}";
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Constructor
        public Wetcardboard_Utilities_ApiService_Base(
            Wetcardboard_Utilities_Fe_Appsettings appSettings)
        {
            _appSettings = appSettings;
        }
        #endregion \ Constructor
    }
}
