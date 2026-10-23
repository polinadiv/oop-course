using System;
using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

public class Appointment : IPayable
{
    private static int _nextId = 1;

    public int Id { get; }

    public int PatientId { get; }

    public int DoctorId { get; }

    public DateTime ScheduledAt { get; set; }

    private int _durationMinutes;

    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            ClinicValidator.ValidatePositive(value, nameof(DurationMinutes));
            _durationMinutes = value;
        }
    }

    private bool _isPaid;

    public AppointmentStatus Status { get; private set; }
    public bool IsPaid
    {
        get
        {
            return _isPaid;
        }
    }

    public string Notes { get; private set; }

    public DateTime EndsAt
    {
        get
        {
            return ScheduledAt.AddMinutes(DurationMinutes);
        }
    }

    public bool IsUpcoming
    {
        get
        {
            return Status == AppointmentStatus.Scheduled
                   && ScheduledAt > DateTime.Now;
        }
    }

    public Appointment(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        int durationMinutes = 30)
    {
        Id = _nextId++;

        PatientId = patientId;
        DoctorId = doctorId;

        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;

        Status = AppointmentStatus.Scheduled;
        Notes = "";
        _isPaid = false;
    }

    public bool Cancel(string reason = "")
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            return false;
        }

        Status = AppointmentStatus.Cancelled;
        Notes = reason;

        return true;
    }

    public bool Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            return false;
        }

        Status = AppointmentStatus.Completed;

        return true;
    }

    public decimal GetCost()
    {
        return DurationMinutes * 10m;
    }

    public void MarkPaid()
    {
        if (Status == AppointmentStatus.Cancelled)
        {
            return;
        }

        _isPaid = true;
    }

    public override string ToString()
    {
        string result =
            "[" + Id + "] " +
            "Пацієнт #" + PatientId +
            " → Лікар #" + DoctorId +
            " | " +
            ScheduledAt.ToString("dd.MM.yyyy HH:mm") +
            "–" +
            EndsAt.ToString("HH:mm") +
            " | " +
            Status;

        if (Notes.Length > 0)
        {
            result += " | " + Notes;
        }

        return result;
    }
}
