using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();

        PropertyInfo[] properties = typeof(T).GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];

            if (prop.GetSetMethod() == null)
            {
                continue;
            }

            while (true)
            {
                try
                {
                    string hint = "";

                    RequiredAttribute? required =
                        prop.GetCustomAttribute<RequiredAttribute>();

                    if (required != null)
                    {
                        hint += "обов'язкове";
                    }

                    MaxLengthAttribute? maxLength =
                        prop.GetCustomAttribute<MaxLengthAttribute>();

                    if (maxLength != null)
                    {
                        if (hint.Length > 0)
                        {
                            hint += ", ";
                        }

                        hint += $"макс. {maxLength.Length}";
                    }

                    MinValueAttribute? minValue =
                        prop.GetCustomAttribute<MinValueAttribute>();

                    if (minValue != null)
                    {
                        if (hint.Length > 0)
                        {
                            hint += ", ";
                        }

                        hint += $"мін. {minValue.Min}";
                    }

                    if (hint.Length > 0)
                    {
                        Console.Write($"{prop.Name} ({hint}): ");
                    }
                    else
                    {
                        Console.Write($"{prop.Name}: ");
                    }

                    string? input = Console.ReadLine();

                    object? converted;

                    if (prop.PropertyType == typeof(string))
                    {
                        converted = input ?? "";
                    }
                    else if (prop.PropertyType.IsEnum)
                    {
                        converted = Enum.Parse(
                            prop.PropertyType,
                            input ?? "",
                            true);
                    }
                    else
                    {
                        converted = Convert.ChangeType(
                            input,
                            prop.PropertyType);
                    }

                    prop.SetValue(obj, converted);
                    break;
                }
                catch
                {
                    Console.WriteLine(
                        "Некоректне значення, спробуйте ще раз.");
                }
            }
        }

        return obj;
    }
}
