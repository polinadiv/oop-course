/* namespace ClinicApp;

public class Program
{
    public static void Main()
    {
        Patient p1 = new Patient(
            "Іван",
            "Петренко",
            new DateTime(1985, 5, 15),
            "A+",
            "0501234567");

        Patient p2 = new Patient(
            "Олена",
            "Коваль",
            new DateTime(1993, 8, 20),
            "B-",
            "0672345678");

        Patient p3 = new Patient(
            "Максим",
            "Бойко",
            new DateTime(2009, 10, 1),
            "O+",
            "0933456789");

        Patient p4 = new Patient();

        Patient p5 = new Patient(
            "Марія",
            "Ткач");

        Console.WriteLine(p1);
        Console.WriteLine(p2);
        Console.WriteLine(p3);
        Console.WriteLine(p4);
        Console.WriteLine(p5);
    }
} */
namespace ClinicApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Doctor doctor1 = new Doctor(
                "Олег",
                "Сидоренко",
                "Кардіологія",
                "LIC-001",
                "+380501111111");

            Doctor doctor2 = new Doctor(
                "Наталія",
                "Мороз",
                "Неврологія",
                "LIC-002",
                "+380502222222");

            Doctor doctor3 = new Doctor(
                "Андрій",
                "Власенко",
                "Педіатрія",
                "LIC-003",
                "+380503333333");

            Doctor doctor4 = new Doctor(
                "Ірина",
                "Коваль",
                "Дерматологія",
                "LIC-004",
                "+380504444444");

            doctor2.WorkStartHour = 10;
            doctor2.WorkEndHour = 19;

            doctor3.WorkStartHour = 7;
            doctor3.WorkEndHour = 15;

            Console.WriteLine(doctor1);
            Console.WriteLine(doctor2);
            Console.WriteLine(doctor3);
            Console.WriteLine(doctor4);

            Console.WriteLine();

            Console.WriteLine("Доступні зараз:");

            Doctor[] doctors =
            {
                doctor1,
                doctor2,
                doctor3,
                doctor4
            };

            for (int i = 0; i < doctors.Length; i++)
            {
                if (doctors[i].IsAvailableNow)
                {
                    Console.WriteLine(doctors[i].FullName);
                }
            }

            Console.WriteLine();
        }
    }
}
