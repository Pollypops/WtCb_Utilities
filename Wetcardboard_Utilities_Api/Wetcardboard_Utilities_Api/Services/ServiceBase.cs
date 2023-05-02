using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Database.Connector;

namespace Wetcardboard_Utilities_Api.Services
{
    public class ServiceBase
    {
        #region Fields & Properties
        protected IDbConn_Wetcardboard_Utilities _dbConn_Wetcardboard_Utilities;
        protected IWtCbLogger _wtCbLogger;
        #endregion \ Fields & Properties


        #region Constructor
        public ServiceBase(
            IDbConn_Wetcardboard_Utilities dbConn_Wetcardboard_Utilities,
            IWtCbLogger wtCbLogger
            )
        {
            _dbConn_Wetcardboard_Utilities = dbConn_Wetcardboard_Utilities;
            _wtCbLogger = wtCbLogger;
        }
        #endregion \ Constructor
    }
}
