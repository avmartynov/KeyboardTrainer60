namespace Twidlle.Library.Testing.AppControl;

/// <summary>
/// Данные для старта оконного приложения. Работает вместе с классом WinAppController.
/// </summary>
public class WinAppStartInfo
{
    /// <param name="exeFilePaths">Путь к исполняемому файлу приложения
    /// (или отдельные компоненты этого пути).</param>
    public WinAppStartInfo(params string[] exeFilePaths)
    {
        ExeFilePath = Path.Combine(exeFilePaths);
        WorkingDirectory = Path.GetDirectoryName(ExeFilePath);
        Arguments = "";
    }

    /// <summary> Путь к исполняемому файлу приложения. </summary>
    public string ExeFilePath { get; }

    /// <summary> Путь к текущему каталогу для исполнения приложения. </summary>
    public string? WorkingDirectory { get; init; }

    /// <summary> Аргументы командной строки для запуска приложения. </summary>
    public string? Arguments { get; init; }

    public override string ToString() =>
        $"{ExeFilePath} {Arguments}, WorkingDirectory: '{WorkingDirectory}'";
}

