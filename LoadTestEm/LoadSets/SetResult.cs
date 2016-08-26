using LoadTestEm.LoadTasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestEm.LoadSets
{
    public interface ISetResult
    {
        string Identifier { get; set; }

        long ExecutionTime { get; set; }

        ICollection<ILoadResult> LoadTaskResults { get; set; }
    }

    public class SetResult : ISetResult
    {
        private ICollection<ILoadResult> _LoadTaskResults = new List<ILoadResult>();

        private string _identifier = string.Empty;
        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public long ExecutionTime { get; set; }

        public ICollection<ILoadResult> LoadTaskResults
        {
            get { return _LoadTaskResults; }

            set { _LoadTaskResults = value; }
        }
    }
}
