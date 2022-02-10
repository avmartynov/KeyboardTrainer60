using System.Diagnostics;

namespace Twidlle.Library.Utility;

/// <summary>
/// Методы-расширения для работы с пользовательской оболочкой Windows.
/// </summary>
public static class WindowsShellExtensions
{
    /// <summary> Открывает папку в Windows Explorer. </summary>        
    public static void Open(this DirectoryInfo directory)
    {
        ThrowIfNull(directory);

        if (!directory.Exists)
        {
            throw new InvalidOperationException($"Directory '{directory.FullName}' does not exist.");
        }
        ShellOpen(directory.FullName);
    }

    /// <summary> Открывает файл в программе, настроенной в Windows для просмотра файлов этого типа. </summary>        
    public static void Open( this FileInfo file)
    {
        ThrowIfNull(file);

        if (!file.Exists)
        {
            throw new InvalidOperationException($"File '{file.FullName}' does not exist.");
        }

        ShellOpen(file.FullName);
    }

    /// <summary> Открывает url-адрес в броузере по умолчанию. </summary>        
    public static void NavigateTo(this Uri url)
    {
        ThrowIfNull(url);

        ShellOpen(url.ToString());
    }

    private static void ShellOpen(string fileName)
    {
        ThrowIfNull(fileName);

        Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
    }
}

