using System;
using System.IO;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private readonly Clinic _clinic;
    private readonly string _folder;

    public PatientPassportWriter(
        Clinic clinic,
        string folder = "patients")
    {
        _clinic = clinic;
        _folder = folder;

        Directory.CreateDirectory(_folder);
    }

    public void OnPatientAdded(
        object? sender,
        PatientEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnAppointmentCompleted(
        object? sender,
        AppointmentEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnPlanCompleted(
        object? sender,
        TreatmentPlanEventArgs e)
    {
        WritePassport(e.PatientId);
    }



    private void WritePassport(int patientId)
    {
        Patient? patient =
            _clinic.Patients.FindById(patientId);

        if (patient == null)
        {
            return;
        }

        string filePath =
            Path.Combine(
                _folder,
                "passport_" + patientId + ".txt");

        using StreamWriter writer =
            new StreamWriter(filePath, false);

        writer.WriteLine(
            "=== Паспорт пацієнта #" +
            patient.Id +
            " ===");

        writer.WriteLine(
            "Згенеровано: " +
            DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

        writer.WriteLine();

        writer.WriteLine(
            "Ім'я: " +
            patient.FullName);

        writer.WriteLine(
    "Дата народження: " +
    patient.DateOfBirth.ToString("dd.MM.yyyy") +
    " (" +
    patient.Age +
    " років)");

        writer.WriteLine(
            "Група крові: " +
            patient.BloodType);

        writer.WriteLine(
            "Телефон: " +
            patient.Phone);

        writer.WriteLine();

        MedicalRecord[] records =
    _clinic.MedicalRecords.GetByPatient(patientId);


        writer.WriteLine("-- Діагнози --");

        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is Diagnosis diagnosis)
            {
                writer.WriteLine(
                    diagnosis.GetSummary());
            }
        }

        writer.WriteLine();

        writer.WriteLine("-- Аналізи --");

        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is LabResult labResult)
            {
                writer.WriteLine(
                    labResult.GetSummary());
            }
        }

        writer.WriteLine();

        writer.WriteLine("-- Рецепти --");

        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is Prescription prescription)
            {
                writer.WriteLine(
                    prescription.GetSummary());
            }
        }

        writer.WriteLine();

        writer.WriteLine();

        writer.WriteLine("-- Записи на прийом --");

        Appointment[] appointments =
            _clinic.Appointments.GetByPatient(patientId);

        for (int i = 0; i < appointments.Length; i++)
        {
            writer.WriteLine(
                appointments[i].ToString());
        }

        writer.WriteLine();

        writer.WriteLine("-- Плани лікування --");

        TreatmentPlan[] plans =
            _clinic.TreatmentPlans.GetByPatient(patientId);


        for (int i = 0; i < plans.Length; i++)
        {
            writer.WriteLine(
                plans[i].ToString());
        }

        writer.WriteLine();

        writer.WriteLine(
            "Заборгованість: " +
            _clinic.Billing
                .GetPatientDebt(patientId)
                .ToString("F2") +
            " грн");
    }
}
