using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoadTestEm.LoadSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadTestEm.LoadTasks;
using LoadTestEm.ValueGetters;

namespace LoadTestEm.LoadSets.Tests
{
    [TestClass()]
    public class LoadGroupTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            var conStr = "Data Source=u7ksu7yxs0.database.windows.net;Database=SharedEpisodeTracker;Integrated Security=False;User ID=ETDev;Password=d3v3lupm!nt;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (var set1 = new SqlLoadSet { Identifier = "With Views", ConnectionString = conStr })
            using (var set2 = new SqlLoadSet { Identifier = "With Tables", ConnectionString = conStr })
            using (var group = new LoadGroup { Identifier = "The Group", Rounds = 2 })
            {
                set1.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));
                set1.Tasks.Add(new SqlLoadTask { Identifier = "DataPointView", Command = "select * from DataPointView where ProfileID = @profileId " });
                set1.Tasks.Add(new SqlLoadTask { Identifier = "NativeDataPointView", Command = "select * from NativeDataPointView where ProfileID = @profileId" });
                set1.Tasks.Add(new SqlLoadTask { Identifier = "ComputedDataPointView", Command = "select * from ComputedDataPointView where ProfileID = @profileId" });
                group.Sets.Add(set1);

                set2.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));
                var cmd1 = new StringBuilder("select * from QuestionnaireResult where ProfileID = @profileId ");
                var cmd2 = new StringBuilder(cmd1.ToString());
                cmd2.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 0)");
                var cmd3 = new StringBuilder(cmd1.ToString());
                cmd3.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 1)");

                set2.Tasks.Add(new SqlLoadTask { Identifier = "All", Command = cmd1.ToString() });
                set2.Tasks.Add(new SqlLoadTask { Identifier = "Computed = 0", Command = cmd2.ToString() });
                set2.Tasks.Add(new SqlLoadTask { Identifier = "Computed = 1", Command = cmd3.ToString() });
                group.Sets.Add(set2);

                var result = group.Execute();
            }

        }

        [TestMethod()]
        public void ExecuteAsyncTest()
        {
            var conStr = "Data Source=u7ksu7yxs0.database.windows.net;Database=SharedEpisodeTracker;Integrated Security=False;User ID=ETDev;Password=d3v3lupm!nt;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (var set1 = new SqlLoadSet { Identifier = "With Views", ConnectionString = conStr })
            using (var set2 = new SqlLoadSet { Identifier = "With Tables", ConnectionString = conStr })
            using (var group = new LoadGroup { Identifier = "The Group", Rounds = 2 })
            {
                set1.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));
                set1.Tasks.Add(new SqlLoadTask { Identifier = "DataPointView", Command = "select * from DataPointView where ProfileID = @profileId " });
                set1.Tasks.Add(new SqlLoadTask { Identifier = "NativeDataPointView", Command = "select * from NativeDataPointView where ProfileID = @profileId" });
                set1.Tasks.Add(new SqlLoadTask { Identifier = "ComputedDataPointView", Command = "select * from ComputedDataPointView where ProfileID = @profileId" });
                group.Sets.Add(set1);

                set2.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));
                var cmd1 = new StringBuilder("select * from QuestionnaireResult where ProfileID = @profileId ");
                var cmd2 = new StringBuilder(cmd1.ToString());
                cmd2.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 0)");
                var cmd3 = new StringBuilder(cmd1.ToString());
                cmd3.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 1)");

                set2.Tasks.Add(new SqlLoadTask { Identifier = "All", Command = cmd1.ToString() });
                set2.Tasks.Add(new SqlLoadTask { Identifier = "Computed = 0", Command = cmd2.ToString() });
                set2.Tasks.Add(new SqlLoadTask { Identifier = "Computed = 1", Command = cmd3.ToString() });
                group.Sets.Add(set2);

                var result = group.ExecuteAsync().Result;
            }
        }
    }
}