using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheRoot
{
    public class Node<T>
    {
        public IList<Node<T>> Parents { get; set; }
        public int Value { get; set; }
        public IList<Node<T>> Children;
        public Node(int Value)
        {
            this.Value = Value;
            this.Children = new List<Node<T>>();
            this.Parents = new List<Node<T>>();
        }
        public override bool Equals(object other)
        {
            var element = (Node<T>)other;
            if (element == null)
                return false;

            return object.Equals(this.Value, element.Value);
        }
    }
}
