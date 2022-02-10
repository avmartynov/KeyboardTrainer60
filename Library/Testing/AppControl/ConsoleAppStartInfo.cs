namespace Twidlle.Library.Testing.AppControl;

/// <summary>
/// Данные для старта консольного приложения. Работает вместе с классом ConsoleApp.
/// </summary>
public class ConsoleAppStartInfo
{
    public ConsoleAppStartInfo(params string[] paths)
    {
        ExePath = Path.Combine(paths);
        WorkingDirectory = Path.GetDirectoryName(ExePath);
        Arguments = "";
    }

    /// <summary> Путь к исполняемому файлу приложения. </summary>
    public string ExePath { get; } 

    /// <summary> Путь к текущему каталогу для исполнения приложения. </summary>
    public string? WorkingDirectory { get; init; }

    /// <summary> Аргументы командной строки для запуска приложения. </summary>
    public string? Arguments { get; init; }

    /// <summary> Выводимая приложением на консоль строка при переходе приложения в режим ожидания нажатия [Enter] или ввода QuitLine. </summary>
    public string? WaitLine { get; init; }

    /// <summary> Ввод этой строки останавливает приложение. Если эти строка не задана или пуста, 
    /// то приложение должно закрываться при нажатии [Enter]. </summary>
    public string? QuitLine { get; init; }

    /// <summary> Признак отмены ожидания перехода приложения в режим ожидания нажатия [Enter]. </summary>
    public bool NoWait { get; init; }
}

