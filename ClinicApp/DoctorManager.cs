namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;

    private Doctor[] _doctors;
    private int _count;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public DoctorManager()
    {
        _doctors = new Doctor[MaxDoctors];
        _count = 0;
    }

    public void Add(Doctor doctor)
    {
        if (_count >= MaxDoctors)
        {
            Console.WriteLine("Досягнуто ліміт лікарів.");
            return;
        }

        _doctors[_count] = doctor;
        _count++;

        Console.WriteLine("Лікаря " +
                          doctor.FullName +
                          " додано.");
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                return _doctors[i];
            }
        }

        return null;
    }

    public Doctor[] FindBySpeciality(string speciality)
    {
        string search = speciality.ToLower();

        int matches = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality
                .ToLower()
                .Contains(search))
            {
                matches++;
            }
        }

        Doctor[] result = new Doctor[matches];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality
                .ToLower()
                .Contains(search))
            {
                result[index] = _doctors[i];
                index++;
            }
        }

        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] result = new Doctor[_count];

        for (int i = 0; i < _count; i++)
        {
            result[i] = _doctors[i];
        }

        return result;
    }

    public bool Remove(int id)
    {
        int index = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
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
            _doctors[i] = _doctors[i + 1];
        }

        _doctors[_count - 1] = null;
        _count--;

        return true;
    }

    public void DisplayAll()
    {
        Console.WriteLine();
        Console.WriteLine("=== Лікарі (" +
                          _count +
                          " / " +
                          MaxDoctors +
                          ") ===");

        if (_count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i]);
        }

        Console.WriteLine("------------------------------");
    }

    public void DisplayStats()
    {
        Console.WriteLine();
        Console.WriteLine("=== Статистика лікарів ===");

        Console.WriteLine("Всього: " + _count);

        int availableCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine("Доступні зараз: " + availableCount);

        Console.WriteLine("По спеціальностях:");

        for (int i = 0; i < _count; i++)
        {
            bool alreadyShown = false;

            for (int j = 0; j < i; j++)
            {
                if (_doctors[i].Speciality ==
                    _doctors[j].Speciality)
                {
                    alreadyShown = true;
                    break;
                }
            }

            if (!alreadyShown)
            {
                int count = 0;

                for (int k = 0; k < _count; k++)
                {
                    if (_doctors[k].Speciality ==
                        _doctors[i].Speciality)
                    {
                        count++;
                    }
                }
                Console.WriteLine(
                    _doctors[i].Speciality +
                    ": " +
                    count);
            }
        }
    }
}
