using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTestEm
{
    public class Loader
    {

        public void TestEm()
        {
            using (var trace = new TraceReader())
            {
                trace.Read();

                var tasks = new List<Task>();
                var agents = new List<SqlAgent>();

                foreach (var agent in agents)
                {
                    tasks.Add(agent.ExecuteAsync());
                }

                Task.WhenAll(tasks.ToArray());
                trace.Stop();
            }

            //var sw = new Stopwatch();
            //var minute = new TimeSpan(0, 1, 10);
            //sw.Start();
            //while (sw.Elapsed < minute)
            //{
            //    Thread.Sleep(5000);
            //}
        }

        //private void Blah(string t)
        //{
        //    using (var conn = new SqlConnection("Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI"))
        //    {
        //        conn.Open();
        //        var cmd = new SqlCommand("select * from Names", conn);

        //        using (var rdr = cmd.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                Console.WriteLine(rdr[0]);
        //            }
        //        }
        //    }

        //    //mre.Set();
        //}
    }
}
