using LoadTestEm.LoadTasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LoadTestEm.LoadSets
{
    public interface ILoadSet : IDisposable
    {
        string Identifier { get; set; }

        ICollection<ILoadTask> Tasks { get; set; }

        ISetResult Execute();

        Task<ISetResult> ExecuteAsync();
    }

    public class LoadSet : ILoadSet
    {
        private ICollection<ILoadTask> _tasks = new List<ILoadTask>();
        public ICollection<ILoadTask> Tasks
        {
            get { return _tasks; }

            set { _tasks = value; }
        }

        public virtual void Dispose() { }

        private string _identifier = string.Empty;
        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public virtual ISetResult Execute()
        {
            var result = new SetResult() { Identifier = Identifier };

            var sw = new Stopwatch();
            sw.Start();
            foreach (var task in Tasks)
            {
                var execute = task.Execute();
                result.LoadTaskResults.Add(execute);
            }
            sw.Stop();
            result.ExecutionTime = sw.ElapsedMilliseconds;

            return result;
        }

        public async Task<ISetResult> ExecuteAsync()
        {
            var tasks = Task.Run(() => Execute());
            var result = await tasks;

            return result;
        }
    }
}
