using System.Collections;

namespace Parent.Common;

public abstract class ValueObjects<T> : ICollection<T>
    where T : ValueObject
{
    protected readonly List<T> _valueList;

    protected ValueObjects(IEnumerable<T> valueObjects)
    {
        _valueList = new List<T>(valueObjects);
    }

    public IEnumerator<T> GetEnumerator() => _valueList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    
    public void Add(T item)
    {
        _valueList.Add(item);
    }

    public void Clear()
    {
        _valueList.Clear();
    }

    public bool Contains(T item)
    {
        return _valueList.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _valueList.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _valueList.Remove(item);
    }

    public int Count => _valueList.Count;
    public bool IsReadOnly => false;
}