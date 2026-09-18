using System;

namespace Lab02;

internal class Task8
{
    public static void Run()
    {
        int d = int.Parse(Console.ReadLine());
        int w = int.Parse(Console.ReadLine());

        int[,,] data = new int[d, w, 2];
        int[] departmentTotals = new int[d];

        for (int dep = 0; dep < d; dep++)
        {
            for (int week = 0; week < w; week++)
            {
                for (int shift = 0; shift < 2; shift++)
                {
                    data[dep, week, shift] = int.Parse(Console.ReadLine());

                    departmentTotals[dep] += data[dep, week, shift];
                }
            }
        }

        for (int dep = 0; dep < d; dep++)
        {
            Console.WriteLine($"Відділення {dep + 1}:");

            for (int week = 0; week < w; week++)
            {
                int morning = data[dep, week, 0];
                int evening = data[dep, week, 1];
                int total = morning + evening;

                Console.WriteLine(
                    $"  Тиждень {week + 1}: ранок {morning}, вечір {evening} → разом {total}");
            }

            Console.WriteLine($"Разом: {departmentTotals[dep]} пацієнтів");
        }

        int bestDepartment = 0;

        for (int i = 1; i < d; i++)
        {
            if (departmentTotals[i] > departmentTotals[bestDepartment])
            {
                bestDepartment = i;
            }
        }

        Console.WriteLine(
            $"Найзавантаженіше: Відділення {bestDepartment + 1} ({departmentTotals[bestDepartment]} пацієнтів)");
    }
}
