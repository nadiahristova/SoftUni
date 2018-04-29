using System;
using System.Collections.Generic;
using System.Linq;

namespace Recursion_Homework
{
    class PathsBetweenCellsInMatrix
    {
        static char[,] matrix = 
        {
            {'S', ' ', ' ', ' ', ' ', ' '},
            {' ', '*', '*', ' ', '*', ' '},
            {' ', '*', '*', ' ', '*', ' '},
            {' ', '*', 'E', ' ', ' ', ' '},
            {' ', ' ', ' ', '*', ' ', ' '},
        };

        static int pathsFound = 0;
        static List<char> paths = new List<char>();

        static readonly int mRows = matrix.GetLength(0);
        static readonly int mCols = matrix.GetLength(1);

        static void Main()
        {
            try
            {
                int[] stPos = FindStartPosition();
                FindPathsInMatrix(stPos[0], stPos[1], 'S');
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private static int[] FindStartPosition()
        {
            for (int row = 0; row < mRows; row++)
            {
                for (int col = 0; col < mCols; col++)
                {
                    if (matrix[row, col] == 'S')
                    {
                        return new int[] { row, col };
                    }
                }
                Console.WriteLine();
            }

            throw new Exception("No starting possition found.");
        }

        private static void FindPathsInMatrix(int row, int col, char ch)
        {
            if (!InRange(row, col) || (matrix[row, col] != ' ' && matrix[row, col] != 'E' && !(matrix[row, col] == 'S' && ch == 'S')))
            {
                return;
            }


            if (matrix[row, col] == 'E')
            {
                PrintPathOut();
                return;
            }

            paths.Add(ch);
            matrix[row, col] = ch;

            FindPathsInMatrix(row - 1, col, 'U'); //Up
            FindPathsInMatrix(row, col + 1, 'R'); //Right
            FindPathsInMatrix(row + 1, col, 'D'); //Down
            FindPathsInMatrix(row, col - 1, 'L'); //Up

            matrix[row, col] = ' ';
            paths.RemoveAt(paths.Count-1);
        }

        private static void PrintPathOut()
        {
            for (int row = 0; row < mRows; row++)
            {
                for (int col = 0; col < mCols; col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("{0}-th path to freedom: {1} => Exit\n", ++pathsFound, string.Join(" -> ", paths));
            
        }

        private static bool InRange(int row, int col)
        {
            bool inRange = row >= 0 && row < mRows && col >= 0 && col < mCols;

            return inRange;
        }
    }
}
