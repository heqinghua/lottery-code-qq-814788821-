using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    public class Permutations<T> : IEnumerable<IList<T>>
    {
        private List<IList<T>> _permutations;
        private IList<T> _items;
        private int _length;
        private List<int[]> _indices;
        private int[] _value;
        private int _level = -1;
        public Permutations(IList<T> items)
            : this(items, items.Count)
        {
        }
        public Permutations(IList<T> items, int length)
        {
            _items = items;
            _length = length;
            _permutations = new List<IList<T>>();
            _indices = new List<int[]>();
            BuildIndices();
            foreach (IList<T> oneCom in new Combinations<T>(items, length))
            {
                _permutations.AddRange(GetPermutations(oneCom));
            }
        }
        private void BuildIndices()
        {
            _value = new int[_length];
            Visit(0);
        }
        private void Visit(int k)
        {
            _level += 1;
            _value[k] = _level;
            if (_level == _length)
            {
                _indices.Add(_value);
                int[] newValue = new int[_length];
                Array.Copy(_value, newValue, _length);
                _value = newValue;
            }
            else
            {
                for (int i = 0; i < _length; i++)
                {
                    if (_value[i] == 0)
                    {
                        Visit(i);
                    }
                }
            }
            _level -= 1;
            _value[k] = 0;
        }
        private IList<IList<T>> GetPermutations(IList<T> oneCom)
        {
            List<IList<T>> t = new List<IList<T>>();
            foreach (int[] idxs in _indices)
            {
                T[] onePerm = new T[_length];
                for (int i = 0; i < _length; i++)
                {
                    onePerm[i] = oneCom[idxs[i] - 1];
                }
                t.Add(onePerm);
            }
            return t;
        }
        private int GetFactorial(int n)
        {
            int result = 1;
            while (n > 1)
            {
                result *= n;
                n--;
            }
            return result;
        }
        public IEnumerator<IList<T>> GetEnumerator()
        {
            return _permutations.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)_permutations.GetEnumerator();
        }
        public int Count
        {
            get
            {
                return _permutations.Count;
            }
        }


    }
}
