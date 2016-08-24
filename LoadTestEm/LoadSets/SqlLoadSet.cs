using LoadTestEm.LoadTasks;
using LoadTestEm.ValueGetters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LoadTestEm.LoadSets
{
    public class SqlLoadSet : LoadSet
    {
        private SqlConnection connection;
        private bool reuseConnection = true;
        private ICollection<KeyValuePair<string, IValueGetter>> _commandParameters = new List<KeyValuePair<string, IValueGetter>>();
        private ICollection<IDictionary> _allStatistics = new List<IDictionary>();

        private SqlConnection GetConnection()
        {
            if (connection == null)
                connection = new SqlConnection(ConnectionString);

            return connection;
        }

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

        public ICollection<IDictionary> AllStatistics
        {
            get
            {
                return _allStatistics;
            }
            set
            {
                _allStatistics = value;
            }
        }

        public override long Execute()
        {
            var result = 0L;

            foreach (SqlLoadTask task in Tasks)
            {
                if (string.IsNullOrWhiteSpace(task.ConnectionString))
                    task.ConnectionString = ConnectionString;

                if (task.CommandParameters.Any() == false && CommandParameters.Any())
                {
                    foreach (var parameter in CommandParameters)
                    {
                        task.CommandParameters.Add(parameter);
                    }
                }

                var taskResult = task.Execute();
                result += taskResult.ExecutionTime;

                _allStatistics.Add(taskResult.Statistics);
            }

            return result;
        }

        public override void Dispose()
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
    }
}
