using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointments;
    public BillingManager(
        AppointmentManager appointments)
    {
        _appointments = appointments;
    }

    private IPayable[] GetUnpaid(
        Appointment[] source)
    {
        int matches = 0;

        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].IsPaid &&
                !source[i].IsCancelled)
            {
                matches++;
            }
        }

        IPayable[] result =
            new IPayable[matches];

        int index = 0;

        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].IsPaid &&
                !source[i].IsCancelled)
            {
                result[index] = source[i];
                index++;
            }
        }

        return result;
    }

    public IPayable[] GetAllUnpaid()
    {
        return GetUnpaid(
            _appointments.GetAll());
    }

    public IPayable[] GetUnpaidByPatient(
        int patientId)
    {
        return GetUnpaid(
            _appointments.GetByPatient(
                patientId));
    }

    public decimal GetTotalDebt()
    {
        IPayable[] items =
            GetAllUnpaid();

        decimal total = 0m;

        for (int i = 0; i < items.Length; i++)
        {
            total += items[i].GetCost();
        }

        return total;
    }

    public decimal GetPatientDebt(
        int patientId)
    {
        IPayable[] items =
            GetUnpaidByPatient(
                patientId);

        decimal total = 0m;

        for (int i = 0; i < items.Length; i++)
        {
            total += items[i].GetCost();
        }

        return total;
    }

    public bool PayAppointment(
        int appointmentId)
    {
        Appointment[] items =
            _appointments.GetAll();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Id ==
                appointmentId)
            {
                if (items[i].IsPaid)
                {
                    return false;
                }

                if (items[i].IsCancelled)
                {
                    return false;
                }

                items[i].MarkPaid();

                return true;
            }
        }

        return false;
    }

    public void DisplayUnpaid(
        IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine(
                "Немає неоплачених записів.");

            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] is Appointment a)
            {
                Console.WriteLine(
                    a +
                    " | Сума: " +
                    a.GetCost().ToString("F2") +
                    " грн");
            }
            else
            {
                Console.WriteLine(
                    (i + 1) +
                    " | Сума: " +
                    items[i]
                        .GetCost()
                        .ToString("F2") +
                    " грн");
            }
        }
    }
}



