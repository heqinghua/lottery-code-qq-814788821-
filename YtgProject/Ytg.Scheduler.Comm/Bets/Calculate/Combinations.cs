using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    public class Combinations<T> : IEnumerable<IList<T>>
    {
        public List<IList<T>> _combinations;
        private IList<T> _items;
        private int _length;
        private int[] _endIndices;
        public Combinations(IList<T> itemList)
            : this(itemList, itemList.Count)
        {
        }
        public Combinations(IList<T> itemList, int length)
        {
            _items = itemList;
            _length = length;
            _combinations = new List<IList<T>>();
            _endIndices = new int[length];
            int j = length - 1;
            for (int i = _items.Count - 1; i > _items.Count - 1 - length; i--)
            {
                _endIndices[j] = i;
                j--;
            }
            ComputeCombination();
        }
        private void ComputeCombination()
        {
            int[] indices = new int[_length];
            for (int i = 0; i < _length; i++)
            {
                indices[i] = i;
            }
            do
            {
                T[] oneCom = new T[_length];
                for (int k = 0; k < _length; k++)
                {
                    oneCom[k] = _items[indices[k]];
                }
                _combinations.Add(oneCom);
            }
            while (GetNext(indices));
        }
        private bool GetNext(int[] indices)
        {
            bool hasMore = true;
            for (int j = _endIndices.Length - 1; j > -1; j--)
            {
                if (indices[j] < _endIndices[j])
                {
                    indices[j]++;
                    for (int k = 1; j + k < _endIndices.Length; k++)
                    {
                        indices[j + k] = indices[j] + k;
                    }
                    break;
                }
                else if (j == 0)
                {
                    hasMore = false;
                }
            }
            return hasMore;
        }
        public int Count
        {
            get { return _combinations.Count; }
        }
        public IEnumerator<IList<T>> GetEnumerator()
        {
            return _combinations.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<IList<T>>)GetEnumerator();
        }

    }
}
