using LoadTestEm.LoadTasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestEm.LoadSets
{
    public interface ILoadSet
    {
        ICollection<ILoadTask> Tasks { get; set; }

        long Execute();

        Task<long> ExecuteAsync();
    }
    public class LoadSet : ILoadSet
    {
        private ICollection<ILoadTask> _tasks = new List<ILoadTask>();
        public ICollection<ILoadTask> Tasks
        {
            get { return _tasks; }

            set { _tasks = value; }
        }

        public long Execute()
        {
            var result = 0L;

            foreach (var task in Tasks)
            {
                result += task.Execute();
            }

            return result;
        }

        public async Task<long> ExecuteAsync()
        {
            var task = Task.Run(() => Execute());
            var result = await task;

            return result;
        }
    }
}
