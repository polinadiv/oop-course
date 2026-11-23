using System;
using System.IO;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Interfaces;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private readonly Clinic _clinic;
    private readonly string _baseDir;

    public ClinicExporter(
        Clinic clinic,
        string baseDir = "reports")
    {
        _clinic = clinic;
        _baseDir = baseDir;
    }

    private string CreateReportDirectory()
    {
        string directory =
            Path.Combine(
                _baseDir,
                DateTime.Now.ToString("yyyy-MM-dd"));

        Directory.CreateDirectory(directory);

        return directory;
    }

    public string ExportPatients()
    {
        string directory = CreateReportDirectory();

        string filePath =
            Path.Combine(directory, "patients.txt");

        Patient[] patients =
            _clinic.Patients.GetAll();

        using StreamWriter writer =
            new StreamWriter(filePath, false, Encoding.UTF8);

        writer.WriteLine("=== Пацієнти - звіт ===");
        writer.WriteLine(
            "Згенеровано: " +
            DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

        writer.WriteLine("--------------------------------");

        for (int i = 0; i < patients.Length; i++)
        {
            writer.WriteLine(
                $"{i + 1,3}. {patients[i]}");
        }

        writer.WriteLine("--------------------------------");
        writer.WriteLine("Всього: " + patients.Length);

        return filePath;
    }

    public string ExportAppointments()
    {
        string directory = CreateReportDirectory();

        string filePath =
            Path.Combine(directory, "appointments.txt");

        Appointment[] appointments =
            _clinic.Appointments.GetAll();

        using StreamWriter writer =
            new StreamWriter(filePath, false, Encoding.UTF8);

        writer.WriteLine("=== Записи - звіт ===");
        writer.WriteLine(
            "Згенеровано: " +
            DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

        writer.WriteLine("--------------------------------");

        for (int i = 0; i < appointments.Length; i++)
        {
            writer.WriteLine(
                $"{i + 1,3}. {appointments[i]}");
        }

        writer.WriteLine("--------------------------------");
        writer.WriteLine("Всього: " + appointments.Length);

        return filePath;
    }

    public string ExportBilling()
    {
        string directory = CreateReportDirectory();

        string filePath =
            Path.Combine(directory, "billing.txt");

        IPayable[] unpaid =
            _clinic.Billing.GetAllUnpaid();

        using StreamWriter writer =
            new StreamWriter(filePath, false, Encoding.UTF8);

        writer.WriteLine("=== Борги - звіт ===");
        writer.WriteLine(
            "Згенеровано: " +
            DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

        writer.WriteLine("--------------------------------");

        for (int i = 0; i < unpaid.Length; i++)
        {
            writer.WriteLine(
                $"{i + 1,3}. {unpaid[i].GetCost():F2} грн");
        }

        writer.WriteLine("--------------------------------");

        writer.WriteLine(
            "Загальний борг: " +
            _clinic.Billing.GetTotalDebt().ToString("F2") +
            " грн");

        return filePath;
    }

    public string ExportTreatmentPlans()
    {
        string directory = CreateReportDirectory();

        string filePath =
            Path.Combine(directory, "treatment_plans.txt");

        TreatmentPlan[] plans =
            _clinic.TreatmentPlans.GetAll();

        using StreamWriter writer =
            new StreamWriter(filePath, false, Encoding.UTF8);

        writer.WriteLine("=== Плани лікування - звіт ===");
        writer.WriteLine(
            "Згенеровано: " +
            DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

        writer.WriteLine("--------------------------------");
        for (int i = 0; i < plans.Length; i++)
        {
            writer.WriteLine(
                $"{i + 1,3}. {plans[i]}");
        }

        writer.WriteLine("--------------------------------");
        writer.WriteLine("Всього: " + plans.Length);

        return filePath;
    }

    public string[] ExportAll()
    {
        return new string[]
        {
            ExportPatients(),
            ExportAppointments(),
            ExportBilling(),
            ExportTreatmentPlans()
        };
    }
}