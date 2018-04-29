using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheRoot
{
    class FindTheRoot
    {
        static int edjesCount;
        static int nodesCount;
        private static Node<int>[] allNodes;
        private static bool isTree = true;
        private static int rootCount;
        private static int root;
        public static void ReadInputInfo()
        {
            nodesCount = int.Parse(Console.ReadLine().Trim());
            edjesCount = int.Parse(Console.ReadLine().Trim());
            allNodes = new Node<int>[nodesCount];
            InitializeNodes();
            for (int i = 0; i < edjesCount; i++)
            {
                var data = Console.ReadLine().Trim()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                BuildConnection(data[0], data[1]);
            }
        }

        private static void InitializeNodes()
        {
            for (int i = 0; i < nodesCount; i++)
            {
                allNodes[i] = new Node<int>(i);
            }
        }
        public static void BuildConnection(int parentNodeIndex, int childNodeIndex)
        {

            Node<int>  parentNode = allNodes[parentNodeIndex];
            Node<int> childNode = allNodes[childNodeIndex];

            if(isTree && childNode.Parents.Any(x => x.Value == parentNodeIndex))
                isTree = false;

            parentNode.Children.Add(childNode);
            childNode.Parents.Add(parentNode);
        }
        static void Main()
        {
            ReadInputInfo();
            PrintResult();
        }

        private static void PrintResult()
        {

            Console.WriteLine();

            if(!isTree)
                Console.WriteLine("No root! The graph is not a tree.");
            foreach(var node in allNodes)
            {
                if (node.Parents.Count == 0)
                {
                    root = node.Value;
                    rootCount++;
                }
            }

            switch(rootCount)
            {
                case 0:
                    if (isTree) 
                        Console.WriteLine("No root! The graph is not a tree."); 
                    break;
                case 1: 
                    if(isTree) 
                        Console.WriteLine("The graph is a valid tree with root element: " + root);
                    break; 
                default: 
                    if(isTree) 
                        Console.WriteLine("Multiple root Nodes!\nThe graph is not a tree, it's a forest!");
                    break;
            }
        }
    }
}
