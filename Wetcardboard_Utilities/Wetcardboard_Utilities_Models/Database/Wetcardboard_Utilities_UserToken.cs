using System.Data;
using System.Text.Json.Serialization;
using Wetcardboard_Database.Models;

namespace Wetcardboard_Utilities_Models.Database
{
    public class Wetcardboard_Utilities_UserToken : DbModelBase
    {
        #region Fields & Properties
        #region Properties
        [JsonPropertyName("expires")]
        public DateTime Expires { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Abstract Implementations
        #region DbModelBase Implementation
        public static DbModelBase CreateFromDataRow(DataRow row)
        {
            var id = Convert.ToInt32(row["id"]);
            var expires = Convert.ToDateTime(row["expires"]);
            var userId = Convert.ToInt32(row["user_id"]);
            var token = $"{row["token"]}";

            return new Wetcardboard_Utilities_UserToken
            {
                Id = id,
                Expires = expires,
                UserId = userId,
                Token = token
            };
        }
        #endregion \ DbModelBase Implementation
        #endregion \ Abstract Implementations
    }
}
