using System.Collections.Generic;

namespace LoadTestEm.LoadSets
{
    public interface IGroupResult
    {
        string Identifier { get; set; }

        long ExecutionTime { get; set; }

        IDictionary<int,ICollection<ISetResult>> LoadSetResults { get; set; }
    }

    public class GroupResult : IGroupResult
    {
        private IDictionary<int, ICollection<ISetResult>> _loadSetResults = new Dictionary<int, ICollection<ISetResult>>();

        private string _identifier = string.Empty;
        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public long ExecutionTime { get; set; }

        public IDictionary<int, ICollection<ISetResult>> LoadSetResults
        {
            get { return _loadSetResults; }

            set { _loadSetResults = value; }
        }
    }
}
