namespace Lab01;

public static class Task2
{
    public static void Run()
    {
        Console.Write("Введіть ціну одного прийому (грн): ");

        if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
        {
            Console.WriteLine("Помилка: введіть коректну ціну.");
            return;
        }

        Console.Write("Введіть кількість прийомів: ");

        if (!int.TryParse(Console.ReadLine(), out int count) || count < 0)
        {
            Console.WriteLine("Помилка: введіть коректну кількість.");
            return;
        }

        Console.Write("Введіть знижку (%): ");

        if (!int.TryParse(Console.ReadLine(), out int discount) ||
            discount < 0 || discount > 100)
        {
            Console.WriteLine("Помилка: введіть коректну знижку від 0 до 100.");
            return;
        }

        double total =
            price * count * (1 - discount / 100.0);

        Console.WriteLine($"Сума: {total:F2} грн");
    }
}
