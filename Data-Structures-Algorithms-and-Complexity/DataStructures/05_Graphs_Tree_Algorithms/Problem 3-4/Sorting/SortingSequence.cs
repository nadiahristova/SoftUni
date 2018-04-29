using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class SortingSequence
    {
        private static int numSequenceEl;
        private static int numConsecutiveEl;
        private static Queue<int[]> helper;
        private static HashSet<int[]> uniqueSequenceHolder = new HashSet<int[]>();
        private static bool sortingIsPossible;
        private static int[] sortedSequence;
        static void Main()
        {
            AcceptingData();
            LoadUniqueSequences();
            Console.WriteLine(uniqueSequenceHolder.Count);
        }

        private static void LoadUniqueSequences()
        {
            while (helper.Any() && !sortingIsPossible)
            {
                var currSequence = helper.Dequeue();
                MakePossibleShifts(currSequence);
            }
        }

        private static void MakePossibleShifts(int[] currSequence)
        {
            int[] currSeq = currSequence;
            int minIndex = Array.IndexOf(currSeq, currSeq.Min());
            for (int i = minIndex; i >=0; i--)
            {
                int[] curr = currSeq.Select(x => x).ToArray();                
                int[] partialArray = new int[numConsecutiveEl];
                Array.Copy(curr, i, partialArray, 0, numConsecutiveEl);
                Array.Reverse(partialArray);
                var newSequence = curr.Select(x => x).ToArray();
                Array.Copy(partialArray, 0, newSequence, i, numConsecutiveEl);
                int numPossibilities = uniqueSequenceHolder.Count;
                uniqueSequenceHolder.Add(newSequence);

                if (Enumerable.SequenceEqual(sortedSequence, newSequence))
                    sortingIsPossible = true;
                if (numPossibilities < uniqueSequenceHolder.Count)
                    helper.Enqueue(newSequence);
                currSeq = newSequence;
            }
        }

        private static void AcceptingData()
        {
            numSequenceEl = 5; //int.Parse(Console.ReadLine().Trim());
            helper = new Queue<int[]>();
            //int[] initialSequence = Console.ReadLine().Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var initialSequence = new int[] { 5, 4, 3, 2, 1 };
            helper.Enqueue(initialSequence);
            sortedSequence = initialSequence.Select(x => x).ToArray();
            Array.Sort(sortedSequence);            
            numConsecutiveEl = 2; //int.Parse(Console.ReadLine().Trim());
        }
    }
}
