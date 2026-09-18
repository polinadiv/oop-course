using System;

namespace Lab02;

internal class Task6
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine());

        int[][] costs = new int[n][];

        int bestDoctor = 0;
        int bestIncome = 0;

        for (int i = 0; i < n; i++)
        {
            int k = int.Parse(Console.ReadLine());

            costs[i] = new int[k];

            int sum = 0;

            for (int j = 0; j < k; j++)
            {
                costs[i][j] = int.Parse(Console.ReadLine());
                sum += costs[i][j];
            }

            double average = (double)sum / k;

            Console.WriteLine(
                $"Лікар {i + 1}: {k} прийоми, сума={sum} грн, середнє={average:F1} грн");

            if (i == 0 || sum > bestIncome)
            {
                bestIncome = sum;
                bestDoctor = i;
            }
        }

        Console.WriteLine(
            $"Найбільший дохід: Лікар {bestDoctor + 1} ({bestIncome} грн)");
    }
}
