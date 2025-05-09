using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Utilities;


public class CustomAvlTree<T> where T : IComparable<T>
{
    private class AvlNode
    {
        public T Value { get; set; }
        public AvlNode Left { get; set; }
        public AvlNode Right { get; set; }
        public int Height { get; set; } = 1;
    }

    private AvlNode _root;
    private readonly object _lock = new object();

    public void Insert(T value)
    {
        lock (_lock)
        {
            _root = Insert(_root, value);
        }
    }

    private AvlNode Insert(AvlNode node, T value)
    {
        if (node == null) return new AvlNode { Value = value };

        int compareResult = value.CompareTo(node.Value);
        if (compareResult < 0)
            node.Left = Insert(node.Left, value);
        else if (compareResult > 0)
            node.Right = Insert(node.Right, value);
        else
            return node; // Duplicate values not allowed

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        return Rebalance(node);
    }


    public T Find(T value)
    {
        lock (_lock)
        {
            return Find(_root, value);
        }
    }

    private T Find(AvlNode node, T value)
    {
        if (node == null) throw new KeyNotFoundException();

        int compareResult = value.CompareTo(node.Value);
        return compareResult switch
        {
            < 0 => Find(node.Left, value),
            > 0 => Find(node.Right, value),
            _ => node.Value
        };
    }

    // bu fonksiyonda değişiklik yaptım 
    public List<Product> SearchCategory(string categoryName)
    {
        if (typeof(T) != typeof(Category))
            throw new InvalidOperationException("SearchCategory only works when T is Category.");

        lock (_lock)
        {
            var category = Find((T)(object)new Category { Name = categoryName });
            var categoryEntity = category as Category;
            return categoryEntity?.Products?.ToList() ?? new List<Product>();
        }
    }


    private int Height(AvlNode node) => node?.Height ?? 0;

    private AvlNode Rebalance(AvlNode node)
    {
        int balance = GetBalance(node);

        if (balance > 1)
        {
            if (GetBalance(node.Left) >= 0)
                return RotateRight(node);

            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (balance < -1)
        {
            if (GetBalance(node.Right) <= 0)
                return RotateLeft(node);

            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private int GetBalance(AvlNode node) => Height(node.Left) - Height(node.Right);

    private AvlNode RotateRight(AvlNode y)
    {
        var x = y.Left;
        var T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));
        x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));

        return x;
    }

    private AvlNode RotateLeft(AvlNode x)
    {
        var y = x.Right;
        var T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
        y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));

        return y;
    }
}