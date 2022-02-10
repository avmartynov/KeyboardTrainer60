using System.Configuration;
using Twidlle.Library.Utility;

namespace Twidlle.Library.WinForms;

public static class FormExtensions
{
    /// <summary> Выполняет действие в UI-потоке. </summary>
    public static void InvokeUI(this Form owner, Action action)
    {
        ThrowIfNull(action);
        ThrowIfNull(owner);

        try
        {
            if (owner.InvokeRequired)
                owner.Invoke(action);
            else
                action();
        }
        catch (ObjectDisposedException)
        {
            // В очереди сообщений могут оставаться сообщения для форм, которые уже освобождены. 
        }
        catch (Exception x)
        {
            x.ShowMessageBox(owner);
        }
    }


    /// <summary> Выполняет действие в UI-потоке, перехватывая все исключения. </summary>
    public static void Invoke(this Form owner, Action action)
    {
        ThrowIfNull(action);
        ThrowIfNull(owner);

        try
        {
            action.Invoke();
        }
        catch (Exception x)
        {
            x.ShowMessageBox(owner);
        }
    }

    public static void ShowMessageBox(this Exception exception,
                                      IWin32Window? owner = null,
                                      string? message = null,
                                      string? caption = null)
    {
        ThrowIfNull(exception);

        Clipboard.SetText(GetDiagnosticsInfo() + ", " + Environment.NewLine + exception);

        caption ??= Application.ProductName;
        message ??= "Clipboard contains details of error.";
        message = $"{exception.Message}" + Environment.NewLine + Environment.NewLine + message;

        MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }


    public static string GetDiagnosticsInfo()
    {
        var filePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
        return new
        {
            ClrVersion = Environment.Version,
            OSVersion = Environment.OSVersion.VersionString,
            Is64BitOS = Environment.Is64BitOperatingSystem,
            Environment.Is64BitProcess,
            Environment.ProcessorCount,
            Environment.CurrentDirectory,
            Environment.CommandLine,
            Environment.MachineName,
            DomainName = Environment.UserDomainName,
            Environment.UserName,
            ConfigurationFile = filePath
        }.ToJsonIndented();
    }
}

