using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();

        Type type = obj.GetType();

        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];

            object? value = prop.GetValue(obj);

            RequiredAttribute? required =
                prop.GetCustomAttribute<RequiredAttribute>();

            if (required != null)
            {
                bool invalid = false;

                if (value == null)
                {
                    invalid = true;
                }
                else if (value is string text &&
                         string.IsNullOrWhiteSpace(text))
                {
                    invalid = true;
                }

                if (invalid)
                {
                    result.AddError(
                        prop.Name + ": " + required.ErrorMessage);
                }
            }

            MaxLengthAttribute? maxLength =
                prop.GetCustomAttribute<MaxLengthAttribute>();

            if (maxLength != null &&
                value is string str &&
                str.Length > maxLength.Length)
            {
                string message = string.IsNullOrWhiteSpace(maxLength.ErrorMessage)
                    ? $"Maximum length is {maxLength.Length}."
                    : maxLength.ErrorMessage;

                result.AddError(
                    prop.Name + ": " + message);
            }

            MinValueAttribute? minValue =
                prop.GetCustomAttribute<MinValueAttribute>();

            if (minValue != null && value != null)
            {
                double number = Convert.ToDouble(value);

                if (number < minValue.Min)
                {
                    string message = string.IsNullOrWhiteSpace(minValue.ErrorMessage)
                        ? $"Minimum value is {minValue.Min}."
                        : minValue.ErrorMessage;

                    result.AddError(
                        prop.Name + ": " + message);
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine("Type: " + type.Name);

        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];

            Console.WriteLine(
                $"Property: {prop.Name} ({prop.PropertyType.Name})");

            IEnumerable<Attribute> attributes = prop.GetCustomAttributes();

            foreach (Attribute attribute in attributes)
            {
                Console.WriteLine("  - " + attribute.GetType().Name);
            }
        }
    }
}
