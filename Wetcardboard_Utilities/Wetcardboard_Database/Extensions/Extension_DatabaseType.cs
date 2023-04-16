using MySql.Data.MySqlClient;
using System.Data;
using Wetcardboard_Database.DbTypes;

namespace Wetcardboard_Database.Extensions
{
    public static class Extension_DatabaseType
    {
        public static MySqlDbType ToMySqlDbType(this DatabaseType sqlDbType)
        {
            MySqlDbType res;

            switch (sqlDbType)
            {
                case DatabaseType.BigInt: res = MySqlDbType.Int64; break;
                case DatabaseType.Binary: res = MySqlDbType.Binary; break;
                case DatabaseType.Bit: res = MySqlDbType.Bit; break;
                case DatabaseType.Char: res = MySqlDbType.VarChar; break;
                case DatabaseType.DateTime: res = MySqlDbType.DateTime; break;
                case DatabaseType.Decimal: res = MySqlDbType.Decimal; break;
                case DatabaseType.Float: res = MySqlDbType.Float; break;
                case DatabaseType.Int: res = MySqlDbType.Int32; break;
                case DatabaseType.Text: res = MySqlDbType.Text; break;
                case DatabaseType.Timestamp: res = MySqlDbType.Timestamp; break;
                case DatabaseType.VarBinary: res = MySqlDbType.VarBinary; break;
                case DatabaseType.VarChar: res = MySqlDbType.VarChar; break;
                case DatabaseType.Date: res = MySqlDbType.Date; break;
                case DatabaseType.Time: res = MySqlDbType.Time; break;
                case DatabaseType.MediumText: res = MySqlDbType.MediumText; break;

                default: throw new NotImplementedException($"No implementation for: '{sqlDbType}'");
            }

            return res;
        }
        public static SqlDbType ToSqlDbType(this DatabaseType sqlDbType)
        {
            SqlDbType res;

            switch (sqlDbType)
            {
                case DatabaseType.BigInt: res = SqlDbType.BigInt; break;
                case DatabaseType.Binary: res = SqlDbType.Binary; break;
                case DatabaseType.Bit: res = SqlDbType.Bit; break;
                case DatabaseType.Char: res = SqlDbType.Char; break;
                case DatabaseType.DateTime: res = SqlDbType.DateTime; break;
                case DatabaseType.Decimal: res = SqlDbType.Decimal; break;
                case DatabaseType.Float: res = SqlDbType.Float; break;
                case DatabaseType.Image: res = SqlDbType.Image; break;
                case DatabaseType.Int: res = SqlDbType.Int; break;
                case DatabaseType.Money: res = SqlDbType.Money; break;
                case DatabaseType.NChar: res = SqlDbType.NChar; break;
                case DatabaseType.NText: res = SqlDbType.NText; break;
                case DatabaseType.NVarChar: res = SqlDbType.NVarChar; break;
                case DatabaseType.Real: res = SqlDbType.Real; break;
                case DatabaseType.UniqueIdentifier: res = SqlDbType.UniqueIdentifier; break;
                case DatabaseType.SmallDateTime: res = SqlDbType.SmallDateTime; break;
                case DatabaseType.SmallInt: res = SqlDbType.SmallInt; break;
                case DatabaseType.SmallMoney: res = SqlDbType.SmallMoney; break;
                case DatabaseType.Text: res = SqlDbType.Text; break;
                case DatabaseType.Timestamp: res = SqlDbType.Timestamp; break;
                case DatabaseType.TinyInt: res = SqlDbType.TinyInt; break;
                case DatabaseType.VarBinary: res = SqlDbType.VarBinary; break;
                case DatabaseType.VarChar: res = SqlDbType.VarChar; break;
                case DatabaseType.Variant: res = SqlDbType.Variant; break;
                case DatabaseType.Xml: res = SqlDbType.Xml; break;
                case DatabaseType.Udt: res = SqlDbType.Udt; break;
                case DatabaseType.Structured: res = SqlDbType.Structured; break;
                case DatabaseType.Date: res = SqlDbType.Date; break;
                case DatabaseType.Time: res = SqlDbType.Time; break;
                case DatabaseType.DateTime2: res = SqlDbType.DateTime2; break;
                case DatabaseType.DateTimeOffset: res = SqlDbType.DateTimeOffset; break;

                default: throw new NotImplementedException($"No implementation for: '{sqlDbType}'");
            }

            return res;
        }
    }
}
