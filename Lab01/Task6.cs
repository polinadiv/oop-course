namespace Lab01;

public static class Task6
{
    public static void Run()
    {
        Console.Write("Введіть номер медичної картки: ");

        if (!int.TryParse(Console.ReadLine(), out int cardNumber) || cardNumber <= 0)
        {
            Console.WriteLine("Помилка: введіть коректний номер картки.");
            return;
        }

        int lastDigit = cardNumber % 10;

        string department = lastDigit switch
        {
            0 or 1 => "загальна терапія",
            2 or 3 => "хірургія",
            4 or 5 => "кардіологія",
            6 or 7 => "неврологія",
            8 or 9 => "офтальмологія",
            _ => "невідомо"
        };

        string privileged = cardNumber % 2 == 0 ? "так" : "ні";
        string examination = cardNumber % 3 == 0 ? "так" : "ні";

        Console.WriteLine($"Відділення: {department}");
        Console.WriteLine($"Пільгова: {privileged}");
        Console.WriteLine($"Огляд: {examination}");
    }
}
