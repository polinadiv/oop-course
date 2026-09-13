namespace Lab01;

public static class Task2
{
    public static void Run()
    {
        Console.Write("Введіть базову вартість послуги (грн): ");

        if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
        {
            Console.WriteLine("Помилка: вартість повинна бути невід'ємним числом.");
            return;
        }

        Console.Write("Чи є у вас страховка або дисконтна картка? (yes/no): ");

        string answer = Console.ReadLine()?.Trim().ToLower() ?? "";

        if (answer is not ("yes" or "no"))
        {
            Console.WriteLine("Помилка: введіть 'yes' або 'no'.");
            return;
        }

        double discount = answer switch
        {
            "yes" => 0.15,
            _ => 0.0
        };

        double finalPrice = price * (1 - discount);

        Console.WriteLine($"Базова ціна: {price:F2} грн");
        Console.WriteLine($"Знижка: {discount:P0}");
        Console.WriteLine($"Фінальна вартість до сплати: {finalPrice:F2} грн");
    }
}
