
    using System.Collections.Generic;

    namespace OrderManagementSystem.Utilities
    {
        public class CustomLinkedList<T>
        {
            private LinkedList<T> _list = new LinkedList<T>();
            private readonly object _lock = new object();

            public void AddLast(T item)
            {
                lock (_lock)
                {
                    _list.AddLast(item);
                }
            }

            public IEnumerable<T> Where(Func<T, bool> predicate)
            {
                lock (_lock)
                {
                    return _list.Where(predicate);
                }
            }
        }
    }

