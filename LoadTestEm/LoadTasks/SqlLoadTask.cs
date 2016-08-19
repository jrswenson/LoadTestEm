using LoadTestEm.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{
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

        public async Task<long> ExecuteAsync()
        {            
            var task = Task.Run(() => Execute());
            var result = await task;
            
            return result;
        }

        public long Execute()
        {
            var watch = Stopwatch.StartNew();

            if (ReuseConnection)
            {
                ExecuteCommand(GetConnection());
            }
            else
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    ExecuteCommand(conn);
                }
            }

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private SqlConnection GetConnection()
        {
            if (connection == null)
                connection = new SqlConnection(ConnectionString);

            return connection;
        }

        private void ExecuteCommand(SqlConnection conn)
        {
            if (conn == null)
                throw new ArgumentException("You must provide a valid SqlConnection.");

            if (string.IsNullOrWhiteSpace(Command))
                throw new ArgumentException("You must provide a valid command.");

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
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

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                var rows = cmd.ExecuteNonQuery();
            }
        }
    }
}
