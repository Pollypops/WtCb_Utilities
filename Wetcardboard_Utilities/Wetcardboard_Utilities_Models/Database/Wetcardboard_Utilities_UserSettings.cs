using Newtonsoft.Json;
using System.Data;
using Wetcardboard_Database.Models;

namespace Wetcardboard_Utilities_Models.Database
{
    public class Wetcardboard_Utilities_UserSettings : DbModelBase
    {
        #region Fields & Properties
        #region Properties
        [JsonProperty(propertyName: "created")]
        public DateTime? Created { get; set; }
        [JsonProperty(propertyName: "updated")]
        public DateTime? Updated { get; set; }

        [JsonProperty(propertyName: "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(propertyName: "setting_name")]
        public string? SettingName { get; set; }
        [JsonProperty(propertyName: "setting_value")]
        public string? SettingValue { get; set; }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region DbModelBase Implementation
        public static DbModelBase CreateFromDataRow(DataRow row)
        {
            var created = Convert.ToDateTime(row["created"]);
            var updated = Convert.ToDateTime(row["updated"]);
            var userId = Convert.ToInt32(row["user_id"]);
            var settName = Convert.ToString(row["setting_name"]);
            var settVal = Convert.ToString(row["setting_value"]);

            return new Wetcardboard_Utilities_UserSettings
            {
                Created = created,
                Updated = updated,
                UserId = userId,
                SettingName = settName,
                SettingValue = settVal 
            };
        }
        #endregion \ DbModelBase Implementation
    }
}
