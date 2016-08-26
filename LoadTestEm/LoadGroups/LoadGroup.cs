using LoadTestEm.LoadTasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LoadTestEm.LoadSets
{
    public interface ILoadGroup : IDisposable
    {
        string Identifier { get; set; }

        int Rounds { get; set; }

        ICollection<ILoadSet> Sets { get; set; }

        IGroupResult Execute();

        Task<IGroupResult> ExecuteAsync();
    }

    public class LoadGroup : ILoadGroup
    {
        private ICollection<ILoadSet> _sets = new List<ILoadSet>();

        private string _identifier = string.Empty;

        private int _rounds = 1;

        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public int Rounds { get { return _rounds; } set { _rounds = value; } }

        public ICollection<ILoadSet> Sets
        {
            get { return _sets; }

            set { _sets = value; }
        }

        public virtual void Dispose() { }

        public virtual IGroupResult Execute()
        {
            var result = new GroupResult() { Identifier = Identifier };

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < _rounds; i++)
            {
                var resultList = new List<ISetResult>();
                result.LoadSetResults.Add(i + 1, resultList);
                foreach (var set in Sets)
                {
                    var execute = set.Execute();
                    resultList.Add(execute);
                }
            }
            sw.Stop();
            result.ExecutionTime = sw.ElapsedMilliseconds;

            return result;
        }

        public async Task<IGroupResult> ExecuteAsync()
        {
            var result = new GroupResult() { Identifier = Identifier };

            var sets = new Dictionary<int, IList<Task<ISetResult>>>();
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < _rounds; i++)
            {
                var setList = new List<Task<ISetResult>>();
                sets.Add(i, setList);
                foreach (var set in Sets)
                {
                    setList.Add(set.ExecuteAsync());
                }
            }

            var tmpList = new List<Task<ISetResult>>();
            foreach (var item in sets)
            {
                foreach (var sub in item.Value)
                {
                    tmpList.Add(sub);
                }
            }
            await Task.WhenAll(tmpList);
            sw.Stop();
            result.ExecutionTime = sw.ElapsedMilliseconds;

            foreach (var item in sets)
            {
                var resultList = new List<ISetResult>();
                result.LoadSetResults.Add(item.Key + 1, resultList);
                foreach (var rs in item.Value)
                {
                    resultList.Add(rs.Result);
                }
            }

            return result;
        }
    }
}
