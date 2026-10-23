using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;
namespace ClinicApp.Models
{
    public class Doctor : ISchedulable
    {
        private string _firstName;
        private string _lastName;
        private string _licenseNumber;
        private string _phone;
        private static int _nextId = 1;

        public int Id { get; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(FirstName));
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(LastName));
                _lastName = value;
            }
        }
        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                ClinicValidator.ValidateLicenseNumber(value, nameof(LicenseNumber));
                _licenseNumber = value;
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                ClinicValidator.ValidatePhone(value, nameof(Phone));
                _phone = value;
            }
        }
        public Speciality Speciality { get; set; }

        public WorkSchedule Schedule { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public int WorkingHoursPerDay
        {
            get
            {
                return Schedule.HoursPerDay;
            }
        }

        public string WorkSchedule
        {
            get
            {
                return Schedule.Display;
            }
        }

        public bool IsAvailableNow
        {
            get
            {
                return Schedule.IsNow;
            }
        }

        public Doctor()
            : this("", "", Speciality.General, "", "")
        {
        }

        public Doctor(
            string firstName,
            string lastName,
            Speciality speciality)
            : this(
                firstName,
                lastName,
                speciality,
                "",
                "")
        {
        }

        public Doctor(
            string firstName,
            string lastName,
            Speciality speciality,
            string licenseNumber,
            string phone)
        {
            Id = _nextId++;

            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
            LicenseNumber = licenseNumber;
            Phone = phone;

            Schedule =
                new WorkSchedule(8, 17);
        }

        public bool CanAcceptAt(int hour)
        {
            return Schedule.Contains(hour);
        }

        public bool CanSchedule(DateTime at)
        {
            return CanAcceptAt(at.Hour);
        }

        public DateTime[] GetAvailableSlots(
            DateTime date,
            int slotCount)
        {
            ClinicValidator.ValidatePositive(
                slotCount,
                nameof(slotCount));

            int count = slotCount;

            if (count > Schedule.HoursPerDay)
            {
                count = Schedule.HoursPerDay;
            }

            DateTime[] result =
                new DateTime[count];

            DateTime start =
                date.Date.AddHours(
                    Schedule.Start);

            for (int i = 0; i < count; i++)
            {
                result[i] =
                    start.AddHours(i);
            }

            return result;
        }

        public override string ToString()
        {
            string status;

            if (IsAvailableNow)
            {
                status = "доступний";
            }
            else
            {
                status = "не в робочий час";
            }

            return "[" + Id + "] " +
                   FullName + " | " +
                   ClinicFormatter.FormatSpeciality(Speciality) + " | " +
                   LicenseNumber + " | " +
                   "Тел: " + ClinicFormatter.FormatPhone(Phone) + " | " +
                   WorkSchedule + " (" +
                   WorkingHoursPerDay + " год.) | " +
                   status;
        }
    }
}
