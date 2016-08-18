using System;
using System.Threading.Tasks;

namespace LoadTestEm
{
    public interface IAgent
    {
        Task<long> ExecuteAsync();
    }
}