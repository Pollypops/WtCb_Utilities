using System.Data;
using Wetcardboard_Database.Models;

namespace Wetcardboard_Database.Helpers
{
    public static class DbHelper
    {
        /// <summary>
        /// Return object of class type T.<para/>
        /// This function expects no more than a single row present and will use the first available row.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public static T? GetSingleObjectFromDataSet<T>(DataSet dataSet) where T : DbModelBase
        {
            if (dataSet.Tables.Count == 0)
            {
                return default;
            }

            var tbl = dataSet.Tables[0];
            if (tbl is null || tbl.Rows.Count == 0)
            {
                return default;
            }

            var row = tbl.Rows[0];
            if (row is null || row.ItemArray is null || row.ItemArray.Length == 0)
            {
                return default;
            }

            return (T)T.CreateFromDataRow(row);
        }

        /// <summary>
        /// Return IEnumerable with content of type T. <para/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetObjectsDataSet<T>(DataSet dataSet) where T : DbModelBase
        {
            var res = new List<T>();

            if (dataSet.Tables.Count == 0)
            {
                return res;
            }

            var tbl = dataSet.Tables[0];
            if (tbl is null || tbl.Rows.Count == 0)
            {
                return res;
            }

            foreach(DataRow row in tbl.Rows)
            {
                var tObj = (T)T.CreateFromDataRow(row);
                res.Add(tObj);
            }

            return res;
        }
    }
}
