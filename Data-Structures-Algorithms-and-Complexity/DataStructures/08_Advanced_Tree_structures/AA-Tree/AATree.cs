using System;
using System.Collections.Generic;
namespace AA_Tree
{

    public class AATree<T> where T : IComparable<T>
    {
        public class AANode
        {
            internal int level;
            internal AANode leftChild;
            internal AANode rightChild;
            internal T value;

            //Sentinel Node - used as an alternative over using null as the path terminator 
            internal AANode()
            {
                this.level = 0;
                this.leftChild = this;
                this.rightChild = this;
            }
            //Normal Node - it starts it's life as a leaf(lvl 1 always)
            internal AANode(T value)
            {
                this.level = 1;
                this.leftChild = new AANode();
                this.rightChild = new AANode();
                this.value = value;
            }
            public bool IsSentinel()
            {
                return this.level == 0;
            }
        }

        private AANode root;
        private AANode mediator;
        private readonly AANode sentinel;

        private bool IsLeaf(AANode node)
        {
            return node.level == 1 && node.leftChild.IsSentinel() && node.rightChild.IsSentinel();
        }

        public AATree()
        {
            this.root = new AANode();
            sentinel = new AANode();
        }
        public AANode Root
        {
            get { return this.root; }
            set { this.root = value; }
        }
        private AANode Skew(AANode tree)
        {
            if (tree.IsSentinel())
                return sentinel;

            if (tree.leftChild.IsSentinel())
                return tree;

            if (tree.leftChild.level == tree.level)
            {
                var leftChildTree = tree.leftChild;
                tree.leftChild = leftChildTree.rightChild;
                leftChildTree.rightChild = tree;
                return leftChildTree;
            }
            else return tree;
        }
        private AANode Split(AANode tree)
        {
            if (tree == sentinel)
                return sentinel;

            if (tree.rightChild.IsSentinel() || tree.rightChild.rightChild.IsSentinel())
                return tree;

            if (tree.level == tree.rightChild.rightChild.level)
            {
                var rightChild = tree.rightChild;
                tree.rightChild = rightChild.leftChild;
                rightChild.leftChild = tree;
                rightChild.level = rightChild.level + 1;
                return rightChild;
            }
            else return tree;
        }
        public AANode Add(T value, AANode node)
        {
            if (node.IsSentinel())
                return new AANode(value);

            if (value.CompareTo(node.value) < 0)
                node.leftChild = Add(value, node.leftChild);
            else if (value.CompareTo(node.value) > 0)
                node.rightChild = Add(value, node.rightChild);

            node = this.Skew(node);
            node = this.Split(node);

            return node;
        }

        public AANode Delete(T value, AANode node)
        {
            if (node.IsSentinel())
                return node;

            if (value.CompareTo(node.value) > 0)
                node.rightChild = Delete(value, node.rightChild);
            else if (value.CompareTo(node.value) < 0)
                node.leftChild = Delete(value, node.leftChild);
            else
            {
                if (IsLeaf(node))
                    return sentinel;
                else if (node.leftChild.IsSentinel())
                {
                    FindSuccessor(node);
                    node.rightChild = Delete(mediator.value, node.rightChild);
                    node.value = mediator.value;
                }
                else
                {
                    FindPredecessor(node);
                    node.leftChild = Delete(mediator.value, node.leftChild);
                    node.value = mediator.value;
                }
            }

            node = DecreaseLevel(node);
            node = Skew(node);
            node.rightChild = Skew(node.rightChild);

            if (!node.rightChild.IsSentinel())
                node.rightChild.rightChild = Skew(node.rightChild.rightChild);

            node = Split(node);
            node.rightChild = Split(node.rightChild);
            return node;
        }

        private AANode DecreaseLevel(AANode node)
        {
            int decreasedLevel = Math.Min(node.leftChild.level, node.rightChild.level) + 1;
            if (decreasedLevel < node.level)
            {
                node.level = decreasedLevel;
                if (decreasedLevel < node.rightChild.level)
                    node.rightChild.level = decreasedLevel;
            }

            return node;
        }

        private void FindPredecessor(AANode node)
        {
            mediator = node.leftChild;
            while (!mediator.rightChild.IsSentinel())
            {
                mediator = mediator.rightChild;
            }
        }

        private void FindSuccessor(AANode node)
        {
            mediator = node.rightChild;
            while (!mediator.leftChild.IsSentinel())
            {
                mediator = mediator.leftChild;
            }
        }
    }
}
