using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPathInTree
{
    class LongestPathInTree
    {
        private static Dictionary<int, List<int>> children = new Dictionary<int, List<int>>();
        private static Dictionary<int, int> parents = new Dictionary<int, int>();
        private static Dictionary<int, int> weightToTheRoot = new Dictionary<int, int>();
        private static bool nodeFound;
        private static int treeRoot;
        static void Main()
        {
            CreateTree();
            FindPathWeightToTheRoot();
            int weightOfTheLongestPath = FindWeightOFLongestPath();
            Console.WriteLine(weightToTheRoot);
        }

        private static void CreateTree()
        {
            int treeNodes = int.Parse(Console.ReadLine().Trim()); //we don't really need this
            int treeEdges = int.Parse(Console.ReadLine().Trim());
            for (int edge = 0; edge < treeEdges; edge++)
            {
                string line = Console.ReadLine();
                int[] data = line.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                if (!children.ContainsKey(data[0]))
                    children[data[0]] = new List<int>();
                children[data[0]].Add(data[1]);

                if (!parents.ContainsKey(data[1]))
                {
                    parents[data[1]] = data[0];
                }
                else if (parents[data[1]] != data[0])
                    throw new ArgumentException("A tree node cannot have more than 1 parent.");                    
            }

            FindTreeRoot();
        }

        private static int FindWeightOFLongestPath()
        {
            var nodesExceptRoot = parents.Keys.ToList();
            int heaviestPath = int.MinValue;
            for (int i = 0; i < nodesExceptRoot.Count; i++)
            {
                for (int j = i + 1; j < nodesExceptRoot.Count; j++)
                {
                    int node1 = nodesExceptRoot[i];
                    int node2 = nodesExceptRoot[j];

                    int commonParent = FindNearestCommonParent(node1, node2);
                    int currWeight = weightToTheRoot[node1] + weightToTheRoot[node2] - 2 * weightToTheRoot[commonParent] + commonParent;

                    if (currWeight > heaviestPath)
                        heaviestPath = currWeight;
                }
            }

            return heaviestPath;
        }

        private static int FindNearestCommonParent(int node1, int node2)
        {
            Stack<int> parentsToNode1 = new Stack<int>();
            Stack<int> parentsToNode2 = new Stack<int>();

            parentsToNode1.Push(node1);
            parentsToNode2.Push(node2);
            
            while (true)
            {
                if (parentsToNode2.Contains(parentsToNode1.Peek()))
                    return parentsToNode1.Peek();
                if (parentsToNode1.Contains(parentsToNode2.Peek()))
                    return parentsToNode2.Peek();
                if (node1 != treeRoot)
                {
                    node1 = parents[node1];
                    parentsToNode1.Push(node1);
                }                    
                if (node2 != treeRoot)
                {
                    node2 = parents[node2];
                    parentsToNode2.Push(node2);
                }                    
            }
        }

        private static void FindPathWeightToTheRoot()
        {
            var a = parents.Keys;
            weightToTheRoot[treeRoot] = treeRoot;

            foreach(int node in parents.Keys.ToArray())
            {
                weightToTheRoot[node] = FindWeightOfPath(treeRoot, node);
                nodeFound = false;
            }
        }

        private static int FindWeightOfPath(int currNode, int wantedNode)
        {
            if(children.Keys.Contains(currNode))
                foreach(int child in children[currNode])
                {
                    if (child == wantedNode)
                    {
                        nodeFound = true;
                        return child + treeRoot;
                    }
                    int currSum = FindWeightOfPath(child, wantedNode);
                    if(nodeFound)
                    {
                        return currSum + child;
                    }
                }
            
            return 0;
        }

        private static void FindTreeRoot()
        {
            treeRoot = (children.Keys).Except(parents.Keys).FirstOrDefault();
        }
    }
}
