using Wetcardboard_Database.Connector;
using Wetcardboard_Shared.Logging;

namespace Wetcardboard_Utilities_Database.Connector
{
    public class DbConn_Wetcardboard_Utilities_Base
    {
        #region Fields & Properties
        #region Fields
        protected readonly IDbConn _dbConn;
        protected readonly IWtCbLogger _logger;
        #endregion \ Fields
        #endregion \ Fields & Properties


        #region Constructor
        public DbConn_Wetcardboard_Utilities_Base(
            IDbConn dbConn,
            IWtCbLogger logger)
        {
            _dbConn = dbConn;
            _logger = logger;
        }
        #endregion \ Constructor
    }
}
