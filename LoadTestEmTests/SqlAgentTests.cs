using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoadTestEm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadTestEm.ValueGetters;
using LoadTestEm.Agents;

namespace LoadTestEm.Tests
{
    [TestClass()]
    public class SqlAgentTests
    {
        [TestMethod()]
        public void ExecuteAsyncTest()
        {
            var agent = new SqlAgent
            {
                ConnectionString = "Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI",
                Command = "select * from Names"
            };

            var result = Task.Run(() => agent.ExecuteAsync()).Result;
            Assert.IsTrue(result > 0);
        }

        [TestMethod()]
        public void ExecuteAsyncTest2()
        {
            var agent = new SqlAgent
            {
                ConnectionString = "Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI",
                Command = "select * from Names"
            };

            var total = 0L;
            for (var i = 0; i < 5; i++)
            {
                total += Task.Run(() => agent.ExecuteAsync()).Result;
            }

            agent = new SqlAgent
            {
                ConnectionString = "Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI",
                ReuseConnection = false,
                Command = "select * from Names"
            };

            total = 0L;
            for (var i = 0; i < 5; i++)
            {
                total += Task.Run(() => agent.ExecuteAsync()).Result;
            }

            Assert.IsTrue(total > 0);
        }

        [TestMethod()]
        public void ExecuteAsyncInsertTest()
        {
            var conStr = "Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI";
            var cmd = "insert into Names (Name) values (@param1)";

            var agent = new SqlAgent
            {
                ConnectionString = conStr,
                Command = cmd
            };

            agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@param1", new StringValueGetter("amy")));

            var total = 0L;
            for (var i = 0; i < 5; i++)
            {
                total += Task.Run(() => agent.ExecuteAsync()).Result;
            }
        }

        [TestMethod()]
        public void ExecuteAsyncUpdateTest()
        {
            var conStr = "Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI";
            var cmd = "Update Names set Name = @newName where name = @oldName";

            var agent = new SqlAgent
            {
                ConnectionString = conStr,
                Command = cmd
            };

            agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@newName", new StringValueGetter("amy")));
            agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@oldName", new StringValueGetter("jon")));

            var total = 0L;
            for (var i = 0; i < 5; i++)
            {
                total += Task.Run(() => agent.ExecuteAsync()).Result;
            }
        }

        [TestMethod()]
        public void ExecuteAsyncDeleteTest()
        {
            var conStr = "Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI";
            var cmd = "Delete from Names where name = @name";

            var agent = new SqlAgent
            {
                ConnectionString = conStr,
                Command = cmd
            };

            agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@name", new StringValueGetter("amy")));

            var total = 0L;
            for (var i = 0; i < 5; i++)
            {
                total += Task.Run(() => agent.ExecuteAsync()).Result;
            }
        }
    }
}