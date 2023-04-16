using MySql.Data.MySqlClient;
using System.Data;
using Wetcardboard_Database.Extensions;
using Wetcardboard_Database.Parameters;

namespace Wetcardboard_Database.Connector
{
    public class DbConn_MySql : DbConnBase, IDbConn
    {
        #region Functions
        private DataSet GetDataSet(MySqlCommand command)
        {
            var res = new DataSet();
            using var adapter = new MySqlDataAdapter(command);
            adapter.Fill(res);
            return res;
        }
        public DataSet? Exec(string commandText, CommandType commandType, IEnumerable<SqlParameterWithValue>? parameters = null)
        {
            DataSet? res;

            try
            {
                using var conn = GetConnection();
                using var command = CreateCommand(conn, commandText, commandType, parameters);
                res = GetDataSet(command);
            }
            catch (Exception)
            {
                // TODO: Implement error handling!
                res = null;
            }

            return res;
        }
        private int ExecNonQuery(string commandText, CommandType commandType, IEnumerable<SqlParameterWithValue>? parameters = null)
        {
            int res;

            try
            {
                using var conn = GetConnection();
                conn.Open();
                using var command = CreateCommand(conn, commandText, commandType, parameters);
                res = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // TODO: Implement error handling!
                throw;
                res = -1;
            }

            return res;
        }
        private MySqlCommand CreateCommand(MySqlConnection conn, string commandText, CommandType commandType)
        {
            var res = conn.CreateCommand();
            res.CommandText = commandText;
            res.CommandType = commandType;
            return res;
        }
        private MySqlCommand CreateCommand(MySqlConnection conn, string commandText, CommandType commandType, IEnumerable<SqlParameterWithValue>? parameters)
        {
            var res = CreateCommand(conn, commandText, commandType);
            AddParametersToCommand(res, parameters);
            return res;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        private void AddParametersToCommand(MySqlCommand command, IEnumerable<SqlParameterWithValue>? parameters)
        {
            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter.Name, parameter.Type.ToMySqlDbType()).Value = parameter.Value;
                }
            }
        }
        #endregion \ Functions


        #region Interface Implementations
        #region IDbConn Implementation
        public DataSet? Execute(string query, IEnumerable<SqlParameterWithValue>? parameters = null)
        {
            return Exec(query, CommandType.Text, parameters);
        }
        public int ExecuteNonQuery(string query, IEnumerable<SqlParameterWithValue>? parameters = null)
        {
            return ExecNonQuery(query, CommandType.Text, parameters);
        }

        public DataSet? ExecuteStoredProcedure(string spName, IEnumerable<SqlParameterWithValue>? parameters = null)
        {
            return Exec(spName, CommandType.StoredProcedure, parameters);
        }
        public int ExecuteStoredProcedureNonQuery(string spName, IEnumerable<SqlParameterWithValue>? parameters = null)
        {
            return ExecNonQuery(spName, CommandType.StoredProcedure, parameters);
        }

        public void SetConnectionString(string connStr)
        {
            ConnectionString = connStr;
        }

        /// <summary>
        /// Returns the only row from <paramref name="ds"/> otherwise returns null.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataRow? GetDataRow(DataSet ds)
        {
            if (ds is null || ds.Tables.Count == 0)
            {
                // TODO: Implement error handling!
                return null;
            }
            var tbl = ds.Tables[0];
            if (tbl.Rows.Count == 0)
            {
                // TODO: Implement error handling!
                return null;
            }
            var row = tbl.Rows[0];
            return row;
        }
        /// <summary>
        /// Return datarows of <paramref name="ds"/> assuming only 1 table exists or empty container.<para/>
        /// If <paramref name="rowCount"/> > 0 will enforce strict row count.<para/>
        ///  - Empty container will be returned if row count of <paramref name="ds"/> does not match <paramref name="rowCount"/>
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public IEnumerable<DataRow> GetDataRows(DataSet ds, int rowCount)
        {
            var res = new List<DataRow>();
            if (ds is null)
            {
                // TODO: Implement error handling!
                return res;
            }
            if (ds.Tables.Count == 0)
            {
                // TODO: Implement error handling!
                return res;
            }
            var tbl = ds.Tables[0];
            var strictRowCount = rowCount > 0;
            if (strictRowCount && tbl.Rows.Count < rowCount)
            {
                // TODO: Implement error handling!
                return res;
            }
            foreach (DataRow row in tbl.Rows)
            {
                res.Add(row);
            }
            return res;
        }
        public IEnumerable<SqlParameterWithValue> CreateSqlParametersWithValues(params KeyValuePair<string, object>[] values)
        {
            var sqlParams = new List<SqlParameterWithValue>();
            foreach (var value in values)
            {
                var newParams = new SqlParameterWithValue(value.Key, value.Value);
                sqlParams.Add(newParams);
            }
            return sqlParams;
        }
        #endregion \ IDbConn Implementation
        #endregion \ Interface Implementations
    }
}
