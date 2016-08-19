using System;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{
    public interface ILoadTask
    {
        long Execute();

        Task<long> ExecuteAsync();
    }
}