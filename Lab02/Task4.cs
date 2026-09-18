using System;

namespace Lab02;

internal class Task4
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine());
        int m = int.Parse(Console.ReadLine());

        int[,] matrix = new int[n, m];

        int maxValue = int.MinValue;
        int maxRow = 0;
        int maxCol = 0;

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split(' ');

            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = int.Parse(parts[j]);

                if (matrix[i, j] > maxValue)
                {
                    maxValue = matrix[i, j];
                    maxRow = i;
                    maxCol = j;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            int rowSum = 0;

            for (int j = 0; j < m; j++)
            {
                rowSum += matrix[i, j];
            }

            Console.WriteLine($"Лікар {i + 1}: {rowSum} прийомів");
        }

        Console.Write("По днях: ");

        for (int j = 0; j < m; j++)
        {
            int colSum = 0;

            for (int i = 0; i < n; i++)
            {
                colSum += matrix[i, j];
            }

            Console.Write(colSum);

            if (j < m - 1)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine();

        Console.WriteLine(
            $"Максимум: {maxValue} (Лікар {maxRow + 1}, День {maxCol + 1})");
    }
}
