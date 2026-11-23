using System;
using System.IO;
using System.Text;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        _logPath = logPath;
    }

    public void LogInfo(string message)
    {
        Write("INFO ", message);
    }

    public void LogWarning(string message)
    {
        Write("WARN ", message);
    }

    public void LogError(string message)
    {
        Write("ERROR", message);
    }

    private void Write(string level, string message)
    {
        string line =
            "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " +
            "[" + level + "] " +
            message;

        File.AppendAllText(
            _logPath,
            line + Environment.NewLine,
            Encoding.UTF8);
    }

    public string[] GetLastLines(int n)
    {
        if (!File.Exists(_logPath))
        {
            return new string[0];
        }

        string[] lines = File.ReadAllLines(_logPath, Encoding.UTF8);

        int start = lines.Length - n;

        if (start < 0)
        {
            start = 0;
        }

        string[] result = new string[lines.Length - start];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = lines[start + i];
        }

        return result;
    }

    public void Clear()
    {
        if (File.Exists(_logPath))
        {
            File.WriteAllText(_logPath, string.Empty);
        }
    }

    public bool Exists()
    {
        return File.Exists(_logPath);
    }
}
