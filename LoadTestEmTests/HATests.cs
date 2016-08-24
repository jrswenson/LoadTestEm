using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using LoadTestEm.LoadTasks;
using LoadTestEm.ValueGetters;
using System.Diagnostics;
using LoadTestEm.LoadSets;

namespace LoadTestEm.Tests
{
    [TestClass()]
    public class HATests
    {
        private string conStr = "Data Source=u7ksu7yxs0.database.windows.net;Database=SharedEpisodeTracker;Integrated Security=False;User ID=ETDev;Password=d3v3lupm!nt;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //[TestMethod()]
        //public void TestEmTest1()
        //{
        //    var agent = new SqlLoadTask
        //    {
        //        ConnectionString = conStr,
        //        Command = "select * from DataPointView where ProfileID = @profileId"
        //    };

        //    agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));

        //    var viewResult = agent.Execute();
        //    Trace.WriteLine($"1");
        //    Trace.WriteLine($"Results from  view {viewResult.ExecutionTime}");

        //    agent.Command = "select * from QuestionnaireResult where ProfileID = @profileId";
        //    var tableResult = agent.Execute();
        //    Trace.WriteLine($"Results from  table {tableResult.ExecutionTime}");
        //}

        //[TestMethod()]
        //public void TestEmTest2()
        //{
        //    var agent = new SqlLoadTask
        //    {
        //        ConnectionString = "Data Source=u7ksu7yxs0.database.windows.net;Database=SharedEpisodeTracker;Integrated Security=False;User ID=ETDev;Password=d3v3lupm!nt;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
        //        Command = "select * from DataPointView where ProfileID = @profileId",
        //        ReuseConnection = true
        //    };

        //    agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));

        //    var viewResult = agent.Execute();

        //    Trace.WriteLine($"2");
        //    Trace.WriteLine($"Results from  view {viewResult.ExecutionTime}");

        //    viewResult = agent.Execute();

        //    Trace.WriteLine($"Results from  view {viewResult.ExecutionTime}");

        //    agent.Command = "select * from QuestionnaireResult where ProfileID = @profileId";
        //    var tableResult = agent.Execute();
        //    Trace.WriteLine($"Results from  table {tableResult.ExecutionTime}");

        //    tableResult = agent.Execute();
        //    Trace.WriteLine($"Results from  table {tableResult.ExecutionTime}");
        //}

        //[TestMethod()]
        //public void TestEmTest3()
        //{
        //    var agent = new SqlLoadTask
        //    {
        //        ConnectionString = conStr,
        //        Command = "select * from NativeDataPointView where ProfileID = @profileId",
        //        ReuseConnection = true
        //    };

        //    agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));

        //    var viewResult = agent.Execute();

        //    Trace.WriteLine($"3");
        //    Trace.WriteLine($"Results from  view {viewResult.ExecutionTime}");

        //    viewResult = agent.Execute();
        //    Trace.WriteLine($"Results from  view {viewResult.ExecutionTime}");

        //    var commandBuilder = new StringBuilder("select * from QuestionnaireResult where ProfileID = @profileId ");
        //    commandBuilder.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 0)");
        //    agent.Command = commandBuilder.ToString();

        //    var tableResult = agent.Execute();
        //    Trace.WriteLine($"Results from  table {tableResult.ExecutionTime}");

        //    tableResult = agent.Execute();
        //    Trace.WriteLine($"Results from  table {tableResult.ExecutionTime}");
        //}

        //[TestMethod()]
        //public void TestEmTest4()
        //{
        //    var agent = new SqlLoadTask
        //    {
        //        ConnectionString = conStr,
        //        Command = "select * from ComputedDataPointView where ProfileID = @profileId",
        //        ReuseConnection = true
        //    };

        //    agent.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));

        //    var viewResult = agent.Execute();

        //    Trace.WriteLine($"4");
        //    Trace.WriteLine($"Results from  view {viewResult.ExecutionTime}");

        //    viewResult = agent.Execute();
        //    Trace.WriteLine($"Results from  view {viewResult.ExecutionTime}");

        //    var commandBuilder = new StringBuilder("select * from QuestionnaireResult where ProfileID = @profileId ");
        //    commandBuilder.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 1)");
        //    agent.Command = commandBuilder.ToString();

        //    var tableResult = agent.Execute();
        //    Trace.WriteLine($"Results from  table {tableResult.ExecutionTime}");

        //    tableResult = agent.Execute();
        //    Trace.WriteLine($"Results from  table {tableResult.ExecutionTime}");
        //}

        //[TestMethod()]
        //public void TestEmTest5()
        //{
        //    using (var set = new SqlLoadSet { ConnectionString = conStr })
        //    {
        //        set.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));
        //        var cmd1 = new StringBuilder("select * from QuestionnaireResult where ProfileID = @profileId ");
        //        var cmd2 = new StringBuilder(cmd1.ToString());
        //        cmd2.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 0)");
        //        var cmd3 = new StringBuilder(cmd1.ToString());
        //        cmd3.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 1)");

        //        set.Tasks.Add(new SqlLoadTask { Command = cmd1.ToString() });
        //        set.Tasks.Add(new SqlLoadTask { Command = cmd2.ToString() });
        //        set.Tasks.Add(new SqlLoadTask { Command = cmd3.ToString() });

        //        var result = set.Execute();
        //    }
        //}

        //[TestMethod()]
        //public void TestEmTest6()
        //{
        //    using (var set = new SqlLoadSet())
        //    {
        //        set.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));

        //        var cmd1 = new StringBuilder("select * from QuestionnaireResult where ProfileID = @profileId ");
        //        var cmd2 = new StringBuilder(cmd1.ToString());
        //        cmd2.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 0)");
        //        var cmd3 = new StringBuilder(cmd1.ToString());
        //        cmd3.AppendLine("and [QuestionnaireTypeID] in (SELECT [ID] FROM [dbo].[QuestionnaireType] where[IsComputed] = 1)");

        //        set.Tasks.Add(new SqlLoadTask { Command = cmd1.ToString(), ConnectionString = conStr, ReuseConnection = true });
        //        set.Tasks.Add(new SqlLoadTask { Command = cmd2.ToString(), ConnectionString = conStr, ReuseConnection = true });
        //        set.Tasks.Add(new SqlLoadTask { Command = cmd3.ToString(), ConnectionString = conStr, ReuseConnection = true });

        //        var result = set.Execute();
        //    }
        //}

        //[TestMethod()]
        //public void TestEmTest7()
        //{
        //    var set = new SqlLoadSet { ConnectionString = conStr };
        //    set.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));

        //    set.Tasks.Add(new SqlLoadTask { Command = "select * from DataPointView where ProfileID = @profileId " });
        //    set.Tasks.Add(new SqlLoadTask { Command = "select * from NativeDataPointView where ProfileID = @profileId" });
        //    set.Tasks.Add(new SqlLoadTask { Command = "select * from ComputedDataPointView where ProfileID = @profileId" });

        //    var result = set.Execute();
        //}

        //[TestMethod()]
        //public void TestEmTest8()
        //{
        //    using (var set = new SqlLoadSet())
        //    {
        //        set.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@profileId", new StringValueGetter("170FD672-7B0B-4A97-B0A7-6B2FADD148D4")));

        //        set.Tasks.Add(new SqlLoadTask { Command = "select * from DataPointView where ProfileID = @profileId", ConnectionString = conStr });
        //        set.Tasks.Add(new SqlLoadTask { Command = "select * from NativeDataPointView where ProfileID = @profileId", ConnectionString = conStr });
        //        set.Tasks.Add(new SqlLoadTask { Command = "select * from ComputedDataPointView where ProfileID = @profileId", ConnectionString = conStr });

        //        var result = set.Execute();
        //    }
        //}
    }
}