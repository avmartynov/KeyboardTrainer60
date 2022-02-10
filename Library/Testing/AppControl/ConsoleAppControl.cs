using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using Twidlle.Library.Utility;

namespace Twidlle.Library.Testing.AppControl;

public static class ConsoleAppControl
{
    public static void StartWaitFinish(params string[] paths) => 
        StartWaitFinish(new ConsoleAppStartInfo(Path.Combine(paths)));


    public static void StartWaitFinish(ConsoleAppStartInfo startInfo) => 
        Start(startInfo).Dispose();


    public static IDisposable Start(ConsoleAppStartInfo startInfo) => 
        new ConsoleAppController(startInfo);


    public static void StartWaitFinishConfigured(string configSectionName) => 
        StartConfigured(configSectionName).Dispose();


    
    public static IDisposable StartConfigured(string configSectionName) => 
        new ConsoleAppController(ReadStartInfoConfigSection(configSectionName));


    
    public static string Execute(string appPath, string args, 
         string? input = null,  string? workingDirectory = null)
    {
        var startInfo = new ProcessStartInfo
                        {
                            FileName = appPath,
                            WorkingDirectory = workingDirectory ?? "",
                            Arguments = args,
                            RedirectStandardInput  = true,
                            RedirectStandardOutput = true,
                            UseShellExecute        = false,
                        };
        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException($"Can't start application '{appPath}'.");
        
        process.StandardInput.Write(input);
        process.StandardInput.Close();
        return process.StandardOutput.ReadToEnd();
    }


    /// <summary> Читает настройки из конфигурационной секции типа System.Configuration.NameValueFileSectionHandler. </summary>
    private static ConsoleAppStartInfo ReadStartInfoConfigSection(string configSectionName)
    {
        var config = (NameValueCollection?)ConfigurationManager.GetSection(configSectionName);
        if (config is null)
            throw new InvalidOperationException($"Config section '{configSectionName}' does not exist.");

        var exePath = config["ExePath"];
        if (exePath is null)
            throw new InvalidOperationException("Path to executable file has not been specified.");

        if (!Path.IsPathRooted(exePath))
            exePath = PathExtensions.GetRootedPath(exePath);

        var workingDirectory = config["WorkingDirectory"] ?? PathExtensions.GetAppDirectory();

        if (!Path.IsPathRooted(workingDirectory))
            workingDirectory = PathExtensions.GetRootedPath(workingDirectory);

        var arguments = config["Arguments"]?.Replace("${{WorkingDirectory}}", workingDirectory);

        return new ConsoleAppStartInfo(exePath)
            {
                WorkingDirectory = workingDirectory,
                Arguments = arguments,
                WaitLine = config["WaitLine"],
                QuitLine = config["QuitLine"],
                NoWait = bool.Parse(config["NoWait"] ?? "false")
            };
    }
}
