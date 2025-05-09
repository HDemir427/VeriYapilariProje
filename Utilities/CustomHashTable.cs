using System.Collections.Generic;
using System;

namespace OrderManagementSystem.Utilities
{

    public class CustomHashTable<TKey, TValue>
    {
        private class HashNode
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public HashNode Next { get; set; }
        }


        private readonly HashNode[] _buckets = new HashNode[1000];
        private readonly object[] _bucketLocks = new object[1000];

        public CustomHashTable()
        {
            for (int i = 0; i < _bucketLocks.Length; i++)
                _bucketLocks[i] = new object();
        }

        private int GetBucketIndex(TKey key)
            => Math.Abs(key.GetHashCode()) % _buckets.Length;


        public void Add(TKey key, TValue value)
        {
            int index = GetBucketIndex(key);
            lock (_bucketLocks[index])
            {
                var newNode = new HashNode { Key = key, Value = value };

                if (_buckets[index] == null)
                {
                    _buckets[index] = newNode;
                }
                else
                {
                    var currentNode = _buckets[index];
                    while (currentNode.Next != null)
                    {
                        if (currentNode.Key.Equals(key))
                            throw new ArgumentException("Key already exists.");
                        currentNode = currentNode.Next;
                    }
                    currentNode.Next = newNode;
                }
            }
        }

        public TValue Get(TKey key)
        {
            int index = GetBucketIndex(key);
            lock (_bucketLocks[index])
            {
                var node = _buckets[index];
                while (node != null)
                {
                    if (node.Key.Equals(key))
                        return node.Value;
                    node = node.Next;
                }
                throw new KeyNotFoundException($"Key '{key}' not found.");
            }
        }


        public IEnumerable<TValue> GetAll()
        {
            foreach (var bucket in _buckets)
            {
                var currentNode = bucket;
                while (currentNode != null)
                {
                    yield return currentNode.Value;
                    currentNode = currentNode.Next;
                }
            }
        }


        public bool Remove(TKey key)
        {
            int index = GetBucketIndex(key);
            lock (_bucketLocks[index])
            {
                HashNode prev = null;
                var current = _buckets[index];

                while (current != null)
                {
                    if (current.Key.Equals(key))
                    {
                        if (prev == null)
                            _buckets[index] = current.Next;
                        else
                            prev.Next = current.Next;
                        return true;
                    }
                    prev = current;
                    current = current.Next;
                }
                return false;
            }
        }
    }
}