namespace Lab01;

public static class Task7
{
    public static void Run()
    {
        Console.Write("Введіть кількість прийомів: ");

        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Помилка: кількість повинна бути більшою за 0.");
            return;
        }

        decimal[] prices = new decimal[n];

        // Введення даних
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Вартість прийому #{i + 1}: ");

            if (!decimal.TryParse(Console.ReadLine(), out prices[i]) || prices[i] < 0)
            {
                Console.WriteLine("Помилка: введіть коректну вартість.");
                i--;
                continue;
            }
        }

        // Сума, мінімум, максимум
        decimal sum = 0;
        decimal min = prices[0];
        decimal max = prices[0];

        foreach (decimal price in prices)
        {
            sum += price;

            if (price < min)
                min = price;

            if (price > max)
                max = price;
        }

        decimal average = sum / n;

        // Кількість значень вище середнього
        int aboveAverageCount = 0;

        for (int i = 0; i < n; i++)
        {
            if (prices[i] > average)
            {
                aboveAverageCount++;
            }
        }

        // Пошук першого прийому дорожчого за 1000 грн
        int firstExpensiveIndex = -1;
        int index = 0;

        while (index < n)
        {
            if (prices[index] > 1000)
            {
                firstExpensiveIndex = index;
                break;
            }

            index++;
        }

        Console.WriteLine();
        Console.WriteLine("=== Звіт по прийомах ===");
        Console.WriteLine($"Кількість: {n}");
        Console.WriteLine($"Загальна сума: {sum:F2} грн");
        Console.WriteLine($"Середня: {average:F2} грн");
        Console.WriteLine($"Мін / Макс: {min:F2} / {max:F2} грн");
        Console.WriteLine($"Вище середнього: {aboveAverageCount} з {n}");

        if (firstExpensiveIndex == -1)
        {
            Console.WriteLine("Перший > 1000: немає");
        }
        else
        {
            Console.WriteLine(
                $"Перший > 1000: #{firstExpensiveIndex + 1} — {prices[firstExpensiveIndex]} грн");
        }

        Console.WriteLine("=========================");
    }
}
