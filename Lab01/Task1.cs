namespace Lab01;

public static class Task1
{
    public static void Run()
    {
        Console.Write("Введіть вагу (кг): ");

        if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
        {
            Console.WriteLine("Помилка: вага повинна бути додатним числом.");
            return;
        }

        Console.Write("Введіть зріст (м): ");

        if (!double.TryParse(Console.ReadLine(), out double heightMeters) || heightMeters <= 0)
        {
            Console.WriteLine("Помилка: зріст повинен бути додатним числом.");
            return;
        }

        double bmi = weight / (heightMeters * heightMeters);

        string category = bmi switch
        {
            < 18.5 => "недобір ваги",
            < 25.0 => "норма",
            < 30.0 => "надвага",
            _ => "ожиріння"
        };

        Console.WriteLine($"ІМТ: {bmi:F1} — {category}");
    }
}