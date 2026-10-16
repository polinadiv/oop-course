using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class MedicalRecordManager
    {
        private const int MaxRecords = 1000;

        private MedicalRecord[] _records;
        private int _count;

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public MedicalRecordManager()
        {
            _records = new MedicalRecord[MaxRecords];
            _count = 0;
        }

        public void Add(MedicalRecord record)
        {
            if (_count >= MaxRecords)
            {
                Console.WriteLine("Досягнуто ліміт записів.");
                return;
            }

            _records[_count] = record;
            _count++;

            Console.WriteLine(
                "Запис [" +
                record.Id +
                "] " +
                record.GetRecordType() +
                " додано.");
        }

        public MedicalRecord? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].Id == id)
                {
                    return _records[i];
                }
            }

            return null;
        }

        public MedicalRecord[] GetByPatient(int patientId)
        {
            int found = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId)
                {
                    found++;
                }
            }

            MedicalRecord[] result = new MedicalRecord[found];

            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId)
                {
                    result[index] = _records[i];
                    index++;
                }
            }

            return result;
        }

        public MedicalRecord[] GetByDoctor(int doctorId)
        {
            int found = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i].DoctorId == doctorId)
                {
                    found++;
                }
            }

            MedicalRecord[] result = new MedicalRecord[found];

            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i].DoctorId == doctorId)
                {
                    result[index] = _records[i];
                    index++;
                }
            }

            return result;
        }

        public void DisplayAll()
        {
            if (_count == 0)
            {
                Console.WriteLine("Записи відсутні.");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_records[i]);
            }
        }

        public void DisplayList(MedicalRecord[] records)
        {
            if (records.Length == 0)
            {
                Console.WriteLine("Записи відсутні.");
                return;
            }

            for (int i = 0; i < records.Length; i++)
            {
                Console.WriteLine(records[i]);
            }
        }

        public MedicalRecord? this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    return null;
                }

                return _records[index];
            }
        }
        public Diagnosis[] GetDiagnoses(int patientId)
        {
            int count = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Diagnosis diagnosis &&
                    diagnosis.PatientId == patientId)
                {
                    count++;
                }
            }

            Diagnosis[] result = new Diagnosis[count];

            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Diagnosis diagnosis &&
                    diagnosis.PatientId == patientId)
                {
                    result[index] = diagnosis;
                    index++;
                }
            }

            return result;
        }
        public LabResult[] GetLabResults(int patientId)
        {
            int count = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is LabResult labResult &&
                    labResult.PatientId == patientId)
                {
                    count++;
                }
            }

            LabResult[] result = new LabResult[count];

            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is LabResult labResult &&
                    labResult.PatientId == patientId)
                {
                    result[index] = labResult;
                    index++;
                }
            }

            return result;
        }
        public Prescription[] GetPrescriptions(int patientId)
        {
            int count = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Prescription prescription &&
                    prescription.PatientId == patientId)
                {
                    count++;
                }
            }

            Prescription[] result = new Prescription[count];

            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Prescription prescription &&
                    prescription.PatientId == patientId)
                {
                    result[index] = prescription;
                    index++;
                }
            }

            return result;
        }
        public Diagnosis[] GetChronicDiagnoses(int patientId)
        {
            int count = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Diagnosis diagnosis &&
                    diagnosis.PatientId == patientId &&
                    diagnosis.IsChronic)
                {
                    count++;
                }
            }

            Diagnosis[] result = new Diagnosis[count];

            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Diagnosis diagnosis &&
                    diagnosis.PatientId == patientId &&
                    diagnosis.IsChronic)
                {
                    result[index] = diagnosis;
                    index++;
                }
            }

            return result;
        }
        public Prescription[] GetActivePrescriptions(int patientId)
        {
            int count = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Prescription prescription &&
                    prescription.PatientId == patientId &&
                    prescription.IsActive())
                {
                    count++;
                }
            }

            Prescription[] result = new Prescription[count];

            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i] is Prescription prescription &&
                    prescription.PatientId == patientId &&
                    prescription.IsActive())
                {
                    result[index] = prescription;
                    index++;
                }
            }

            return result;
        }
        public void DisplayPatientSummary(int patientId)
        {
            Diagnosis[] diagnoses = GetDiagnoses(patientId);
            LabResult[] labResults = GetLabResults(patientId);
            Prescription[] prescriptions = GetPrescriptions(patientId);

            int total =
                diagnoses.Length +
                labResults.Length +
                prescriptions.Length;

            if (total == 0)
            {
                Console.WriteLine("Записи відсутні.");
                return;
            }

            Console.WriteLine("=== Медична картка пацієнта #" +
                              patientId +
                              " ===");

            Console.WriteLine(
                "Всього записів: " +
                total +
                " (діагнозів: " +
                diagnoses.Length +
                ", аналізів: " +
                labResults.Length +
                ", рецептів: " +
                prescriptions.Length +
                ")");

            Diagnosis[] chronicDiagnoses =
                GetChronicDiagnoses(patientId);

            if (chronicDiagnoses.Length > 0)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Хронічні діагнози (" +
                    chronicDiagnoses.Length +
                    "):");

                for (int i = 0; i < chronicDiagnoses.Length; i++)
                {
                    Console.WriteLine(chronicDiagnoses[i]);
                }
            }

            Prescription[] activePrescriptions =
                GetActivePrescriptions(patientId);

            if (activePrescriptions.Length > 0)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Активні рецепти (" +
                    activePrescriptions.Length +
                    "):");

                for (int i = 0; i < activePrescriptions.Length; i++)
                {
                    Console.WriteLine(
                        activePrescriptions[i] +
                        " | до " +
                        activePrescriptions[i]
                            .ExpiresAt
                            .ToString("dd.MM.yyyy"));
                }
            }
        }
    }
}
