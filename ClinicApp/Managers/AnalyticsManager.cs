using System.Collections.Generic;
using ClinicApp.Models;
using System.Linq;

namespace ClinicApp.Managers
{
    public class AnalyticsManager
    {
        private readonly AppointmentManager _appointments;
        private readonly DoctorManager _doctors;
        private readonly PatientManager _patients;

        public AnalyticsManager(
            AppointmentManager appointments,
            DoctorManager doctors,
            PatientManager patients)
        {
            _appointments = appointments;
            _doctors = doctors;
            _patients = patients;
        }

        public IEnumerable<DoctorStats> ComputeDoctorStats()
        {
            Doctor[] doctors = _doctors.GetAll();
            Appointment[] appointments = _appointments.GetAll();

            return doctors.Select(d =>
            {
                Appointment[] doctorAppointments = appointments
                    .Where(a => a.DoctorId == d.Id)
                    .ToArray();

                return new DoctorStats(
                    d.Id,
                    d.FullName,
                    doctorAppointments.Count(),
                    doctorAppointments.Sum(a => a.GetCost()),
                    doctorAppointments.Any()
                        ? doctorAppointments.Max(a => a.ScheduledAt)
                        : DateTime.MinValue);
            });
        }

        public IEnumerable<PatientStats> ComputePatientStats()
        {
            Patient[] patients = _patients.GetAll();
            Appointment[] appointments = _appointments.GetAll();

            return patients.Select(p =>
            {
                Appointment[] patientAppointments = appointments
                    .Where(a => a.PatientId == p.Id)
                    .ToArray();

                return new PatientStats(
                    p.Id,
                    p.FullName,
                    patientAppointments.Count(),
                    patientAppointments.Sum(a => a.GetCost()),
                    patientAppointments.Any()
                        ? patientAppointments.Max(a => a.ScheduledAt)
                        : DateTime.MinValue);
            });
        }
    }
}
