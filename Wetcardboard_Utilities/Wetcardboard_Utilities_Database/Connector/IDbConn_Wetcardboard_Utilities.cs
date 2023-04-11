using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Database.Connector
{
    public interface IDbConn_Wetcardboard_Utilities
    {
        #region Tokens
        bool AddUserToken(int userId, string token, DateTime expires);
        Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserId(int userId);
        Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserGuid(string userGuid);
        #endregion \ Tokens


        #region Users
        Wetcardboard_Utilities_User? GetUserByEmail(string email);
        Wetcardboard_Utilities_User? GetUserByGuid(string guid);
        #endregion \ Users
    }
}
