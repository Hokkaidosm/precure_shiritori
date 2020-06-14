using System.Collections.Generic;
using System.Linq;

namespace precure_shiritori
{
    class PrecureChainSolver
    {
        public List<Precure> PrecureList { get; set; }
        public PrecureChainSolver(List<Precure> precureList)
        {
            PrecureList = precureList.ToList();
        }

        private Queue<PrecureChainList> _queue = new Queue<PrecureChainList>();
        public PrecureChainList Solve(Precure precure)
        {
            PrecureChainList firstState = new PrecureChainList(PrecureList);
            firstState.Add(precure);
            PrecureChainList ans = firstState;
            _queue.Enqueue(firstState);
            while (_queue.Count > 0)
            {
                var curr = _queue.Dequeue();
                ans = curr;
                foreach (var w in Candidate(curr.LastPrecure))
                {
                    if (ans.Find(x => x == w) != null)
                        continue;
                    curr.Add(w);
                    _queue.Enqueue(curr.Clone());
                    curr.RemoveAt(curr.Count - 1);
                }

            }
            return ans;
        }
        // 候補の単語を列挙する
        private IEnumerable<Precure> Candidate(Precure precure)
        {
            return PrecureList.Where(x => precure.Last == x.First).ToList();
        }
    }
}
