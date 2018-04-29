using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1.PlayWithTrees
{
    class Tree<T>
    {
        public Tree<T> Parent { get; set; }
        public IList<Tree<T>> Children { get; private set; }
        public T Value { get; set; }

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();
            foreach(var child in children)
            {
                child.Parent = this;
                this.Children.Add(child);
            }
        }

        public void Each(Action<T> action)
        {
            action(this.Value);
            foreach(var child in Children)
            {
                child.Each(action);
            }
        }
    }
}
