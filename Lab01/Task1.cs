namespace Lab01;

public static class Task1
{
    public static void Run()
    {
        if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
        {
            Console.WriteLine("Помилка");
            return;
        }

        if (!double.TryParse(Console.ReadLine(), out double height) || height <= 0)
        {
            Console.WriteLine("Помилка");
            return;
        }

        double bmi = weight / (height * height);

        Console.WriteLine($"ІМТ: {bmi:F2}");
    }
}