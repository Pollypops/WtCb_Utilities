namespace Wetcardboard_Shared.Navigation
{
    public class UrlFactory
    {
        #region Fields & Properties
        private string _baseAbsoluteFullUrl;
        private string _baseRelativeFullUrl;
        #endregion \ Fields & Properties


        #region Constructor
        public UrlFactory(string baseAbsoluteFullUrl, string baseRelativeUrl)
        {
            _baseAbsoluteFullUrl = baseAbsoluteFullUrl;
            _baseRelativeFullUrl = baseRelativeUrl;
        }
        #endregion \ Constructor


        #region Methods
        #region Public Methods
        public string CreateUrl_Absolute(string path)
        {
            var res = CreateUrl(_baseAbsoluteFullUrl, path);
            return res;
        }

        public string CreateUrl_Relative(string path)
        {
            var res = CreateUrl(_baseRelativeFullUrl, path);
            return res;
        }
        #endregion \ Public Methods

        #region Private Methods
        private string CreateUrl(string prefix, string path)
        {
            var res = prefix;
            if (!res.EndsWith("/"))
            {
                res += "/";
            }

            if (path.StartsWith("/"))
            {
                if (path.Length > 1)
                {
                    res += path.Substring(1);
                }
            }
            else
            {
                res += path;
            }

            return res;
        }
        #endregion \ Private Methods
        #endregion \ Methods
    }
}
