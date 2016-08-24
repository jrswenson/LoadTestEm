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
    public class LoadSetTests
    {
        //[TestMethod()]
        //public void ExecuteAsyncTest()
        //{
        //    var loadSet = new LoadSet();

        //    var conStr = "Data Source=(local);Initial Catalog=LoadTestEm;Integrated Security=SSPI";
        //    var cmd = "insert into Names (Name) values (@param1)";

        //    var loadTask1 = new SqlLoadTask
        //    {
        //        ConnectionString = conStr,
        //        Command = cmd
        //    };

        //    loadTask1.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@param1", new RandomStringValueGetter(5, "One-")));
        //    loadSet.Tasks.Add(loadTask1);

        //    cmd = "select * from Names where name like 'One-%'";
        //    var loadTask2 = new SqlLoadTask
        //    {
        //        ConnectionString = conStr,
        //        Command = cmd
        //    };
        //    loadSet.Tasks.Add(loadTask2);

        //    cmd = "Update Names set Name = @newName where name like 'One-%'"; 
        //    var loadTask3 = new SqlLoadTask
        //    {
        //        ConnectionString = conStr,
        //        Command = cmd
        //    };
        //    loadTask3.CommandParameters.Add(new KeyValuePair<string, IValueGetter>("@newName", new RandomStringValueGetter(5, "Three-")));            
        //    loadSet.Tasks.Add(loadTask3);

        //    cmd = "delete from Names where name like 'Three-%'";
        //    var loadTask4 = new SqlLoadTask
        //    {
        //        ConnectionString = conStr,
        //        Command = cmd
        //    };
        //    loadSet.Tasks.Add(loadTask4);

        //    var total = Task.Run(() => loadSet.ExecuteAsync()).Result;
        //}
    }
}