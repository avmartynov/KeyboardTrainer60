using System.Diagnostics;
using System.Runtime.InteropServices;
using NLog;
using Twidlle.Library.Utility;


namespace Twidlle.Library.Testing.AppControl;

/// <summary> Управляет временем жизни оконного приложения в стиле IDisposable. </summary>
/// 
public sealed class WinAppController : IDisposable
{
    private readonly WinAppStartInfo _appInfo;

    private bool _disposed;
    private Process? _process;

    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    /// <inheritdoc />
    /// <summary> Инициирует старт приложения и дожидается, 
    /// когда приложение перейдёт в состояние ожидания.</summary>
    public WinAppController(string exeFilePath)
        : this(new WinAppStartInfo(exeFilePath))
    {
    }

    /// <summary> Инициирует старт приложения и дожидается, 
    /// когда приложение перейдёт в состояние ожидания.</summary>
    public WinAppController(WinAppStartInfo appInfo)
    {
        ThrowIfNull(appInfo);

        _appInfo = appInfo;
    }

    /// <summary> Инициирует старт приложения и дожидается, 
    /// когда приложение перейдёт в состояние ожидания.</summary>
    public WinAppController StartWaitForInputIdle()
    {
        _logger.Trace($"Start application: {_appInfo}. ");

        if (string.IsNullOrWhiteSpace(_appInfo.ExeFilePath))
        {
            throw new InvalidOperationException("Path to executable file has not been specified.");
        }

        var exePath = _appInfo.ExeFilePath;

        if (!Path.IsPathRooted(exePath))
        {
            exePath = PathExtensions.GetRootedPath(exePath);
        }

        _process = Process.Start(new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = _appInfo.Arguments ?? "",
            WorkingDirectory = _appInfo.WorkingDirectory ?? ""
        });

        if (_process == null)
        {
            throw new InvalidOperationException("Can't start application.");
        }

        var idle = _process.WaitForInputIdle(10000);

        if (!idle)
        {
            throw new InvalidOperationException("Application is busy during more than 10 seconds.");
        }

        Thread.Sleep(500);

        if (_process.HasExited)
        {
            throw new InvalidOperationException("Application has exited after start and idle state.");
        }
        var startDate = DateTime.Now;

        while (!_process.Responding)
        {
            Thread.Sleep(100);
            if ((DateTime.Now - startDate).TotalSeconds > 10000)
            {
                throw new InvalidOperationException("Application does not respond during more than 10 seconds.");
            }
        }

        if (_process.MainWindowHandle != GetFocusWindow())
        {
            throw new InvalidOperationException("Application main window has not focus.");
        }
        _logger.Info("Application successfully started");
        return this;
    }


    /// <inheritdoc />
    /// <summary> Завершает приложение закрывая главное окно приложения. </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        if (_process is null)
            return;

        if (!_process.CloseMainWindow())
        {
            _logger.Error("Can't close main window of application. Killing application...");
            _process.Kill();
        }

        if (!_process.WaitForExit(1000))
        {
            _logger.Error("Can't stop application. Killing application...");
            _process.Kill();
        }
        _disposed = true;
        _logger.Info($"Application stopped.");
        _process = null;
    }

    /// <summary> Находит окно имеющее фокус ввода. </summary>
    private static IntPtr GetFocusWindow()
    {
        var foregroundWindowHandle = GetForegroundWindow();
        var threadId = GetWindowThreadProcessId(foregroundWindowHandle, out _);
        var guiThreadInfo = new GUITHREADINFO();
        guiThreadInfo.cbSize = Marshal.SizeOf(guiThreadInfo);
        GetGUIThreadInfo((uint)threadId, ref guiThreadInfo);

        return guiThreadInfo.hwndFocus;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

    [DllImport("user32.dll")]
    static extern bool GetGUIThreadInfo(uint idThread, ref GUITHREADINFO guiThreadInfo);

    [Flags]
    public enum GuiThreadInfoFlags
    {
        GUI_CARETBLINKING = 0x00000001,
        GUI_INMENUMODE = 0x00000004,
        GUI_INMOVESIZE = 0x00000002,
        GUI_POPUPMENUMODE = 0x00000010,
        GUI_SYSTEMMENUMODE = 0x00000008
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GUITHREADINFO
    {
        public int cbSize;
        public GuiThreadInfoFlags flags;
        public IntPtr hwndActive;
        public IntPtr hwndFocus;
        public IntPtr hwndCapture;
        public IntPtr hwndMenuOwner;
        public IntPtr hwndMoveSize;
        public IntPtr hwndCaret;
        public System.Drawing.Rectangle rcCaret;
    }
}