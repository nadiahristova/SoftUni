using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

using System.ComponentModel;
using System.Text.RegularExpressions;

namespace RoundDance
{
    public class Graph<T>
    {
        private static Dictionary<T, List<T>> graph = new Dictionary<T,List<T>>();
        private static Dictionary<T, bool> nodeStateHolder = new Dictionary<T, bool>();
        private int currLength;
        private int maxLength;
        public int MostPplInDance { get { return this.maxLength; } }
        private static int numFriendships;
        public T Leader { get; set;}
        private class GraphNode<T>
        {
            public T Value { get; private set; }
            public bool Visited {get; set;}
            public GraphNode(T value)
            {
                this.Value = value;
            }
        }
        
        internal void ReadInputData()
        {
            numFriendships = int.Parse(Console.ReadLine().Trim());
            this.Leader = Convert(Console.ReadLine().Trim());
            for (int i = 0; i < numFriendships; i++)
            {
                string[] data = Regex.Split(Console.ReadLine().Trim(), @"\s+").ToArray();
                ConstructConnections(data);
            }
        }

        private void ConstructConnections(string[] data)
        {
            T firstNode = Convert(data[0]);
            T secondNode = Convert(data[1]);
            if (!graph.ContainsKey(firstNode))
                graph[firstNode] = new List<T>();
            if (!graph.ContainsKey(secondNode))
                graph[secondNode] = new List<T>();
            graph[firstNode].Add(secondNode);
            graph[secondNode].Add(firstNode);

            foreach(var key in graph.Keys)
            {
                nodeStateHolder[key] = true;
            }
        }
        private static T Convert(string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(input);
                }
            }
            catch(Exception)
            {
                throw new ArgumentException("You are using nonbuild-in type for the Graph Value.");
            }       

            return default(T);
        }

        internal void FindLongestPath(T node)
        {
            if(currLength > maxLength)
            {
                maxLength = currLength;
            }

            if (nodeStateHolder[node])
            {
                currLength++;
                nodeStateHolder[node] = false;
                foreach(var comradNode in graph[node])
                {
                   FindLongestPath(comradNode);
                }
            }

            currLength = 1;
        }
    }
}
