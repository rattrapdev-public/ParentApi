using System.Collections;

namespace Parent.Common;

public abstract class ValueObjects<T> : IEnumerable<T>
    where T : ValueObject
{
    protected readonly List<T> _valueList;

    protected ValueObjects(IEnumerable<T> valueObjects)
    {
        _valueList = new List<T>(valueObjects);
    }

    public IEnumerator<T> GetEnumerator() => _valueList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}