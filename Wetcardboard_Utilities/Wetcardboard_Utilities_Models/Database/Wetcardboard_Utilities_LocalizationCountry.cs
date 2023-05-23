using Newtonsoft.Json;
using System.Data;
using Wetcardboard_Database.Models;

namespace Wetcardboard_Utilities_Models.Database
{
    public class Wetcardboard_Utilities_LocalizationCountry : DbModelBase
    {
        #region Fields & Properties
        #region Properties
        [JsonProperty(propertyName: "id")]
        public int Id { get; set; }

        [JsonProperty(propertyName: "country")]
        public string Country { get; set; } = string.Empty;
        [JsonProperty(propertyName: "country_code_2")]
        public string CountryCode2 { get; set; } = string.Empty;
        [JsonProperty(propertyName: "country_code_3")]
        public string CountryCode3 { get; set; } = string.Empty;
        [JsonProperty(propertyName: "culture_info_code")]
        public string CultureInfoCode { get; set; } = string.Empty;
        [JsonProperty(propertyName: "language")]
        public string Language { get; set; } = string.Empty;
        [JsonProperty(propertyName: "lang_code_2")]
        public string LangCode2 { get; set; } = string.Empty;
        [JsonProperty(propertyName: "lang_code_3")]
        public string LangCode3 { get; set; } = string.Empty;
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Abstract Implementations
        #region DbModelBase Implementation
        public static DbModelBase CreateFromDataRow(DataRow row)
        {
            var id = Convert.ToInt32(row["id"]);
            var country = Convert.ToString(row["country"]) ?? "";
            var country_code_2 = Convert.ToString(row["country_code_2"]) ?? "";
            var country_code_3 = Convert.ToString(row["country_code_3"]) ?? "";
            var culture_info_code = Convert.ToString(row["culture_info_code"]) ?? "";
            var language = Convert.ToString(row["language"]) ?? "";
            var lang_code_2 = Convert.ToString(row["lang_code_2"]) ?? "";
            var lang_code_3 = Convert.ToString(row["lang_code_3"]) ?? "";

            return new Wetcardboard_Utilities_LocalizationCountry
            {
                Id = id,
                Country = country,
                CountryCode2 = country_code_2,
                CountryCode3 = country_code_3,
                CultureInfoCode = culture_info_code,
                Language = language,
                LangCode2 = lang_code_2,
                LangCode3 = lang_code_3
            };
        }
        #endregion \ DbModelBase Implementation
        #endregion \ Abstract Implementations
    }
}
