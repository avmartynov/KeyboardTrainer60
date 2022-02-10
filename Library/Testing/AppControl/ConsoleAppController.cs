using System.Diagnostics;
using NLog;
using Twidlle.Library.Utility;

namespace Twidlle.Library.Testing.AppControl;

/// <inheritdoc />
/// <summary> Управляет временем жизни консольного приложения в стиле IDisposable. </summary>
/// <remarks>
/// 1. Для корректной работы этого класса, консольное приложение должно завершаться путём нажатия 
///     на клавишу [Enter] (т.е. зависать на операторе Console.ReadLine()).
/// 2. При создании объекта, приложение запускается, затем (опционально) происходит ожидание его полной готовности к работе.
/// 3. При удалении объекта имитируется нажатие на [Enter], в результате чего приложение самостоятельно завершается.
/// </remarks>
public sealed class ConsoleAppController : IDisposable
{
    private bool _disposed;
    private readonly Process? _process;
    private readonly string? _quitLine;

    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    /// <summary> Инициирует старт приложения и дожидается, 
    /// когда приложение перейдёт в состояние ожидания.</summary>
    public ConsoleAppController(ConsoleAppStartInfo appInfo)
    {
        _logger.Trace($"Start application: {appInfo}. ");

        var exePath = PathExtensions.GetRootedPath(appInfo.ExePath);

        var waitLine = appInfo.WaitLine ?? "Press ";

        var si = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = appInfo.Arguments ?? "",
            WorkingDirectory = appInfo.WorkingDirectory ?? "",

            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        _process = Process.Start(si);

        if (_process == null)
            throw new InvalidOperationException($"Can't start application '{exePath}'.");

        if (appInfo.NoWait)
            return;

        var allLines = Environment.NewLine;
        while (true)
        {
            var line = _process.StandardOutput.ReadLine();
            if (line == null)
            {
                _logger.Error($"Аpplication '{exePath}' output: {allLines}");
                throw new InvalidOperationException($"Application '{exePath}' unexpected finished.");
            }

            allLines += Environment.NewLine + line;
            if (!line.Contains(waitLine))
                continue;

            _logger.Trace($"Аpplication '{exePath}' output: {allLines}");
            _logger.Info($"Application '{exePath}' started");
            break;
        }
        _quitLine = appInfo.QuitLine;
    }


    /// <inheritdoc />
    /// <summary> Завершает приложение эмитируя нажатие клавиши [Enter]. </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        if (_process is null)
            return;

        _process.StandardInput.WriteLine(string.IsNullOrEmpty(_quitLine) ? string.Empty : _quitLine);

        if (_quitLine == "KillJustNow")
            _process.Kill();

        if (!_process.WaitForExit(10000))
        {
            _logger.Error("Can't stop application. Killing it...");
            _process.Kill();
        }
        _disposed = true;
        _logger.Info($"Application '{_process.StartInfo.FileName}' stopped.");
    }
}
