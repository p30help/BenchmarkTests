using System.Collections;
using System.Linq;

namespace CollectionBenchmark
{
    public class CustomConcurrentList<T> : IEnumerable<T>
    {
        List<T> _list;

        public CustomConcurrentList()
        {
            _list = new List<T>();
        }

        public T this[int index]
        {
            get => _list[index];
            set
            {
                lock (this)
                {
                    _list[index] = value;
                }
            }
        }

        public int Count => _list.Count;

        public void Add(T item)
        {
            lock (this)
            {
                _list.Add(item);
            }
        }

        public void Clear()
        {
            lock (this)
            {
                _list.Clear();
            }
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);

        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            lock (this)
            {
                _list.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            lock (this)
            {
                return _list.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (this)
            {
                _list.RemoveAt(index);
            }
        }

        public T? FirstOrDefault(Func<T, bool> predicate)
        {
            lock (this)
            {
                return _list.FirstOrDefault(predicate);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

    }
}
