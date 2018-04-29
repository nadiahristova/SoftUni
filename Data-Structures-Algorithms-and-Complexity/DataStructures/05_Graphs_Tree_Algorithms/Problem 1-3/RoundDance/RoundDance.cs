using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundDance
{
    class RoundDance
    {
        public static void Main()
        {
            Graph<int> graph = new Graph<int>();
            graph.ReadInputData();
            graph.FindLongestPath(graph.Leader);
            Console.WriteLine("\nThe number of ppl taking part in the biggest round dance with leader({0}) is {1}.", graph.Leader, graph.MostPplInDance);
        }
    }
}
