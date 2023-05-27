using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Database.Connector
{
    public interface IDbConn_Wetcardboard_Utilities
    {
        #region Localization Countries
        IEnumerable<Wetcardboard_Utilities_LocalizationCountry> GetLocalizationCountries();
        Wetcardboard_Utilities_LocalizationCountry? GetLocalizationCountryById(int id);
        #endregion \ Localization Countries


        #region Tokens
        bool AddUserToken(int userId, string token, DateTime expires);
        Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserId(int userId);
        Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserGuid(string userGuid);
        #endregion \ Tokens


        #region Users
        Wetcardboard_Utilities_User? GetUserByEmail(string email);
        Wetcardboard_Utilities_User? GetUserByGuid(string guid);
        #endregion \ Users


        #region User Roles
        IEnumerable<string> GetUserRolesByUserGuid(string guid);
        #endregion \ User Roles


        #region User Settings
        bool SaveUserSetting(string userGuid, Wetcardboard_Utilities_UserSettings setting);
        IEnumerable<Wetcardboard_Utilities_UserSettings>? GetUserSettings(string userGuid);
        #endregion \ User Settings
    }
}
