using LoadTestEm.LoadTasks;
using LoadTestEm.ValueGetters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

        static readonly object _paramsLock = new object();
        public override ISetResult Execute()
        {
            foreach (SqlLoadTask task in Tasks)
            {
                if (string.IsNullOrWhiteSpace(task.ConnectionString))
                    task.ConnectionString = ConnectionString;

                lock (_paramsLock)
                {
                    if (task.CommandParameters.Any() == false && CommandParameters.Any())
                    {
                        foreach (var parameter in CommandParameters)
                        {
                            task.CommandParameters.Add(parameter);
                        }
                    }
                }                
            }

            return base.Execute();
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
