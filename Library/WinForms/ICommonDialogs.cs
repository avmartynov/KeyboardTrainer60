using System.Diagnostics.CodeAnalysis;

namespace Twidlle.Library.WinForms;

public interface ICommonDialogs
{
    bool AskYesNo(string text, 
                  string caption, 
                  int defaultOption = 0);

    bool? AskYesNoCancel(string text, 
                         string caption, 
                         int defaultOption = 0);

    /// <summary>
    /// Запросить путь к файлу для сохранения.
    /// </summary>
    public bool AskForFileToSave(string title, 
                            string initialFilePath, 
                            string filter, 
                            string extension,
                            [NotNullWhen(true)] out string? filePath);

    /// <summary>
    /// Запросить путь к файлу для открывания.
    /// </summary>
    public bool AskForFileToOpen(string title, 
                                     string initialFilePath, 
                                     string filter, 
                                     string extension,
                                     [NotNullWhen(true)] out string? filePath);
}
