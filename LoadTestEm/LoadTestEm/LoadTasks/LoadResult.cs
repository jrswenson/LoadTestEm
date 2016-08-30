using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestEm.LoadTasks
{
    public interface ILoadResult
    {
        string Identifier { get; set; }

        long ExecutionTime { get; set; }

        IDictionary Diagnostics { get; set; }
    }

    public class LoadResult : ILoadResult
    {
        private string _identifier = string.Empty;
        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public long ExecutionTime { get; set; }

        public IDictionary Diagnostics { get; set; }
    }
}
