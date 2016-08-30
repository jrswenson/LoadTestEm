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
    public class SqlLoadResult : LoadResult
    {
        public string Command { get; set; }
        public int ConnectionHashCode { get; set; }
    }

    public class SqlLoadTask : ILoadTask, IDisposable
    {
        private SqlConnection connection;
        private string _identifier = string.Empty;

        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public string ConnectionString { get; set; }

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
            LoadResult result;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                result = ExecuteCommand(conn);
            }

            return result;
        }

        static readonly object _connLock = new object();
        private LoadResult ExecuteCommand(SqlConnection conn)
        {
            if (conn == null)
                throw new ArgumentException("You must provide a valid SqlConnection.");

            if (string.IsNullOrWhiteSpace(Command))
                throw new ArgumentException("You must provide a valid command.");

            conn.StatisticsEnabled = true;
            conn.ResetStatistics();

            using (var cmd = new SqlCommand { Connection = conn })
            {
                var firstWord = Command.Split(null).FirstOrDefault().ToUpper();
                string[] sqlCommands = { "SELECT", "INSERT", "DELETE", "UPDATE" };

                cmd.CommandType = sqlCommands.Contains(firstWord) ? CommandType.Text : CommandType.StoredProcedure;
                cmd.CommandText = Command;

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

                var result = new SqlLoadResult() { Identifier = Identifier, Command = Command, ConnectionHashCode = conn.GetHashCode() };
                switch (firstWord)
                {
                    case "SELECT":
                        using (var reader = cmd.ExecuteReader())
                        {
                            watch.Stop();

                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            //result.RowsReturned = dataTable.Rows.Count;
                        }
                        break;
                    default:
                        //result.RowsAffected = cmd.ExecuteNonQuery();
                        watch.Stop();
                        break;
                }

                result.ExecutionTime = watch.ElapsedMilliseconds;
                result.Diagnostics = conn.RetrieveStatistics();
                return result;
            }
        }
    }
}
