using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSet
{
    public class SetNode<T>: IEnumerable<T>
    {
        private T value;
        private SetNode<T> leftChild; //the sibling with smaller value
        private SetNode<T> rightChild; //the sibling with larger value
        public SetNode(T value, SetNode<T> leftChild, SetNode<T> rightChild)
        {
            if (value == null)
                throw new ArgumentException("Binary Tree Structure needs values in order to work.");

            this.Value = value;
            if(leftChild != null)
                this.LeftChild = leftChild;
            if(rightChild != null)
                this.RightChild = rightChild;
        }
        public SetNode(T value):this(value, null, null) { }
        public T Value 
        { 
            get { return this.value; }
            set { this.value = value; }
        }

        public SetNode<T> Parent{ get; set; }
        public SetNode<T> LeftChild
        {
            get { return this.leftChild; }
            set 
            {
                this.leftChild = value;  
                if(value != null)
                    this.leftChild.Parent = this;                            
            }
        }
        public SetNode<T> RightChild
        {
            get { return this.rightChild; }
            set
            {
                this.rightChild = value;
                if (value != null)
                    this.rightChild.Parent = this;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.LeftChild != null) //orientate towards (the value of) Left node/s
            {
                foreach(var value in LeftChild)
                {
                    yield return value;
                }
            }

            yield return Value; // take the value of the root tree

            if (this.RightChild != null) //orientate towards (the value of) Right node/s
            {
                foreach (var value in RightChild)
                {
                    yield return value;
                }
            }
        }
    }
}
