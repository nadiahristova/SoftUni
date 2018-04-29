namespace Minesweeper.Core
{
    using Models;

    using System;
    using System.Collections.Generic;

    internal class Engine
    {

        private const int maxFreeCells = 35;
        private const int MinefieldRows = 5;
        private const int MinefieldColumns = 10;
        private const int MaxBombCount = 15;

        private static char[,] minefiled;
        private static char[,] disclosedMinefield;
        private static int turnCount = 0;
        private static bool isGameOver;
        private static int row = 0;
        private static int column = 0;
        private static bool isGameBeginning = true;
        private static bool isMinefieldCleared = false;
        private static List<Player> champions = new List<Player>();

        internal virtual void Run()
        {
            string command = null;
            PrepareNewGame();

            do
            {
                if (isGameBeginning)
                {
                    Console.WriteLine(
                        "Welcome to “Mineweeper”. Try to clear the board without detonating any of the hidden \"mines\" ."
                        + " Type in 'top' to view the top players list, 'restart' to restart the game or 'exit' to exit the game.");
                    DrawMinefield(minefiled);
                    isGameBeginning = false;
                }

                Console.Write("Enter row and column: ");
                command = Console.ReadLine().Trim();
                ExecuteCommand(command);

                if (isGameOver)
                {
                    DrawMinefield(disclosedMinefield);
                    Console.Write("\nYour score: {0} points. " + "Your name: ", turnCount);
                    string name = Console.ReadLine();
                    AddCurrentPlayerToRankList(name, false);

                    PrintRankList();
                    PrepareNewGame();
                }

                if (isMinefieldCleared)
                {
                    Console.WriteLine("\nCongratulations! You successfully cleared the minefield!");
                    DrawMinefield(disclosedMinefield);
                    Console.WriteLine("Your name: ");
                    string playerName = Console.ReadLine();
                    AddCurrentPlayerToRankList(playerName, true);

                    PrintRankList();
                    PrepareNewGame();
                }
            }
            while (command != "exit");
        }


        private static void AddCurrentPlayerToRankList(string name, bool isUndesputedChampion)
        {
            Player player = new Player(name, turnCount);

            if (isUndesputedChampion)
            {
                champions.Add(player);
            }
            else
            {
                if (champions.Count < 5)
                {
                    champions.Add(player);
                }
                else
                {
                    for (int i = 0; i < champions.Count; i++)
                    {
                        if (champions[i].Points < player.Points)
                        {
                            champions.Insert(i, player);
                            champions.RemoveAt(champions.Count - 1);
                            break;
                        }
                    }
                }
            }
        }

        private static void ExecuteCommand(string command)
        {
            command = ParseCommand(command);

            switch (command)
            {
                case "top":
                    PrintRankList();
                    break;
                case "restart":
                    PrepareNewGame();
                    DrawMinefield(minefiled);
                    break;
                case "exit":
                    Console.WriteLine("Ty for participating.");
                    break;
                case "turn":
                    if (disclosedMinefield[row, column] != '*')
                    {
                        if (disclosedMinefield[row, column] == '-')
                        {
                            InsertSafeCellInMinefield();
                            turnCount++;
                        }

                        if (maxFreeCells == turnCount)
                        {
                            isMinefieldCleared = true;
                        }
                        else
                        {
                            DrawMinefield(minefiled);
                        }
                    }
                    else
                    {
                        isGameOver = true;
                    }

                    break;
                default:
                    Console.WriteLine("\nInvalid command. Try again.\n");
                    break;
            }
        }

        private static string ParseCommand(string command)
        {
            if (command.Length >= 3)
            {
                if (int.TryParse(command[0].ToString(), out row) && int.TryParse(command[2].ToString(), out column)
                    && row <= minefiled.GetLength(0) && column <= minefiled.GetLength(1))
                {
                    command = "turn";
                }
            }

            return command;
        }

        private static void PrepareNewGame()
        {
            minefiled = DrawUndisclosedMinefield();
            disclosedMinefield = GenerateMinefield();
            turnCount = 0;
            isGameBeginning = true;
            isGameOver = false;
            isMinefieldCleared = false;
        }

        private static void PrintRankList()
        {
            SortChampions();

            Console.WriteLine("\nRating:");
            if (champions.Count > 0)
            {
                for (int i = 0; i < champions.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} cells", i + 1, champions[i].Name, champions[i].Points);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There are no champions that left their mark at the moment.\n");
            }
        }

        private static void SortChampions()
        {
            champions.Sort((Player player1, Player player2) => player2.Name.CompareTo(player1.Name));
            champions.Sort((Player player1, Player player2) => player2.Points.CompareTo(player1.Points));
        }

        /// <summary>
        /// Inserts the number of bombs which are in direct contact with a given cell, into the undisclosed minefield. 
        /// </summary>
        private static void InsertSafeCellInMinefield()
        {
            char bombsCount = CalculateNumerOfBombsNextToCell(row, column);
            disclosedMinefield[row, column] = bombsCount;
            minefiled[row, column] = bombsCount;
        }

        private static void DrawMinefield(char[,] board)
        {
            int boardRows = board.GetLength(0);
            int boardColumns = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < boardRows; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < boardColumns; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] DrawUndisclosedMinefield()
        {
            char[,] minefield = new char[MinefieldRows, MinefieldColumns];
            for (int row = 0; row < MinefieldRows; row++)
            {
                for (int column = 0; column < MinefieldColumns; column++)
                {
                    minefield[row, column] = '?';
                }
            }

            return minefield;
        }

        /// <summary>
        /// Generates minefield with random distributed bombs in ti. 
        /// </summary>
        private static char[,] GenerateMinefield()
        {
            char[,] minefield = new char[MinefieldRows, MinefieldColumns];

            for (int i = 0; i < minefield.GetLength(0); i++)
            {
                for (int j = 0; j < minefiled.GetLength(1); j++)
                {
                    minefield[i, j] = '-';
                }
            }

            List<int> bombs = GenerateBombs();

            foreach (int bomb in bombs)
            {
                int column = bomb / MinefieldColumns;
                int row = bomb % MinefieldColumns;
                if (row == 0 && bomb != 0)
                {
                    column--;
                    row = MinefieldColumns;
                }
                else
                {
                    row++;
                }

                minefield[column, row - 1] = '*';
            }

            return minefield;
        }

        /// <summary>
        /// Generates one dimensional list for random distributed bombs. 
        /// </summary>
        private static List<int> GenerateBombs()
        {
            List<int> bombs = new List<int>();
            while (bombs.Count < MaxBombCount)
            {
                Random random = new Random();
                int bomb = random.Next(MinefieldRows * MinefieldColumns);
                if (!bombs.Contains(bomb))
                {
                    bombs.Add(bomb);
                }
            }

            return bombs;
        }

        /// <summary>
        /// Takes a given cell and checks how many bombs are there near it. 
        /// </summary>
        /// <param name="row"> Given row of teh minefield</param>
        /// <param name="column"> Given column of teh minefield</param>
        private static char CalculateNumerOfBombsNextToCell(int row, int column)
        {
            int count = 0;
            int rows = disclosedMinefield.GetLength(0);
            int columns = disclosedMinefield.GetLength(1);

            if (row - 1 >= 0)
            {
                if (disclosedMinefield[row - 1, column] == '*')
                {
                    count++;
                }
            }

            if (row + 1 < rows)
            {
                if (disclosedMinefield[row + 1, column] == '*')
                {
                    count++;
                }
            }

            if (column - 1 >= 0)
            {
                if (disclosedMinefield[row, column - 1] == '*')
                {
                    count++;
                }
            }

            if (column + 1 < columns)
            {
                if (disclosedMinefield[row, column + 1] == '*')
                {
                    count++;
                }
            }

            if ((row - 1 >= 0) && (column - 1 >= 0))
            {
                if (disclosedMinefield[row - 1, column - 1] == '*')
                {
                    count++;
                }
            }

            if ((row - 1 >= 0) && (column + 1 < columns))
            {
                if (disclosedMinefield[row - 1, column + 1] == '*')
                {
                    count++;
                }
            }

            if ((row + 1 < rows) && (column - 1 >= 0))
            {
                if (disclosedMinefield[row + 1, column - 1] == '*')
                {
                    count++;
                }
            }

            if ((row + 1 < rows) && (column + 1 < columns))
            {
                if (disclosedMinefield[row + 1, column + 1] == '*')
                {
                    count++;
                }
            }

            return char.Parse(count.ToString());
        }
    }
}
