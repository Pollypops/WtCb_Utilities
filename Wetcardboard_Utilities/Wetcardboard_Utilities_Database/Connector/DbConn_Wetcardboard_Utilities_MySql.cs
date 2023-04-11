using Wetcardboard_Database.Connector;
using Wetcardboard_Database.Helpers;
using Wetcardboard_Database.Parameters;
using Wetcardboard_Shared.Constants;
using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Database.Connector
{
    public class DbConn_Wetcardboard_Utilities_MySql : IDbConn_Wetcardboard_Utilities
    {
        #region Fields & Properties
        private IDbConn DbConn { get; }
        #endregion \ Fields & Properties


        #region Constructor
        public DbConn_Wetcardboard_Utilities_MySql(IDbConn dbConn)
        {
            DbConn = dbConn;
        }
        #endregion \ Constructor


        #region Interface Implementations
        #region IDbConn_Wetcardboard_Utilities Implementation
        #region Tokens
        public bool AddUserToken(int userId, string token, DateTime expires)
        {
            var parameters = new List<SqlParameterWithValue>
            {
                new SqlParameterWithValue("_userId", userId),
                new SqlParameterWithValue("_token", token),
                new SqlParameterWithValue("_expires", expires)
            };
            var dbRes = DbConn.ExecuteStoredProcedureNonQuery(StoredProcedureConstants_Wetcardboard_Utilities.WETCARDBOARD_UTILITIES_SP_USERTOKENS_ADD, parameters);
            if (dbRes < 1)
            {
                return false;
            }

            return true;
        }
        public Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserId(int userId)
        {
            var parameters = new List<SqlParameterWithValue>
            {
                new SqlParameterWithValue("_user_id", userId)
            };
            var dbRes = DbConn.ExecuteStoredProcedure(StoredProcedureConstants_Wetcardboard_Utilities.WETCARDBOARD_UTILITIES_SP_GETLATESTACTIVEUSERTOKENBYUSERID, parameters);
            if (dbRes is null || dbRes.Tables.Count == 0)
            {
                return null;
            }

            return DbHelper.GetSingleObjectFromDataSet<Wetcardboard_Utilities_UserToken>(dbRes);
        }
        public Wetcardboard_Utilities_UserToken? GetLatestActiveUserTokenByUserGuid(string userGuid)
        {
            var parameters = new List<SqlParameterWithValue>
            {
                new SqlParameterWithValue("_user_guid", userGuid)
            };
            var dbRes = DbConn.ExecuteStoredProcedure(StoredProcedureConstants_Wetcardboard_Utilities.WETCARDBOARD_UTILITIES_SP_GETLATESTACTIVEUSERTOKENBYUSERGUID, parameters);
            if (dbRes is null || dbRes.Tables.Count == 0)
            {
                return null;
            }

            return DbHelper.GetSingleObjectFromDataSet<Wetcardboard_Utilities_UserToken>(dbRes);
        }
        #endregion \ Tokens


        #region Users
        public Wetcardboard_Utilities_User? GetUserByEmail(string email)
        {
            var parameters = new List<SqlParameterWithValue>
            {
                new SqlParameterWithValue("_email", email)
            };
            var dbRes = DbConn.ExecuteStoredProcedure(StoredProcedureConstants_Wetcardboard_Utilities.WETCARDBOARD_UTILITIES_SP_GETUSERBYEMAIL, parameters);
            if (dbRes is null || dbRes.Tables.Count == 0)
            {
                return null;
            }

            return DbHelper.GetSingleObjectFromDataSet<Wetcardboard_Utilities_User>(dbRes);
        }
        public Wetcardboard_Utilities_User? GetUserByGuid(string guid)
        {
            var parameters = new List<SqlParameterWithValue>
            {
                new SqlParameterWithValue("_guid", guid)
            };
            var dbRes = DbConn.ExecuteStoredProcedure(StoredProcedureConstants_Wetcardboard_Utilities.WETCARDBOARD_UTILITIES_SP_GETUSERBYGUID, parameters);
            if (dbRes is null || dbRes.Tables.Count == 0)
            {
                return null;
            }

            return DbHelper.GetSingleObjectFromDataSet<Wetcardboard_Utilities_User>(dbRes);
        }
        #endregion \ Users
        #endregion \ IDbConn_Wetcardboard_Utilities Implementation
        #endregion \ Interface Implementations
    }
}
