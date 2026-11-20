using System;
using System.Collections.Generic;
using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans;

    public TreatmentPlanManager()
    {
        _plans = new List<TreatmentPlan>();
    }

    public bool Add(TreatmentPlan plan)
    {
        ValidationResult result = ModelValidator.Validate(plan);

        if (!result.IsValid)
        {
            for (int i = 0; i < result.Errors.Count; i++)
            {
                Console.WriteLine(result.Errors[i]);
            }

            return false;
        }

        _plans.Add(plan);
        return true;
    }

    public TreatmentPlan? GetById(int id)
    {
        for (int i = 0; i < _plans.Count; i++)
        {
            if (_plans[i].Id == id)
            {
                return _plans[i];
            }
        }

        return null;
    }

    public TreatmentPlan[] GetByPatient(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _plans.Count; i++)
        {
            if (_plans[i].PatientId == patientId)
            {
                count++;
            }
        }

        TreatmentPlan[] result = new TreatmentPlan[count];

        int index = 0;

        for (int i = 0; i < _plans.Count; i++)
        {
            if (_plans[i].PatientId == patientId)
            {
                result[index] = _plans[i];
                index++;
            }
        }

        return result;
    }

    public TreatmentPlan[] GetByStatus(TreatmentStatus status)
    {
        int count = 0;

        for (int i = 0; i < _plans.Count; i++)
        {
            if (_plans[i].Status == status)
            {
                count++;
            }
        }

        TreatmentPlan[] result = new TreatmentPlan[count];

        int index = 0;

        for (int i = 0; i < _plans.Count; i++)
        {
            if (_plans[i].Status == status)
            {
                result[index] = _plans[i];
                index++;
            }
        }

        return result;
    }

    public TreatmentPlan[] GetAll()
    {
        return _plans.ToArray();
    }
}