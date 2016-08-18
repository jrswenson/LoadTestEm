using System;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{
    public interface ILoadTask
    {
        Task<long> ExecuteAsync();
    }
}