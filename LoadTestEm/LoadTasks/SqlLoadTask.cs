using LoadTestEm.ValueGetters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{
    public struct SqlLoadResult : ILoadResult
    {
        public long ExecutionTime { get; set; }
        public int RowsReturned { get; set; }
        public int RowsAffected { get; set; }

        public IDictionary Statistics { get; set; }
    }

    public class SqlLoadTask : ILoadTask, IDisposable
    {
        private SqlConnection connection;
        private bool reuseConnection = true;

        public string ConnectionString { get; set; }

        public bool ReuseConnection
        {
            get
            {
                return reuseConnection;
            }
            set
            {
                reuseConnection = value;
            }
        }

        public string Command { get; set; }


        private ICollection<KeyValuePair<string, IValueGetter>> _commandParameters = new List<KeyValuePair<string, IValueGetter>>();
        public ICollection<KeyValuePair<string, IValueGetter>> CommandParameters
        {
            get
            {
                return _commandParameters;
            }
            set
            {
                _commandParameters = value;
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception)
                {
                    //Ignore
                }
                finally
                {
                    connection = null;
                }
            }
        }

        public async Task<ILoadResult> ExecuteAsync()
        {
            var task = Task.Run(() => Execute());
            var result = await task;

            return result;
        }

        public ILoadResult Execute()
        {
            SqlLoadResult result;
            if (ReuseConnection)
            {
                result = ExecuteCommand(GetConnection());
            }
            else
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    result = ExecuteCommand(conn);
                }
            }

            return result;
        }

        private SqlConnection GetConnection()
        {
            if (connection == null)
                connection = new SqlConnection(ConnectionString);

            return connection;
        }

        private SqlLoadResult ExecuteCommand(SqlConnection conn)
        {
            if (conn == null)
                throw new ArgumentException("You must provide a valid SqlConnection.");

            if (string.IsNullOrWhiteSpace(Command))
                throw new ArgumentException("You must provide a valid command.");

            conn.StatisticsEnabled = true;
            conn.ResetStatistics();

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                var firstWord = Command.Split(null).FirstOrDefault().ToUpper();
                string[] sqlCommands = { "SELECT", "INSERT", "DELETE", "UPDATE" };

                cmd.CommandType = sqlCommands.Contains(firstWord) ? CommandType.Text : CommandType.StoredProcedure;
                cmd.CommandText = Command;

                //if (true)
                //    cmd.CommandText += " OPTION (OPTIMIZE FOR UNKNOWN)";

                if (_commandParameters.Any())
                {
                    foreach (var item in _commandParameters)
                    {
                        var key = item.Key;
                        var val = item.Value.Value;
                        cmd.Parameters.AddWithValue(key, val);
                    }
                }

                var watch = Stopwatch.StartNew();

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var result = new SqlLoadResult();
                SqlDataReader reader = null;
                switch (firstWord)
                {
                    case "SELECT":
                        reader = cmd.ExecuteReader();
                        break;
                    default:
                        result.RowsAffected = cmd.ExecuteNonQuery();
                        break;
                }

                watch.Stop();

                result.Statistics = conn.RetrieveStatistics();

                result.ExecutionTime = watch.ElapsedMilliseconds;

                if (reader != null)
                {
                    var rows = 0;
                    while (reader.Read())
                    {
                        rows++;
                    }
                    result.RowsReturned = rows;
                    reader.Close();
                    reader = null;
                }

                result.Statistics = conn.RetrieveStatistics();

                return result;
            }
        }
    }
}
