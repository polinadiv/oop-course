namespace Lab01;

public static class Task3
{
    public static void Run()
    {
        Console.Write("Введіть рік народження: ");

        if (!int.TryParse(Console.ReadLine(), out int birthYear))
        {
            Console.WriteLine("Помилка: введіть коректний рік.");
            return;
        }

        int age = 2026 - birthYear;

        if (age < 0)
        {
            Console.WriteLine("Помилка: рік народження не може бути більшим за 2026.");
            return;
        }

        string category;

        if (age <= 17)
        {
            category = "дитина";
        }
        else if (age <= 59)
        {
            category = "дорослий";
        }
        else
        {
            category = "пенсіонер";
        }

        Console.WriteLine($"Вік: {age} р.");
        Console.WriteLine($"Категорія: {category}");
    }
}
