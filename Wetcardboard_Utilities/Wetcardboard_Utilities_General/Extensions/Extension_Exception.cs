namespace Wetcardboard_General.Extensions
{
    public static class Extension_Exception
    {
        public static string GetFullExceptionMessage(this Exception ex)
        {
            var res = string.Empty;
            if (ex != null)
            {
                res = $"{ex.Message}{Environment.NewLine}{Environment.NewLine}";
                if (ex.InnerException != null)
                {
                    res += GetFullExceptionMessage(ex.InnerException);
                }
            }
            return res;
        }
    }
}
