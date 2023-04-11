using Newtonsoft.Json.Linq;
using System.Data;
using Wetcardboard_Database.DbTypes;

namespace Wetcardboard_Database.Extensions
{
    public static class Extension_Object
    {
        public static DatabaseType ConvertToDatabaseType(this object value)
        {
            DatabaseType res;

            if (value is string) res = DatabaseType.VarChar;
            else if (value is int) res = DatabaseType.Int;
            else if (value is bool) res = DatabaseType.Bit;
            else if (value is DateTime) res = DatabaseType.DateTime;
            else if (value is decimal) res = DatabaseType.Decimal;
            else if (value is double) res = DatabaseType.Float;
            else throw new NotImplementedException($"Conversion of '{value.GetType}' not implemented");

            return res;
        }
        public static SqlDbType ConvertToDbType(this object value)
        {
            SqlDbType res;

            if (value is string) res = SqlDbType.VarChar;
            else if (value is int) res = SqlDbType.Int;
            else if (value is bool) res = SqlDbType.Bit;
            else if (value is DateTime) res = SqlDbType.DateTime;
            else if (value is decimal) res = SqlDbType.Decimal;
            else if (value is double) res = SqlDbType.Float;
            else throw new NotImplementedException($"Conversion of '{value.GetType}' not implemented");

            return res;
        }
        public static string? ToJsonString(this object obj)
        {
            string? res;

            try
            {
                res = JObject.FromObject(obj).ToString();
            }
            catch (Exception)
            {
                // TODO: Implement error handling!
                res = null;
            }

            return res;
        }
    }
}
