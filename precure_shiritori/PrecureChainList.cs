using System;
using System.Collections.Generic;
using System.Linq;

namespace precure_shiritori
{
    class PrecureChainList
    {
        private List<short> _chain = new List<short>();
        private List<Precure> AllPrecures { get; set; }

        public PrecureChainList(List<Precure> precureList)
        {
            this.AllPrecures = precureList;
        }

        public Precure LastPrecure
        {
            get
            {
                int ix = _chain[_chain.Count - 1];
                return AllPrecures[ix];
            }
        }

        public PrecureChainList Clone()
        {
            var wcl = new PrecureChainList(this.AllPrecures)
            {
                _chain = _chain.ToList()
            };
            return wcl;
        }

        public void Add(Precure precure)
        {
            short ix = (short)AllPrecures.FindIndex(x => x.PrecureName == precure.PrecureName);
            _chain.Add(ix);
        }

        public Precure Find(Func<Precure, bool> pred)
        {
            foreach (var w in GetPrecureList())
            {
                if (pred(w) == true)
                {
                    return w;
                }
            }
            return null;
        }

        public IEnumerable<Precure> GetPrecureList()
        {
            foreach (var ix in _chain)
            {
                yield return AllPrecures[ix];
            }
        }

        public int Count
        {
            get { return _chain.Count; }
        }

        internal void RemoveAt(int index)
        {
            _chain.RemoveAt(index);
        }
    }
}
