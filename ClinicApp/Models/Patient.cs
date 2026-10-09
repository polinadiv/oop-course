using ClinicApp.Enums;
using ClinicApp.Utils;
namespace ClinicApp.Models;

public class Patient
{
    private string _firstName;
    private string _lastName;
    private DateTime _dateOfBirth;
    private string _phone;
    private static int _nextId = 1;

    public int Id { get; }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException(nameof(FirstName));
            }

            _firstName = value;
        }
    }
    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException(nameof(LastName));
            }

            _lastName = value;
        }
    }
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (value > DateTime.Today)
            {
                throw new ArgumentOutOfRangeException(nameof(DateOfBirth));
            }

            if (value.Year < 1900)
            {
                throw new ArgumentOutOfRangeException(nameof(DateOfBirth));
            }

            _dateOfBirth = value;
        }
    }
    public string Phone
    {
        get => _phone;
        set
        {
            if (value == null || value.Length != 10)
            {
                throw new ArgumentException(nameof(Phone));
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                {
                    throw new ArgumentException(nameof(Phone));
                }
            }

            _phone = value;
        }
    }

    public BloodType BloodType { get; set; }

    public string Email { get; set; }

    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

    public int Age
    {
        get
        {
            int age = DateTime.Today.Year - DateOfBirth.Year;

            if (DateTime.Today < DateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }

    public bool IsAdult
    {
        get
        {
            return Age >= 18;
        }
    }

    public Patient()
        : this(
            "Невідомий",
            "Пацієнт",
            new DateTime(2000, 1, 1),
            BloodType.Unknown,
            "0000000000")
    {
    }

    public Patient(string firstName, string lastName)
        : this(
            firstName,
            lastName,
            new DateTime(2000, 1, 1),
            BloodType.Unknown,
            "0000000000")
    {
    }

    public Patient(
        string firstName,
        string lastName,
        DateTime dob,
        BloodType bloodType,
        string phone)
    {
        Id = _nextId++;

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        BloodType = bloodType;
        Phone = phone;
        Email = "";
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
        {
            return "дитина";
        }

        if (Age < 60)
        {
            return "дорослий";
        }

        return "літній";
    }

    public override string ToString()
    {
        return "[" + Id + "] "
               + FullName
               + " | Вік: "
               + ClinicFormatter.FormatAge(Age)
               + " (" + GetAgeCategory() + ")"
               + " | Кров: "
               + ClinicFormatter.FormatBloodType(
                   BloodType)
               + " | Тел: "
               + ClinicFormatter.FormatPhone(
                   Phone);
    }
}
