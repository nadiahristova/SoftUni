using System;

namespace AA_Tree
{
    class PlayWithAATree
    {
        public static void Main()
        {
            var tree = new AATree<int>();
            Console.WriteLine("The AA tree created.");
            var nums = new[] { -5, 20, 14, 11, 8, -3, 111, 7, 100, -55 };
            for (int i = 0; i < nums.Length; i++)
            {
                AddNumber(tree, nums[i]);
            }

            Console.WriteLine("\n\nTree after we remove element:\n");
            tree.Delete(14, tree.Root);
            DisplayTree(tree.Root, string.Empty);
        }

        public static void AddNumber(AATree<int> tree, int value)
        {
            if (tree.Root.IsSentinel())
                tree.Root = new AATree<int>.AANode(value);
                
            tree.Root = tree.Add(value, tree.Root);
            Console.WriteLine("Added " + value);

            DisplayTree(tree.Root, string.Empty);
            Console.WriteLine("----------------------");
        }

        private static void DisplayTree(AATree<int>.AANode node, string intend)
        {
            Console.WriteLine(intend + node.value + " (level:" + node.level + ")");
            if (node.leftChild.level != 0)
            {
                DisplayTree(node.leftChild, intend + "  ");
            }
            if (node.rightChild.level != 0)
            {
                DisplayTree(node.rightChild, intend + "  ");
            }
        }
    }
}
