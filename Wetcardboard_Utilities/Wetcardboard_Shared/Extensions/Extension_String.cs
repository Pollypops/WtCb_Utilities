using System.Text;

namespace Wetcardboard_Shared.Extensions
{
    public static class Extension_String
    {
        public static string Random(this string chars, int length = 8)
        {
            var res = new StringBuilder();
            var rand = new Random();
            for(var i = 0; i < length; i++)
            {
                res.Append(chars[rand.Next(chars.Length)]);
            }
            return res.ToString();
        }
    }
}
