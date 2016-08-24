using System;
using System.Collections;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{
    public interface ILoadResult
    {
        long ExecutionTime { get; set; }
        IDictionary Statistics { get; set; }
    }

    public interface ILoadTask
    {
        ILoadResult Execute();

        Task<ILoadResult> ExecuteAsync();
    }
}