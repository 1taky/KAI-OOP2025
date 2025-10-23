using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab2
{
    public class TreeNode<T>
    {
        public T Data;
        public TreeNode<T> Left;
        public TreeNode<T> Right;

        public TreeNode(T data)
        {
            Data = data;
        }
    }

    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public TreeNode<T> Root;

        public void Insert(T value)
        {
            Root = InsertRec(Root, value);
        }

        private TreeNode<T> InsertRec(TreeNode<T> node, T value)
        {
            if (node == null) return new TreeNode<T>(value);
            if (value.CompareTo(node.Data) < 0)
                node.Left = InsertRec(node.Left, value);
            else
                node.Right = InsertRec(node.Right, value);
            return node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return PostOrder(Root).GetEnumerator();
        }

        private IEnumerable<T> PostOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                foreach (var left in PostOrder(node.Left))
                    yield return left;
                foreach (var right in PostOrder(node.Right))
                    yield return right;
                yield return node.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
