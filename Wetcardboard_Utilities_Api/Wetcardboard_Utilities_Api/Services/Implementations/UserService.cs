using Wetcardboard_Shared.Logging;
using Wetcardboard_Utilities_Api.Services.Interfaces;
using Wetcardboard_Utilities_Database.Connector;
using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Api.Services.Implementations
{
    public class UserService : ServiceBase, IUserService
    {
        #region Constructor
        public UserService(
            IDbConn_Wetcardboard_Utilities dbConn_Wetcardboard_Utilities,
            IWtCbLogger logger
            ) : base(dbConn_Wetcardboard_Utilities, logger) { }
        #endregion \ Constructor


        #region Implement IUserService
        public IEnumerable<Wetcardboard_Utilities_UserSettings> GetUserSettings(string userGuid)
        {
            var userSettings = _dbConn_Wetcardboard_Utilities.GetUserSettings(userGuid);
            if (userSettings is null)
            {
                _wtCbLogger.Log("No user settings found.", LogLevel.Warning);
                userSettings = new List<Wetcardboard_Utilities_UserSettings>();
            }
            return userSettings;
        }
        public bool SaveUserSettings(string userGuid, IEnumerable<Wetcardboard_Utilities_UserSettings> settings)
        {
            var res = true;
            foreach(var setting in settings)
            {
                var dbSaveRes = _dbConn_Wetcardboard_Utilities.SaveUserSetting(userGuid, setting);
                if (!dbSaveRes)
                {
                    res = false;
                }
            }
            return res;
        }
        #endregion \ Implement IUserService
    }
}
