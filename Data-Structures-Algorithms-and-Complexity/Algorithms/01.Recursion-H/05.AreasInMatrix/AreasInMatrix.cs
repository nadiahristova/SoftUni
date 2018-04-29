using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursion_Homework
{
    class AreasInMatrix
    {
        static char[,] matrix = 
        {
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
            {'*', ' ', ' ', '*', '*', '*', '*', '*', ' ', ' '},
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
        };

        private static int areaNum = 0;
        private static SortedSet<Area> areas = new SortedSet<Area>();
        private static Area currArea = null;

        static readonly int mRows = matrix.GetLength(0);
        static readonly int mCols = matrix.GetLength(1);

        static void Main()
        {
            FindAreas();
            PrintResults();
        }

        private static void PrintResults()
        {
            if (areas.Count == 0)
            {
                Console.WriteLine("No areas find in teh given matrix;");
            }
            else
            {
                Console.WriteLine("Total areas found: " + areaNum);
                foreach(var area in areas)
                {
                    Console.WriteLine(area.ToString());
                }
            }
        }

        private static void FindAreas()
        {
            for (int row = 0; row < mRows; row++)
            {
                for (int col = 0; col < mCols; col++)
                {
                    if (matrix[row, col] != '*')
                    {
                        currArea = new Area(++areaNum, row, col);
                        MarkArea(row, col);
                        areas.Add(currArea);
                    }
                }
            }
        }

        private static void MarkArea(int row, int col)
        {
            if (!InRange(row, col) || matrix[row, col] == '*')
            {
                return;
            }

            matrix[row, col] = '*';
            currArea.Size++;

            MarkArea(row - 1, col); //Up
            MarkArea(row, col + 1); //Right
            MarkArea(row + 1, col); //Down
            MarkArea(row, col - 1); //Up
        }

        private static bool InRange(int row, int col)
        {
            bool inRange = row >= 0 && row < mRows && col >= 0 && col < mCols;

            return inRange;
        }
    }
}
