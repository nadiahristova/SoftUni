using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSet
{
    public class OrderedSet<T>: IEnumerable<T>
        where T : IComparable<T>
    {
        private SetNode<T> root;
        public int Count { get; private set; }
        public OrderedSet()
        {
            this.Count = 0;
        }
        private void PrintInOrder(SetNode<T> root)
        {
            if(root == null)
            {
                return;
            }
            PrintInOrder(root.LeftChild);
            Console.WriteLine(root.Value + " ");
            PrintInOrder(root.RightChild);
        }
        public void PrintInOrder()
        {
            PrintInOrder(this.root);
            Console.WriteLine();
        }
        public void Add(T value)
        {
            var newNode = new SetNode<T>(value);

            if(this.root == null)
            {
                this.root = newNode;
                this.Count++;
                return;
            }
            this.FindPositionAndInsert(this.root, newNode);            
        }
        private void FindPositionAndInsert(SetNode<T> currNode, SetNode<T> newNode)
        {
            int compareValue = newNode.Value.CompareTo(currNode.Value);
            if(compareValue > 0)
            {
                if (currNode.RightChild != null)
                    FindPositionAndInsert(currNode.RightChild, newNode);
                else
                {
                    currNode.RightChild = newNode;
                    newNode.Parent = currNode;
                    this.Count++;
                }
            }

            if(compareValue < 0)
            {
                if (currNode.LeftChild != null)
                    FindPositionAndInsert(currNode.LeftChild, newNode);
                else
                {
                    currNode.LeftChild = newNode;
                    newNode.Parent = currNode;
                    this.Count++;
                }
            }
        }
        public bool Contains(T value)
        {
            return this.FindNode(value) == null ? false : true;
        }
        private SetNode<T> FindNode(T value)
        {
            SetNode<T> currNode = this.root;

            while(currNode != null)
            {
                int compareValue = value.CompareTo(currNode.Value);
                if (compareValue > 0)
                {
                    currNode = currNode.RightChild;
                }
                else if (compareValue < 0)
                {
                    currNode = currNode.LeftChild;
                }
                else break;
            }

            return currNode;
        }
        public bool Remove(T value)
        {
            SetNode<T> treeInQuestion = this.FindNode(value);
            if(treeInQuestion == null)
            {
                return false;
            }

            this.Remove(treeInQuestion);
            this.Count--;
            return true;
        }
        private void Remove(SetNode<T> treeInQuestion)
        {
            if(treeInQuestion.LeftChild != null && treeInQuestion.RightChild != null)
            {
                SetNode<T> moveChild = this.FindYoungestChild(treeInQuestion.RightChild) ?? treeInQuestion.RightChild;


                HookNodeLeftOrRight(treeInQuestion.Parent, treeInQuestion, moveChild);
                moveChild.Parent = treeInQuestion.Parent;
                moveChild.LeftChild = treeInQuestion.LeftChild;
                treeInQuestion.LeftChild.Parent = moveChild;  
            
                return;
            }

            SetNode<T> theChild = treeInQuestion.LeftChild != null ? treeInQuestion.LeftChild : treeInQuestion.RightChild;

            if(theChild != null)
            {
                theChild.Parent = treeInQuestion.Parent;
                HookNodeLeftOrRight(treeInQuestion.Parent, treeInQuestion, theChild);

                if (treeInQuestion.Parent == null)
                    this.root = theChild;
            }
            else
            {
                if (treeInQuestion.Parent == null)
                    this.root = null;
                else
                {
                    HookNodeLeftOrRight(treeInQuestion.Parent, treeInQuestion, null);
                }
            }
        }
        private void HookNodeLeftOrRight(SetNode<T> parent, SetNode<T> formerChild, SetNode<T> currChild)
        {
            if (parent == null)
            {
                this.root = currChild;
                return;
            }                
            else if (parent.LeftChild == formerChild)
                parent.LeftChild = currChild;
            else
                parent.RightChild = currChild;
        }
        private SetNode<T> FindYoungestChild(SetNode<T> node)
        {
            while(node.LeftChild != null)
            {
                node = node.LeftChild;
            }

            return node.LeftChild;
        }
        public List<T> ToList()
        {
            List<T> output = new List<T>();
            foreach (var el in this)
            {
                output.Add(el);
            }

            return output;
        }
        public void Clear()
        {
            this.root = null;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return this.root.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }        
    }
}
