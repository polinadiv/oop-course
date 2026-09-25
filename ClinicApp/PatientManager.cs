namespace ClinicApp;

public class PatientManager
{
    private const int MaxPatients = 100;

    private Patient[] _patients;
    private int _count;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public PatientManager()
    {
        _patients = new Patient[MaxPatients];
        _count = 0;
    }

    public void Add(Patient patient)
    {
        if (_count >= MaxPatients)
        {
            Console.WriteLine("Досягнуто ліміт пацієнтів.");
            return;
        }

        _patients[_count] = patient;
        _count++;

        Console.WriteLine("Пацієнта " +
                          patient.FullName +
                          " додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }

        return null;
    }

    public Patient[] FindByName(string name)
    {
        string searchName = name.ToLower();

        int matches = 0;

        for (int i = 0; i < _count; i++)
        {

            if (_patients[i].FullName
                .ToLower()
                .Contains(searchName))
            {
                matches++;
            }
        }

        Patient[] result = new Patient[matches];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FullName
                .ToLower()
                .Contains(searchName))
            {
                result[index] = _patients[i];
                index++;
            }
        }

        return result;
    }

    public bool Remove(int id)
    {
        int index = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return false;
        }

        for (int i = index; i < _count - 1; i++)
        {
            _patients[i] = _patients[i + 1];
        }

        _patients[_count - 1] = null;
        _count--;

        return true;
    }

    public void DisplayAll()
    {
        Console.WriteLine();
        Console.WriteLine("=== Пацієнти (" +
                          _count +
                          " / " +
                          MaxPatients +
                          ") ===");

        if (_count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i]);
        }

        Console.WriteLine("------------------------------");
    }

    public void DisplayStats()
    {
        Console.WriteLine();
        Console.WriteLine("=== Статистика пацієнтів ===");

        if (_count == 0)
        {
            Console.WriteLine("Немає даних.");
            return;
        }

        int sumAge = 0;
        int youngestIndex = 0;
        int oldestIndex = 0;
        int adultCount = 0;

        for (int i = 0; i < _count; i++)
        {
            sumAge += _patients[i].Age;

            if (_patients[i].Age <
                _patients[youngestIndex].Age)
            {
                youngestIndex = i;
            }

            if (_patients[i].Age >
                _patients[oldestIndex].Age)
            {
                oldestIndex = i;
            }

            if (_patients[i].IsAdult)
            {
                adultCount++;
            }
        }

        double averageAge =
            (double)sumAge / _count;

        Console.WriteLine("Всього: " + _count);

        Console.WriteLine(
            "Середній вік: " +
            averageAge.ToString("F1") +
            " р.");

        Console.WriteLine(
            "Наймолодший: " +
            _patients[youngestIndex].FullName +
            " (" +
            _patients[youngestIndex].Age +
            " р.)");
        Console.WriteLine(
            "Найстарший: " +
            _patients[oldestIndex].FullName +
            " (" +
            _patients[oldestIndex].Age +
            " р.)");

        Console.WriteLine(
            "Дорослих: " +
            adultCount +
            " з " +
            _count);

        Console.WriteLine("==============================");
    }
}
