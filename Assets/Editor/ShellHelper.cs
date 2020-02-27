using System;
using System.Diagnostics;

public class ShellHelper
{
    private static ShellHelper _instance;

    public static ShellHelper Instance
    {
        get { return _instance ?? (_instance = new ShellHelper()); }
        set { _instance = value; }
    }

    private Process _process;
    private ShellHelper()
    {
       _process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
        };

        _process.Start();
    }

    public string Bash(string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");
     
        _process.StartInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{escapedArgs}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardInput = true
        };
        _process.Start();
        
        string result = _process.StandardOutput.ReadToEnd();
        _process.WaitForExit();

        return result.Trim();
    }
}