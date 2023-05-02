using System.Data;
using System.Text.Json.Serialization;
using Wetcardboard_Database.Models;

namespace Wetcardboard_Utilities_Models.Database
{
    public class Wetcardboard_Utilities_User : DbModelBase
    {
        #region Fields & Properties
        #region Properties
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("user_country_language_id")]
        public int UserCountryLanguageId { get; set; }
        [JsonPropertyName("user_role_id")]
        public int UserRoleId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("guid")]
        public string Guid { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("user_role")]
        public string UserRole { get; set; } = string.Empty;
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Abstract Implementations
        #region DbModelBase Implementation
        public static DbModelBase CreateFromDataRow(DataRow row)
        {
            var id = Convert.ToInt32(row["id"]);
            var guid = Convert.ToString(row["guid"]) ?? "";
            var created = Convert.ToDateTime(row["created"]);
            var updated = Convert.ToDateTime(row["updated"]);
            var login = Convert.ToString(row["login"]) ?? "";
            var firstName = Convert.ToString(row["first_name"]) ?? "";
            var lastName = Convert.ToString(row["last_name"]) ?? "";
            var userRoleId = Convert.ToInt32(row["user_role_id"]);
            var userRole = Convert.ToString(row["user_role"]) ?? "";

            return new Wetcardboard_Utilities_User
            {
                Id = id,
                Guid = guid,
                Created = created,
                Updated = updated,
                Login = login,
                FirstName = firstName,
                LastName = lastName,
                UserRoleId = userRoleId,
                UserRole = userRole
            };
        }
        #endregion \ DbModelBase Implementation
        #endregion \ Abstract Implementations
    }
}
