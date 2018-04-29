using System;
using System.Collections.Generic;
using System.Linq;
public class GraphConnectedComponents
{
    static bool[] visitedNodes;
    static new List<int>[] graph = new List<int>[]
    {
        new List<int>(){3, 6},  //0
        new List<int>(){3, 4, 5, 6},   //1
        new List<int>(){8}, //2
        new List<int>(){0, 1, 5},   //3
        new List<int>(){1, 6},  //4
        new List<int>(){1, 3},  //5
        new List<int>(){0, 1, 4},   //6
        new List<int>(){},  //7
        new List<int>(){2}  //8
    };
    static List<int>[] ReadGraph()
    {
        int numOfNodes = int.Parse(Console.ReadLine());
        var graph = new List<int>[numOfNodes];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = Console.ReadLine().Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        }

        return graph;
    }
    static void DFS(int node)
    {
        if(!visitedNodes[node])
        {
            visitedNodes[node] = true;
            foreach(var connectdNode in graph[node])
            {
                DFS(connectdNode);
            }

            Console.Write(" " + node);
        }
    }
    static void FindConnectedNodes()
    {
        visitedNodes = new bool[graph.Length];
        for (var currNode = 0; currNode < graph.Length; currNode++ )
        {
            if(!visitedNodes[currNode])
            {
                Console.Write("Connected component:");
                DFS(currNode);
                Console.WriteLine();
            }
        }
    }
    public static void Main()
    {
        graph = ReadGraph();
        FindConnectedNodes();
    }
}
