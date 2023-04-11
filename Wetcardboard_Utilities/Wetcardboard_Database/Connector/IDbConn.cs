using System.Data;
using Wetcardboard_Database.Parameters;

namespace Wetcardboard_Database.Connector
{
    public interface IDbConn
    {
        DataSet? Execute(string query, IEnumerable<SqlParameterWithValue> parameters);
        int ExecuteNonQuery(string query, IEnumerable<SqlParameterWithValue> parameters);

        DataSet? ExecuteStoredProcedure(string spName, IEnumerable<SqlParameterWithValue>? parameters = null);
        int ExecuteStoredProcedureNonQuery(string spName, IEnumerable<SqlParameterWithValue>? parameters = null);

        void SetConnectionString(string connStr);

        DataRow? GetDataRow(DataSet ds);
        IEnumerable<DataRow> GetDataRows(DataSet ds, int rowCount);
        IEnumerable<SqlParameterWithValue> CreateSqlParametersWithValues(params KeyValuePair<string, object>[] values);
    }
}
