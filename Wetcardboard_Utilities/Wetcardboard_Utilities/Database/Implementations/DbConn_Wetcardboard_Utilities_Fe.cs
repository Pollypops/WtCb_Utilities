using Wetcardboard_Utilities.Database.Interfaces;
using Wetcardboard_Utilities_Database.Connector;
using Wetcardboard_Utilities_Models.Database;
using Wetcardboard_Utilities_Models.Front_End;

namespace Wetcardboard_Utilities.Database.Implementations
{
    public class DbConn_Wetcardboard_Utilities_Fe : IDbConn_Wetcardboard_Utilities_Fe
    {
        #region Fields & Properties
        #region Fields
        private readonly IDbConn_Wetcardboard_Utilities _dbConn;
        #endregion \ Fields
        #endregion \ Fields & Properties


        #region Constructor
        public DbConn_Wetcardboard_Utilities_Fe(
                IDbConn_Wetcardboard_Utilities dbConn
            )
        {
            _dbConn = dbConn;
        }
        #endregion \ Constructor


        #region Interface Implementations
        #region IWetcardboard_Utilities_Fe_DbConn Implementation
        #region Users
        public Wetcardboard_Utilities_Fe_User? GetUserByEmail(string email)
        {
            var user = _dbConn.GetUserByEmail(email);
            if (user is null)
            {
                return null;
            }

            var res = new Wetcardboard_Utilities_Fe_User
            {
                Guid = user.Guid,
                Login = user.Login
            };
            return res;
        }
        public Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserGuid(string userGuid)
        {
            var token = _dbConn.GetLatestActiveUserTokenByUserGuid(userGuid);
            if (token is null)
            {
                return null;
            }

            return token;
        }
        #endregion \ Users


        #region User Roles
        public IEnumerable<string> GetUserRolesByUserGuid(string guid)
        {
            return _dbConn.GetUserRolesByUserGuid(guid);
        }
        #endregion \ User Roles
        #endregion \ IWetcardboard_Utilities_Fe_DbConn
        #endregion \ Interface Implementations
    }
}
