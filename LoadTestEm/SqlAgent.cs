using LoadTestEm;
using LoadTestEm.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestEm
{
    public class SqlAgent : IAgent, IDisposable
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
            var watch = Stopwatch.StartNew();

            var task = Task.Factory.StartNew(() => Execute());
            await task;

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private void Execute()
        {
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
                        cmd.Parameters.AddWithValue(item.Key, item.Value.Value);
                    }
                }

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                var rows = cmd.ExecuteNonQuery();
            }
        }
    }
}
