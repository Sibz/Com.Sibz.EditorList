using System.Collections;
using System.Collections.Generic;

namespace Sibz.EditorList
{
    /// <summary>
    /// Helper class to override for use with ListDrawers
    /// </summary>
    /// <typeparam name="T">Type of item in list</typeparam>
    public class EditorList<T> : IList<T>
    {
        public List<T> List = new List<T>();

        public T this[int index] { get => List[index]; set => List[index] = value; }

        public int Count => List.Count;

        public bool IsReadOnly => false;

        public void Add(T item) => List.Add(item);

        public void Clear() => List.Clear();

        public bool Contains(T item) => List.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

        public int IndexOf(T item) => List.IndexOf(item);

        public void Insert(int index, T item) => List.Insert(index, item);

        public bool Remove(T item) => List.Remove(item);

        public void RemoveAt(int index) => List.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();
    }
}
