using System;
using System.Threading.Tasks;

namespace LoadTestEm.Agents
{
    public interface IAgent
    {
        Task<long> ExecuteAsync();
    }
}