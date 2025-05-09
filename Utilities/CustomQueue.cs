namespace OrderManagementSystem.Utilities;

public class CustomQueue<T>
{
    private readonly LinkedList<T> _list = new LinkedList<T>();
    private readonly object _lock = new object();

    public void Enqueue(T item)
    {
        lock (_lock)
        {
            _list.AddLast(item);
        }
    }

    public T Dequeue()
    {
        lock (_lock)
        {
            if (_list.Count == 0) throw new InvalidOperationException("Queue is empty");

            var value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }
    }

    public int Count
    {
        get
        {
            lock (_lock)
            {
                return _list.Count;
            }
        }
    }
}