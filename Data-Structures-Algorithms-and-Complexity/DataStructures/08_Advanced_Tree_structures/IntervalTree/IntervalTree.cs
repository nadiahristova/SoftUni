using AA_Tree;
using System;
using System.Collections.Generic;

namespace IntervalTree
{

    public class IntervalTree<T> where T : IComparable
    {
        public class AANode
        {
            internal int level;
            internal AANode leftChild;
            internal AANode rightChild;
            internal Interval<T> interval;
            internal T max;

            //Sentinel Node - used as an alternative over using null as the path terminator 
            internal AANode()
            {
                this.level = 0;
                this.leftChild = this;
                this.rightChild = this;
            }
            //Normal Node - it starts it's life as a leaf(lvl 1 always) with a metadata max value == max value of the interval
            internal AANode(Interval<T> interval)
            {
                this.level = 1;
                this.leftChild = new AANode();
                this.rightChild = new AANode();
                this.interval = interval;
                this.max = interval.maxValue;
            }
            public bool IsSentinel()
            {
                return this.level == 0;
            }

            internal bool Overlaps(Interval<T> interval)
            {
                return this.interval.minValue.CompareTo(interval.maxValue) <= 0 &&
                    this.interval.maxValue.CompareTo(interval.minValue) >= 0;
            }
        }

        private AANode root;
        private AANode mediator;
        private readonly AANode sentinel;
        private ICollection<Interval<T>> overlappedIntervals;

        private bool IsLeaf(AANode node)
        {
            return node.level == 1 && node.leftChild.IsSentinel() && node.rightChild.IsSentinel();
        }

        public IntervalTree()
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
            if (tree.IsSentinel())
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
        public void Insert(Interval<T> interval)
        {
            this.Root = this.Add(interval, this.root);
        }
        public AANode EstablishMetaData(AANode aANode)
        {
            if (aANode.rightChild.IsSentinel())
                return aANode;

            aANode.rightChild = EstablishMetaData(aANode.rightChild);
            aANode = FindBiggest(aANode);
            aANode.leftChild = EstablishMetaData(aANode.leftChild);
            aANode = FindBiggest(aANode);
            return aANode;
        }
        private AANode FindBiggest(AANode aANode)
        {
            aANode.max = aANode.interval.maxValue;

            T biggest = aANode.rightChild.max.CompareTo(aANode.rightChild.interval.maxValue) > 0 ? aANode.rightChild.max : aANode.rightChild.interval.maxValue;
            if (aANode.max.CompareTo(biggest) < 0)
            {
                aANode.max = biggest;
            }
            return aANode;
        }
        public bool Remove(Interval<T> interval)
        {
            AANode node = Delete(interval, this.root);
            this.EstablishMetaData(this.Root); // since we don't have parent info

            return !node.IsSentinel();
        }
        private AANode Add(Interval<T> interval, AANode node)
        {
            if (node.IsSentinel())
                return new AANode(interval);

            if (interval.CompareTo(node.interval) < 0)
                node.leftChild = Add(interval, node.leftChild);
            else if (interval.CompareTo(node.interval) > 0)
                node.rightChild = Add(interval, node.rightChild);

            node = this.Skew(node);
            node = this.Split(node);
            
            node = FindBiggest(node);

            return node;
        }
        private AANode Delete(Interval<T> interval, AANode node)
        {
            if (node.IsSentinel())
                return node;

            if (interval.minValue.CompareTo(node.interval.minValue) > 0)
                node.rightChild = Delete(interval, node.rightChild);
            else if (interval.minValue.CompareTo(node.interval.minValue) < 0)
                node.leftChild = Delete(interval, node.leftChild);
            else
            {
                if (IsLeaf(node))
                    return sentinel;
                else if (node.leftChild.IsSentinel())
                {
                    FindSuccessor(node);
                    node.rightChild = Delete(mediator.interval, node.rightChild);
                    node.interval = mediator.interval;
                }
                else
                {
                    FindPredecessor(node);
                    node.leftChild = Delete(mediator.interval, node.leftChild);
                    node.interval = mediator.interval;
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
        public ICollection<Interval<T>> FindOverlappingIntervals(Interval<T> iniIntreval)
        {
            AANode currNode = this.Root;
            this.overlappedIntervals = new HashSet<Interval<T>>();

            this.Search(iniIntreval, this.Root);

            return this.overlappedIntervals;
        }
        public void Search(Interval<T> interval, AANode node)
        {
            if (node.IsSentinel())
                return;

            if (node.Overlaps(interval))
                overlappedIntervals.Add(node.interval);

            if (!node.leftChild.IsSentinel() && node.leftChild.max.CompareTo(interval.minValue) >= 0)
                Search(interval, node.leftChild);

            if (!node.rightChild.IsSentinel() && node.rightChild.interval.minValue.CompareTo(interval.maxValue) <= 0)
                Search(interval, node.rightChild);
        }
    }
}
