using Wetcardboard_Database.DbTypes;
using Wetcardboard_Database.Extensions;

namespace Wetcardboard_Database.Parameters
{
    public class SqlParameterWithValue
    {
        #region Fields & Properties
        public object? Value { get; set; }
        public DatabaseType Type { get; set; }
        public string Name { get; set; }
        #endregion \ Fields & Properties


        #region Constructor
        public SqlParameterWithValue(string name, object value)
        {
            Name = name;
            Type = value.ConvertToDatabaseType();
            Value = value;
        }
        public SqlParameterWithValue(string name, DatabaseType type, object? value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
        #endregion \ Constructor
    }
}
