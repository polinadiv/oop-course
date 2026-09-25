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
}
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
using System.Text;
namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        PatientManager manager = new PatientManager();

        manager.Add(new Patient(
            "Іван",
            "Петренко",
            new DateTime(1985, 5, 10),
            "A(II)",
            "0501111111"));

        manager.Add(new Patient(
            "Олена",
            "Коваль",
            new DateTime(1992, 8, 15),
            "B(III)",
            "0502222222"));

        manager.Add(new Patient(
            "Максим",
            "Бойко",
            new DateTime(2010, 3, 12),
            "O(I)",
            "0503333333"));

        manager.Add(new Patient(
            "Марія",
            "Ткач",
            new DateTime(1999, 11, 20),
            "AB(IV)",
            "0504444444"));

        PatientsMenu(manager);
    }

    static void PatientsMenu(PatientManager manager)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Пацієнти ===");
            Console.WriteLine("1 - Показати всіх");
            Console.WriteLine("2 - Додати");
            Console.WriteLine("3 - Знайти за ім'ям");
            Console.WriteLine("4 - Видалити");
            Console.WriteLine("5 - Статистика");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();
            Console.WriteLine("Введено: " + choice);

            switch (choice)
            {
                case "1":
                    manager.DisplayAll();
                    break;

                case "2":
                    AddPatient(manager);
                    break;

                case "3":
                    FindPatient(manager);
                    break;

                case "4":
                    RemovePatient(manager);
                    break;

                case "5":
                    manager.DisplayStats();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }

    static void AddPatient(PatientManager manager)
    {
        Console.Write("Ім'я: ");
        string firstName = Console.ReadLine();

        Console.Write("Прізвище: ");
        string lastName = Console.ReadLine();

        Console.Write("Рік народження: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Місяць народження: ");
        int month = int.Parse(Console.ReadLine());

        Console.Write("День народження: ");
        int day = int.Parse(Console.ReadLine());

        Console.Write("Група крові: ");
        string bloodType = Console.ReadLine();

        Console.Write("Телефон: ");
        string phone = Console.ReadLine();

        Patient patient = new Patient(
            firstName,
            lastName,
            new DateTime(year, month, day),
            bloodType,
            phone);

        manager.Add(patient);
    }

    static void FindPatient(PatientManager manager)
    {
        Console.Write("Введіть ім'я або прізвище: ");
        string search = Console.ReadLine();

        Patient[] patients = manager.FindByName(search);

        Console.WriteLine();

        if (patients.Length == 0)
        {
            Console.WriteLine("Нічого не знайдено.");
            return;
        }

        Console.WriteLine("Знайдено:");

        for (int i = 0; i < patients.Length; i++)
        {
            Console.WriteLine(patients[i]);
        }
    }

    static void RemovePatient(PatientManager manager)
    {
        Console.Write("Введіть ID пацієнта: ");
        int id = int.Parse(Console.ReadLine());

        bool removed = manager.Remove(id);

        if (removed)
        {
            Console.WriteLine("Пацієнта видалено.");
        }
        else
        {
            Console.WriteLine("Пацієнта не знайдено.");
        }
    }
}
namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        DoctorManager doctorManager = new DoctorManager();

        doctorManager.Add(
    new Doctor(
        "Олег",
        "Сидоренко",
        "Кардіологія",
        "LIC-001",
        "+380501111111"));

        doctorManager.Add(
            new Doctor(
                "Наталія",
                "Мороз",
                "Неврологія",
                "LIC-002",
                "+380502222222"));

        doctorManager.Add(
            new Doctor(
                "Андрій",
                "Власенко",
                "Педіатрія",
                "LIC-003",
                "+380503333333"));

        DoctorsMenu(doctorManager);
    }

    static void DoctorsMenu(DoctorManager manager)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Лікарі ===");
            Console.WriteLine("1 - Показати всіх");
            Console.WriteLine("2 - Додати");
            Console.WriteLine("3 - Знайти за спеціальністю");
            Console.WriteLine("4 - Видалити");
            Console.WriteLine("5 - Статистика");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.DisplayAll();
                    break;

                case "2":
                    AddDoctor(manager);
                    break;

                case "3":
                    FindDoctor(manager);
                    break;

                case "4":
                    RemoveDoctor(manager);
                    break;

                case "5":
                    manager.DisplayStats();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }

    static void AddDoctor(DoctorManager manager)
    {
        Console.Write("Ім'я: ");
        string firstName = Console.ReadLine();

        Console.Write("Прізвище: ");
        string lastName = Console.ReadLine();

        Console.Write("Спеціальність: ");
        string speciality = Console.ReadLine();

        Console.Write("Номер ліцензії: ");
        string licenseNumber = Console.ReadLine();

        Console.Write("Телефон: ");
        string phone = Console.ReadLine();

        Doctor doctor = new Doctor(
            firstName,
            lastName,
            speciality,
            licenseNumber,
            phone);

        manager.Add(doctor);
    }

    static void FindDoctor(DoctorManager manager)
    {
        Console.Write("Введіть спеціальність: ");
        string speciality = Console.ReadLine();

        Doctor[] doctors =
            manager.FindBySpeciality(speciality);

        Console.WriteLine();

        if (doctors.Length == 0)
        {
            Console.WriteLine("Нічого не знайдено.");
            return;
        }

        Console.WriteLine("Знайдено:");

        for (int i = 0; i < doctors.Length; i++)
        {
            Console.WriteLine(doctors[i]);
        }
    }

    static void RemoveDoctor(DoctorManager manager)
    {
        Console.Write("Введіть ID лікаря: ");
        int id = int.Parse(Console.ReadLine());

        bool removed = manager.Remove(id);

        if (removed)
        {
            Console.WriteLine("Лікаря видалено.");
        }
        else
        {
            Console.WriteLine("Лікаря не знайдено.");
        }
    }
}
namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        Appointment appointment1 =
            new Appointment(
                1,
                1,
                new DateTime(2026, 5, 9, 10, 0, 0));

        Appointment appointment2 =
            new Appointment(
                2,
                2,
                new DateTime(2026, 5, 9, 11, 0, 0),
                45);

        Appointment appointment3 =
            new Appointment(
                3,
                3,
                new DateTime(2026, 5, 10, 9, 0, 0),
                20);

        Console.WriteLine(appointment1);
        Console.WriteLine(appointment2);
        Console.WriteLine(appointment3);

        Console.WriteLine();
        Console.WriteLine("// Після Cancel та Complete:");

        appointment1.Cancel("Пацієнт не зміг прийти");
        appointment2.Complete();

        Console.WriteLine(appointment1);
        Console.WriteLine(appointment2);
    }
}
namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        PatientManager patientManager = new PatientManager();

        patientManager.Add(new Patient(
            "Іван",
            "Петренко",
            new DateTime(1985, 5, 10),
            "A(II)",
            "0501111111"));

        patientManager.Add(new Patient(
            "Олена",
            "Коваль",
            new DateTime(1992, 8, 15),
            "B(III)",
            "0502222222"));

        patientManager.Add(new Patient(
            "Максим",
            "Бойко",
            new DateTime(2010, 3, 12),
            "O(I)",
            "0503333333"));

        DoctorManager doctorManager = new DoctorManager();

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

        doctor2.WorkStartHour = 10;
        doctor2.WorkEndHour = 19;

        doctor3.WorkStartHour = 7;
        doctor3.WorkEndHour = 15;

        doctorManager.Add(doctor1);
        doctorManager.Add(doctor2);
        doctorManager.Add(doctor3);

        AppointmentManager appointmentManager =
            new AppointmentManager(
                patientManager,
                doctorManager);

        appointmentManager.Book(
            1,
            1,
            new DateTime(2027, 5, 9, 10, 0, 0));

        appointmentManager.Book(
            2,
            2,
            new DateTime(2027, 5, 9, 11, 0, 0),
            45);

        appointmentManager.Book(
            3,
            3,
            new DateTime(2027, 5, 10, 9, 0, 0),
            20);

        appointmentManager.Book(
            99,
            1,
            new DateTime(2027, 5, 9, 12, 0, 0));

        Console.WriteLine();
        Console.WriteLine("Майбутні записи:");

        appointmentManager.DisplayList(
            appointmentManager.GetUpcoming());

        Console.WriteLine();

        if (appointmentManager.Cancel(
                1,
                "Пацієнт не зміг прийти"))
        {
            Console.WriteLine("Запис [1] скасовано.");
        }

        Console.WriteLine();
        Console.WriteLine("Записи пацієнта #2:");

        appointmentManager.DisplayList(
            appointmentManager.GetByPatient(2));
    }
}
namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        Clinic clinic = new Clinic();

        clinic.Patients.Add(
            new Patient(
                "Іван",
                "Петренко",
                new DateTime(1985, 5, 10),
                "A(II)",
                "0501111111"));

        clinic.Patients.Add(
            new Patient(
                "Олена",
                "Коваль",
                new DateTime(1992, 8, 15),
                "B(III)",
                "0502222222"));

        clinic.Patients.Add(
            new Patient(
                "Максим",
                "Бойко",
                new DateTime(2010, 3, 12),
                "O(I)",
                "0503333333"));

        clinic.Patients.Add(
            new Patient(
                "Марія",
                "Ткач",
                new DateTime(1999, 11, 20),
                "AB(IV)",
                "0504444444"));

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

        clinic.Doctors.Add(doctor1);
        clinic.Doctors.Add(doctor2);
        clinic.Doctors.Add(doctor3);

        clinic.Appointments.Book(
            1,
            1,
            new DateTime(2027, 5, 9, 10, 0, 0));

        clinic.Appointments.Book(
            2,
            2,
            new DateTime(2027, 5, 9, 11, 0, 0),
            45);

        clinic.Appointments.Book(
            3,
            3,
            new DateTime(2027, 5, 10, 9, 0, 0),
            20);

        clinic.PrintSchedule(
            new DateTime(2027, 5, 9));

        clinic.GenerateReport();
    }
}*/
namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        GrowablePatientManager manager =
            new GrowablePatientManager();

        Console.WriteLine(
            "=== Тест GrowablePatientManager ===");

        Console.WriteLine(
            "Додаємо пацієнтів одного за одним...");

        for (int i = 1; i <= 20; i++)
        {
            Patient patient =
                new Patient(
                    "Тест",
                    "Пацієнт" + i,
                    new DateTime(2000, 1, 1),
                    "A(II)",
                    "0500000000");

            manager.Add(patient);

            Console.WriteLine(
                "  Додано [" +
                patient.Id +
                "]. Розмір: " +
                manager.Count +
                " / " +
                manager.Capacity);
        }

        Console.WriteLine();
        Console.WriteLine("Тест пошуку:");

        Patient? found =
            manager.FindById(10);

        if (found != null)
        {
            Console.WriteLine(
                "  FindById(10) → " +
                found.FullName);
        }

        Patient? missing =
            manager.FindById(99);

        if (missing == null)
        {
            Console.WriteLine(
                "  FindById(99) → не знайдено");
        }

        Console.WriteLine();
        Console.WriteLine("Порівняння:");

        Console.WriteLine(
            "  PatientManager:         100 місць (фіксовано)");

        Console.WriteLine(
            "  GrowablePatientManager:  " +
            manager.Capacity +
            " місця (зросте при потребі)");
    }
}
