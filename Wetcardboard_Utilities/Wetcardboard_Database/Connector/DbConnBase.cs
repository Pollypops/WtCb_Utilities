namespace Wetcardboard_Database.Connector
{
    public class DbConnBase
    {
        private bool OverrideStaticConnectionString { get; set; }
        private static string? _staticConnectionString;
        private string _connectionString = string.Empty;
        protected string ConnectionString
        {
            get
            {
                string res;
                if (!OverrideStaticConnectionString && !string.IsNullOrEmpty(_staticConnectionString))
                {
                    res = _staticConnectionString;
                }
                else
                {
                    res = _connectionString;
                }

                return res;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static void SetStaticConnectionString(string connStr)
        {
            _staticConnectionString = connStr;
        }
        public void SetOverrideStaticConnectionString(bool overrideVal)
        {
            OverrideStaticConnectionString = overrideVal;
        }
    }
}
