using ClinicApp.Models;
using ClinicApp.Managers;
using ClinicApp.Utils;
using ClinicApp.Events;
namespace ClinicApp;

public class Clinic
{
    public string Name { get; set; }
    public PatientManager Patients { get; }

    public DoctorManager Doctors { get; }

    public AppointmentManager Appointments { get; }

    public MedicalRecordManager MedicalRecords { get; }

    public BillingManager Billing
    {
        get;
    }

    public AnalyticsManager Analytics { get; }

    public WaitingQueue<Patient> WaitingRoom { get; }

    public TreatmentPlanManager TreatmentPlans { get; }

    public ClinicLogger Logger { get; }

    public ClinicExporter Exporter { get; }

    public SessionManager Session { get; }

    private void SubscribeEvents()
    {
        Appointments.AppointmentBooked += Logger.OnAppointmentBooked;

        Appointments.AppointmentCancelled += Logger.OnAppointmentCancelled;

        Appointments.AppointmentCompleted += Logger.OnAppointmentCompleted;

        Appointments.UrgentAppointmentBooked += Logger.OnUrgentAppointmentBooked;

        Patients.PatientAdded += Logger.OnPatientAdded;

        Billing.PaymentReceived += Logger.OnPaymentReceived;

        TreatmentPlans.PlanCompleted += Logger.OnPlanCompleted;

        Patients.PatientAdded += Passport.OnPatientAdded;

        Appointments.AppointmentCompleted +=
            Passport.OnAppointmentCompleted;

        TreatmentPlans.PlanCompleted +=
            Passport.OnPlanCompleted;

        Patients.PatientAdded +=
    Tracker.OnPatientAdded;

        Appointments.AppointmentBooked +=
            Tracker.OnAppointmentBooked;

        Appointments.UrgentAppointmentBooked +=
            Tracker.OnUrgentAppointmentBooked;

        Appointments.AppointmentCancelled +=
            Tracker.OnAppointmentCancelled;

        Appointments.AppointmentCompleted +=
            Tracker.OnAppointmentCompleted;

        Billing.PaymentReceived +=
            Tracker.OnPaymentReceived;

        TreatmentPlans.PlanCompleted +=
            Tracker.OnPlanCompleted;
    }

    public PatientPassportWriter Passport { get; }

    public SessionEventTracker Tracker { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments =
            new AppointmentManager(
                Patients,
                Doctors);
        MedicalRecords = new MedicalRecordManager();

        Billing =
            new BillingManager(
                Appointments);

        WaitingRoom = new WaitingQueue<Patient>();

        Analytics = new AnalyticsManager(Appointments, Doctors, Patients);

        TreatmentPlans = new TreatmentPlanManager();

        Logger = new ClinicLogger();

        Exporter = new ClinicExporter(this);

        Session = new SessionManager();

        Passport = new PatientPassportWriter(this);

        Tracker = new SessionEventTracker(this);

        SubscribeEvents();
    }

    public void PrintSchedule(DateTime date)
    {
        Console.WriteLine();
        Console.WriteLine(
            "=== Розклад на " +
            date.ToString("dd.MM.yyyy") +
            " ===");

        Appointment[] appointments =
            Appointments.GetByDate(date);

        Appointments.DisplayList(
            appointments);
    }

    public void GenerateReport()
    {
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║  Звіт — Медична Клініка");
        Console.WriteLine("╠══════════════════════════════════════════════╣");

        Console.WriteLine(
            "║  Пацієнтів:          " +
            Patients.Count);

        Console.WriteLine(
            "║  Лікарів:            " +
            Doctors.Count);

        Appointment[] upcoming =
            Appointments.GetUpcoming();

        Console.WriteLine(
            "║  Майбутніх записів:  " +
            upcoming.Length);

        Console.WriteLine("╠══════════════════════════════════════════════╣");
        Console.WriteLine("║  Навантаження лікарів (майбутні записи):");

        Doctor[] doctors =
            Doctors.GetAll();

        for (int i = 0; i < doctors.Length; i++)
        {
            int count = 0;

            for (int j = 0; j < upcoming.Length; j++)
            {
                if (upcoming[j].DoctorId ==
                    doctors[i].Id)
                {
                    count++;
                }
            }

            Console.WriteLine(
                "║    " +
                doctors[i].FullName +
                " (" +
                doctors[i].Speciality +
                "): " +
                count +
                " записів");
        }

        Console.WriteLine("╚══════════════════════════════════════════════╝");
    }
}

