namespace MatrixMultiplier
{
    using System;

    class MatrixMultiplier
    {
        static void Main(string[] args)
        {
            var multiplicandMatrix = new double[,] { { 1, 3 }, { 5, 7 } };
            var multiplierMatrix = new double[,] { { 4, 2 }, { 1, 5 } };
            var productMatrix = MultiplyMatrices(multiplicandMatrix, multiplierMatrix);

            for (int row = 0; row < productMatrix.GetLength(0); row++)
            {
                for (int colmn = 0; colmn < productMatrix.GetLength(1); colmn++)
                {
                    Console.Write(productMatrix[row, colmn] + " ");
                }
                Console.WriteLine();
            }

        }

        static double[,] MultiplyMatrices(double[,] multiplicandMatrix, double[,] multiplierMatrix)
        {
            if (multiplicandMatrix.GetLength(1) != multiplierMatrix.GetLength(0))
            {
                throw new InvalidOperationException("Cannot multiply two matrices with diffrent count of rows and columns.");
            }

            var multiplicandColumnsCount = multiplicandMatrix.GetLength(1);
            var productMatrix = new double[multiplicandMatrix.GetLength(0), multiplierMatrix.GetLength(1)];
            for (int productMatrixRow = 0; productMatrixRow < productMatrix.GetLength(0); productMatrixRow++)
                for (int productMatrixColumn = 0; productMatrixColumn < productMatrix.GetLength(1); productMatrixColumn++)
                    for (int multiplicandMatrixColumn = 0; multiplicandMatrixColumn < multiplicandColumnsCount; multiplicandMatrixColumn++)
                        productMatrix[productMatrixRow, productMatrixColumn] += multiplicandMatrix[productMatrixRow, multiplicandMatrixColumn] * multiplierMatrix[multiplicandMatrixColumn, productMatrixColumn];

            return productMatrix;
        }
    }
}