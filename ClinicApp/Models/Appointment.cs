using System;
using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

public class Appointment : IPayable, ICancellable
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

public bool IsCancelled
{
    get
    {
        return Status ==
               AppointmentStatus.Cancelled;
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

public string CancellationReason
{
    get
    {
        if (IsCancelled)
        {
            return Notes;
        }

        return "";
    }
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

    public virtual decimal GetCost()
    {
        return DurationMinutes * 10m * DiscountMultiplier;
    }

    public virtual string GetDescription()
    {
        return "Прийом";
    }

    public int GetPriority()
    {
        return 3;
    }

    public void MarkPaid()
    {
        if (IsCancelled)
        {
            return;
        }

        _isPaid = true;
    }

    public override string ToString()
    {
        string result = string.Format(
            "[{0}] {1} | Пацієнт #{2} → Лікар #{3} | {4:dd.MM.yyyy HH:mm} | {5} хв | {6:F2} грн | {7}",
            Id,
            GetDescription(),
            PatientId,
            DoctorId,
            ScheduledAt,
            DurationMinutes,
            GetCost(),
            Status);

        if (Notes.Length > 0)
        {
            result += " | " + Notes;
        }

        return result;
    }
    // Варіант А: знижка реалізована через множник у базовому класі Appointment. Усі підкласи автоматично враховують її через base.GetCost(),тому не потрібно створювати нові VIP-підкласи.
    public decimal DiscountMultiplier { get; set; } = 1m;
}
