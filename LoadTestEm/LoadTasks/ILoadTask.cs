using System.Collections;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{   
    public interface ILoadTask
    {
        string Identifier { get; set; }

        ILoadResult Execute();

        Task<ILoadResult> ExecuteAsync();
    }
}