using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalTree
{
    class PlayWithIntervalTree
    {
        static void Main()
        {
            var intIntTree = new IntervalTree<int>();
            intIntTree.Insert(new Interval<int>(-5, 20));
            intIntTree.Insert(new Interval<int>(20, 30));
            intIntTree.Insert(new Interval<int>(14, 40));
            intIntTree.Insert(new Interval<int>(11, 20));
            intIntTree.Insert(new Interval<int>(8, 15));
            intIntTree.Insert(new Interval<int>(-3, 21));
            intIntTree.Insert(new Interval<int>(111, 115));
            intIntTree.Insert(new Interval<int>(-55, 5));

            DisplayTree(intIntTree.Root, string.Empty);

            var intervalInQuestion = new Interval<int>(12, 15);
            var intervals = intIntTree.FindOverlappingIntervals(intervalInQuestion);
            PrintOverlappedIntervals(intervals, intervalInQuestion);
        }
        private static void DisplayTree(IntervalTree<int>.AANode node, string intend)
        {
            Console.WriteLine(intend + node.interval.ToString() + " (level:" + node.level + ")");
            if (node.leftChild.level != 0)
            {
                DisplayTree(node.leftChild, intend + "  ");
            }
            if (node.rightChild.level != 0)
            {
                DisplayTree(node.rightChild, intend + "  ");
            }
        }

        private static void PrintOverlappedIntervals(ICollection<Interval<int>> intervals, Interval<int> intervalInQuestion)
        {
            Console.WriteLine("\nIntervals that overlap with " + intervalInQuestion.ToString());
            foreach(var i in intervals)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }
}
