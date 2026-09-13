namespace Lab01;

public static class Task4
{
    public static void Run()
    {
        Console.Write("Введіть систолічний тиск: ");

        if (!int.TryParse(Console.ReadLine(), out int systolic) || systolic <= 0)
        {
            Console.WriteLine("Помилка: введіть коректний тиск.");
            return;
        }

        Console.Write("Введіть діастолічний тиск: ");

        if (!int.TryParse(Console.ReadLine(), out int diastolic) || diastolic <= 0)
        {
            Console.WriteLine("Помилка: введіть коректний тиск.");
            return;
        }

        string category;

        if (systolic < 120 && diastolic < 80)
        {
            category = "норма";
        }
        else if (systolic < 130 && diastolic < 80)
        {
            category = "підвищений";
        }
        else if (systolic < 140 || diastolic < 90)
        {
            category = "гіпертонія 1 ступеня";
        }
        else
        {
            category = "гіпертонія 2 ступеня";
        }

        Console.WriteLine($"Тиск: {systolic}/{diastolic} — {category}");
    }
}
