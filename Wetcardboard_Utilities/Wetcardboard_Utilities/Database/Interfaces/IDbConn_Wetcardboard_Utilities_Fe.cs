using Wetcardboard_Utilities_Models.Database;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities.Database.Interfaces
{
    public interface IDbConn_Wetcardboard_Utilities_Fe
    {
        Wetcardboard_Utilities_Fe_User? GetUserByEmail(string email);
        Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserGuid(string userGuid);
    }
}
