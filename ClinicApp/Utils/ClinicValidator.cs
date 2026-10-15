using System.Text.RegularExpressions;
namespace ClinicApp.Utils;

public static class ClinicValidator
{
    public static void ValidateName(
        string value,
        string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new ArgumentException(fieldName);
        }
    }

    public static void ValidatePhone(
    string value,
    string fieldName)
    {
        if (value == null ||
            !_phoneRegex.IsMatch(value))
        {
            throw new ArgumentException(fieldName);
        }
    }

    public static void ValidateDate(
        DateTime value,
        string fieldName)
    {
        if (value > DateTime.Today || value.Year < 1900)
        {
            throw new ArgumentOutOfRangeException(fieldName);
        }
    }

    public static void ValidateLicenseNumber(
    string value,
    string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(fieldName);
        }
    }

    public static void ValidatePositive(
        int value,
        string fieldName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(fieldName);
        }
    }

    public static void ValidateHour(
        int value,
        string fieldName,
        int min,
        int max)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException(fieldName);
        }
    }
    public static void ValidateEmail(
    string value,
    string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (!_emailRegex.IsMatch(value))
        {
            throw new ArgumentException(fieldName);
        }
    }
    private static readonly Regex _phoneRegex =
    new Regex(@"^[0-9]{10}$");

    private static readonly Regex _emailRegex =
        new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
}
