using System;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{
    public interface ILoadResult
    {
        long ExecutionTime { get; set; }
    }

    public interface ILoadTask
    {
        ILoadResult Execute();

        Task<ILoadResult> ExecuteAsync();
    }
}