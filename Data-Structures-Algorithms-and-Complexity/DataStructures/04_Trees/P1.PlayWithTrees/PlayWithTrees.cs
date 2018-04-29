using System;
using System.Collections.Generic;
using System.Linq;

namespace P1.PlayWithTrees
{
    class PlayWithTrees
    {
        static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        static int lengthOfPath;

        static int subTreeSum;

        static Queue<int> FindNumberOfChildren = new Queue<int>();
        static void Main(string[] args)
        {
            int treesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < treesCount - 1; i++)
            {
                int[] treeEdge = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                Tree<int> parent = GetTreeNodeByValue(treeEdge[0]);
                Tree<int> child = GetTreeNodeByValue(treeEdge[1]);
                child.Parent = parent;
                parent.Children.Add(child);
            }
            lengthOfPath = int.Parse(Console.ReadLine());
            subTreeSum = int.Parse(Console.ReadLine());
            Console.WriteLine("Root: " + FindRootNode().Value);
            Console.WriteLine("Middle Nodes: " + string.Join(", ", FindMiddleNodes().Select(node => node.Value)));
            Console.WriteLine("Sorted Leafs: " + string.Join(", ", FindLeafs().Select(leaf => leaf.Value).OrderBy(l => l)));
            Console.WriteLine("Leafs from Left to Right: " + string.Join(", ", FindLeafsLeftToRight().Select(leaf => leaf.Value)));
            var a = FindLongestPathFromRoot(FindRootNode()).Select(leaf => leaf.Value).OrderBy(l => l).ToList();
            Console.WriteLine("Longest Path - from Root down: " + 
                string.Join(" -> ", FindLongestPathFromRoot(FindRootNode()).Select(leaf => leaf.Value).ToList()));
            Console.WriteLine("Longest Path - from Leafs up: " +
                string.Join(" -> ", FindLongestPathFromLeaf(FindLeafsLeftToRight()).Select(leaf => leaf.Value).OrderBy(l => l).ToList()));
            Console.WriteLine("Subtrees of sum " + subTreeSum + ":");
            foreach (var subtree in FindSubtreesSum(FindRootNode()))
            {
                var nodes = new List<int>();
                subtree.Each(nodes.Add);
                Console.WriteLine(string.Join(" + ", nodes));
            }
        }

        static Tree<int> GetTreeNodeByValue(int value)
        {
            if(!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }
            return nodeByValue[value];
        }

        static Tree<int> FindRootNode()
        {
            var rootNode = nodeByValue.Values.FirstOrDefault(node => node.Parent == null);
            return rootNode;
        }

        static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = nodeByValue.Values.Where(node =>
                (node.Parent != null && node.Children.Count() > 0));
            return middleNodes;
        }

        static IEnumerable<Tree<int>> FindLeafs()
        {
            var leafs = nodeByValue.Values.Where(node => node.Children.Count() == 0);
            return leafs;
        }

        static IEnumerable<Tree<int>> FindLeafsLeftToRight()
        {
            var leafs = FindLeafs();
            leafs = OrderLeafs(leafs);
            return leafs;
        }

        static IEnumerable<Tree<int>> OrderLeafs(IEnumerable<Tree<int>> leafs)
        {
            List<int> allNodesLeftToRight = new List<int>();
            IList<Tree<int>> orderedLeafs = new List<Tree<int>>();
            FindRootNode().Each(nodeValue => allNodesLeftToRight.Add(nodeValue));
            foreach(int nodeValue in allNodesLeftToRight)
            {
                var currNode = leafs.FirstOrDefault(l => l.Value == nodeValue);
                if(currNode != null)
                    orderedLeafs.Add(leafs.FirstOrDefault(l => l.Value == nodeValue));
            }
            return orderedLeafs;
        }

        static IList<Tree<int>> FindLongestPathFromRoot(Tree<int> currTree)
        {
            IList<Tree<int>> longestPath = new List<Tree<int>>();
            IList<Tree<int>> currentPath;

            foreach(var child in currTree.Children)
            {
                currentPath = FindLongestPathFromRoot(child);
                if(currentPath.Count() > longestPath.Count())
                {
                    longestPath = currentPath;
                }
            } 

            longestPath.Insert(0, currTree);

            return longestPath;
        }
    
        private static int FindTreeSum(Tree<int> currTree)
        {
            int sum = currTree.Value;
            foreach (var child in currTree.Children)
            {
                sum += FindTreeSum(child);
            }

            return sum;
        }

        static IList<Tree<int>> FindLongestPathFromLeaf(IEnumerable<Tree<int>> leafs)
        {
            IList<Tree<int>> longestPath = new List<Tree<int>>();
            
            Tree<int> currNode = null;

            foreach (var leaf in leafs)
            {
                currNode = leaf;
                IList<Tree<int>> currentPath = new List<Tree<int>>();                                     
                    do
                    {
                        currentPath.Add(currNode);
                        currNode = currNode.Parent;
                    } while (currNode != null);
               
                    if (currentPath.Count() > longestPath.Count())
                    {
                        longestPath = currentPath;
                    }
            }
            return longestPath;
        }

        static List<Tree<int>> FindSubtreesSum(Tree<int> tree)
        {
            var results = new List<Tree<int>>();
            var currentSum = FindTreeSum(tree);

            if (currentSum == subTreeSum)
            {
                results.Add(tree);
            }

            foreach (var child in tree.Children)
            {
                results.AddRange(FindSubtreesSum(child));
            }

            return results;
        }
    }
}
