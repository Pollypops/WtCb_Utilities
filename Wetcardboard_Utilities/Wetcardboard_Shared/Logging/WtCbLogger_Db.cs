using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Wetcardboard_Database.Connector;
using Wetcardboard_Database.DbTypes;
using Wetcardboard_Database.Parameters;
using Wetcardboard_General.Extensions;
using Wetcardboard_Utilities_General.Constants;
using Wetcardboard_Utilities_Models.System;

namespace Wetcardboard_Shared.Logging
{
    public class WtCbLogger_Db : IWtCbLogger
    {
        #region Fields & Properties
        #region Fields
        private readonly IDbConn _dbConn;
        private readonly Wetcardboard_Utilities_System_Props _systemProps;
        #endregion \ Fields 
        #endregion \ Fields & Properties


        #region Constructor
        public WtCbLogger_Db(Wetcardboard_Utilities_System_Props systemProps, IDbConn dbConn)
        {
            _dbConn = dbConn;
            _systemProps = systemProps;
        }
        #endregion \ Constructor


        #region IWtCbLogger Implementation
        public void Log(string message, LogLevel logLevel = LogLevel.Information, Exception? exception = null, int? userId = null, 
            [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            string exMsg;
            if (exception != null)
            {
                exMsg = exception.GetFullExceptionMessage();
            }
            else
            {
                exMsg = string.Empty;
            }

            var parameters = new List<SqlParameterWithValue>
            {
                new SqlParameterWithValue("_systemIdentifier", DatabaseType.VarChar, _systemProps.SystemIdentifier),
                new SqlParameterWithValue("_userId", DatabaseType.Int, userId),
                new SqlParameterWithValue("_message", DatabaseType.MediumText, message),
                new SqlParameterWithValue("_exMessage", DatabaseType.MediumText, exMsg),
                new SqlParameterWithValue("_severity", DatabaseType.VarChar, logLevel.ToString()),
                new SqlParameterWithValue("_loggingFile", DatabaseType.VarChar, callerFilePath),
                new SqlParameterWithValue("_loggingMember", DatabaseType.VarChar, callerMemberName),
                new SqlParameterWithValue("_loggingLine", DatabaseType.Int, callerLineNumber)
            };
            _dbConn.ExecuteStoredProcedureNonQuery(StoredProcedureConstants_Wetcardboard_Utilities.WETCARDBOARD_UTILITIES_SP_LOGS_ADD, parameters);
        }
        #endregion \ IWtCbLogger Implementation
    }
}
