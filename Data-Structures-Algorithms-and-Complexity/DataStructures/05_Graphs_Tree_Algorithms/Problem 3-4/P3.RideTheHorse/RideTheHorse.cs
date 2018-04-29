using System;
using System.Collections.Generic;
using System.Linq;

namespace RideTheHorse
{
    class RideTheHorse
    {
        private static int rowsCount;
        private static int colmnCount;
        private static Queue<BoardSquare> helper = new Queue<BoardSquare>();
        private static BoardSquare[][] board;
        private static BoardSquare currBoardSquare;
        private static int turn = 1;
        private static int prevValue;
        private static int countVisitedSquares;

        static void Main()
        {
            GenerateInitialBoard();
            AssignNewValuesToBoard();
            PrintTheBoard();
            PrintMiddleColumn();
        }

        private static void PrintMiddleColumn()
        {
            Console.WriteLine();
            int maxSpaceForValue = (int)Math.Ceiling(Math.Log10(turn));
            string horizontalBorder = new String('-', maxSpaceForValue + 4);
            int colmn = board.GetLength(0) / 2;
            Console.WriteLine(horizontalBorder);
            foreach(var row in board)
            {
                Console.WriteLine("| " + returnPrettyValue(row[colmn].Value.ToString(), maxSpaceForValue) + " |");
                Console.WriteLine(horizontalBorder);
            }
        }

        private static void PrintTheBoard()
        {
            Console.WriteLine();
            int maxSpaceForValue = (int)Math.Ceiling(Math.Log10(turn));
            string horizontalBorder = new String('-', 3 * (colmnCount - 1) + colmnCount * (maxSpaceForValue) + 4);
            Console.WriteLine(horizontalBorder);

            for (int row = 0; row < rowsCount; row++)
            {
                var valuesOnRow = board[row].Select(s => s.Value).ToList();
                Console.WriteLine("| " + String.Join(" | ", valuesOnRow.Select(v => returnPrettyValue(v.ToString(), maxSpaceForValue))) + " |");
                Console.WriteLine(horizontalBorder);
            }
        }

        private static string returnPrettyValue(string v, int length)
        {
            int spaces = length - v.Length;
            int padLeft = spaces / 2 + v.Length;
            return v.PadLeft(padLeft).PadRight(length);
        }

        private static void AssignNewValuesToBoard()
        {
            int startRow = int.Parse(Console.ReadLine().Trim());
            int startColmn = int.Parse(Console.ReadLine().Trim());
            MarkSqare(startColmn, startRow);

            while(helper.Count != 0)
            {
                currBoardSquare = helper.Dequeue();
                if(prevValue != currBoardSquare.Value)
                {
                    prevValue++;
                    turn++;
                }
                LoadPossiblePositionsInQueue();                
            }
        }

        private static void LoadPossiblePositionsInQueue()
        {
            int[] newPositions = new int[] { 1, 2 };

            for (int horseTurn = 0; horseTurn < 4; horseTurn++)
            {
                if (countVisitedSquares == rowsCount * colmnCount) break;
                if (horseTurn % 2 == 0)
                    newPositions[0] *= -1;
                else
                    newPositions[1] *= -1;
                UpdateTwoBoardSquares(newPositions);
            }
        }

        private static void UpdateTwoBoardSquares(int[] newPositions)
        {
            int x = currBoardSquare.X + newPositions[0];
            int y = currBoardSquare.Y + newPositions[1];
            if (x >= 0 && x < colmnCount && y >= 0 && y < rowsCount)
                MarkSqare(x, y);

            x = currBoardSquare.X + newPositions[1];
            y = currBoardSquare.Y + newPositions[0];
            if (x >= 0 && x < colmnCount && y >= 0 && y < rowsCount)
                MarkSqare(x, y);
        }

        private static void MarkSqare(int x, int y)
        {
            if (countVisitedSquares == rowsCount * colmnCount) return;
            BoardSquare visitedSquare = board[y][x];
            if(visitedSquare.visited == false)
            {
                visitedSquare.visited = true;
                countVisitedSquares++;
                visitedSquare.Value = turn;
                helper.Enqueue(visitedSquare);
            }
        }

        private static void GenerateInitialBoard()
        {
            rowsCount = int.Parse(Console.ReadLine().Trim());
            colmnCount = int.Parse(Console.ReadLine().Trim());
            board = new BoardSquare[rowsCount][];

            for (int row = 0; row < rowsCount; row++)
            {
                board[row] = new BoardSquare[colmnCount];
                for (int colmn = 0; colmn < colmnCount; colmn++)
                {
                    board[row][colmn] = new BoardSquare(colmn, row);
                }
            }
        }
    }
}
