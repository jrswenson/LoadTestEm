using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Trace;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestEm
{
    class TraceReader : IDisposable
    {
        //private SqlConnectionInfo connectionInfo;
        private TraceServer trace = new TraceServer();

        public TraceReader()
        {
            var conn = new SqlConnectionInfo();
            conn.DatabaseName = "LoadTestEm";
            conn.UseIntegratedSecurity = true;

            trace.InitializeAsReader(conn, @"C:\SqlTrace\LoadTest.trc");
        }

        public void Read()
        {
            Task.Factory.StartNew(() =>
            {
                while (trace.Read())
                {
                    Debug.WriteLine($"Event: " + trace["EventClass"]);

                    Debug.WriteLine("SPID: " + trace["SPID"]);

                    //Debug.WriteLine("Login: " + trace["LoginName"]);

                    //Debug.WriteLine("Object: " + trace["ObjectName"]);

                    Debug.WriteLine("Text: " + trace["TextData"]);

                    Debug.WriteLine("");
                }
            });
        }

        public void Stop()
        {
            trace.Stop();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (trace != null)
                    {
                        trace.Close();
                        trace = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
