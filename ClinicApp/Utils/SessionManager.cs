using System.Globalization;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Utils;

public class SessionManager
{
    private readonly string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }

    public void Save(Clinic clinic)
    {
        using StreamWriter writer =
            new StreamWriter(_sessionPath);

        writer.WriteLine("[PATIENTS]");

        Patient[] patients =
            clinic.Patients.GetAll();

        for (int i = 0; i < patients.Length; i++)
        {
            writer.WriteLine(
                patients[i].FirstName + "," +
                patients[i].LastName + "," +
                patients[i].DateOfBirth.ToString("dd.MM.yyyy") + "," +
                patients[i].BloodType + "," +
                patients[i].Phone);
        }

        writer.WriteLine("[END]");
    }

    public int Load(Clinic clinic)
    {
        if (!Exists())
        {
            return 0;
        }

        int loadedCount = 0;
        bool inPatientsSection = false;

        foreach (string line in File.ReadLines(_sessionPath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line == "[PATIENTS]")
            {
                inPatientsSection = true;
                continue;
            }

            if (line == "[END]")
            {
                break;
            }

            if (!inPatientsSection)
            {
                continue;
            }

            try
            {
                string[] parts = line.Split(',');

                if (parts.Length != 5)
                {
                    continue;
                }

                string firstName = parts[0];
                string lastName = parts[1];

                DateTime birthDate =
                    DateTime.ParseExact(
                        parts[2],
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture);

                BloodType bloodType =
                    Enum.Parse<BloodType>(parts[3]);

                string phone = parts[4];

                bool alreadyExists = false;

                Patient[] patients =
                    clinic.Patients.GetAll();

                for (int i = 0; i < patients.Length; i++)
                {
                    if (patients[i].FirstName == firstName &&
                        patients[i].LastName == lastName &&
                        patients[i].DateOfBirth.Date == birthDate.Date)
                    {
                        alreadyExists = true;
                        break;
                    }
                }

                if (alreadyExists)
                {
                    continue;
                }

                Patient patient =
                    new Patient(
                        firstName,
                        lastName,
                        birthDate,
                        bloodType,
                        phone);

                clinic.Patients.Add(patient);

                loadedCount++;
            }
            catch
            {
                
            }
        }

        return loadedCount;
    }
}
