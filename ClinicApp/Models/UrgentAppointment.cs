using ClinicApp.Enums;

namespace ClinicApp.Models;

public class UrgentAppointment : Appointment
{
    public string UrgencyNote { get; }

    public UrgentAppointment(
        int patientId,
        int doctorId,
        DateTime appointmentDate,
        int durationMinutes = 30,
        string urgencyNote = "")
        : base(patientId, doctorId, appointmentDate, durationMinutes)
    {
        UrgencyNote = urgencyNote;
    }

    public override decimal GetCost()
    {
        return base.GetCost() * 1.5m;
    }

    public new int GetPriority()
    {
        return 1;
    }

    public sealed override string GetDescription()
    {
        if (string.IsNullOrWhiteSpace(UrgencyNote))
        {
            return "Терміновий прийом";
        }

        return $"Терміновий ({UrgencyNote})";
    }
}
