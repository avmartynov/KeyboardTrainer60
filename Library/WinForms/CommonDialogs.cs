using System.Diagnostics.CodeAnalysis;
using Twidlle.Library.Utility;

namespace Twidlle.Library.WinForms;

public class CommonDialogs : ICommonDialogs
{
    public bool AskYesNo(string text, string caption, int defaultOption = 0)
    {
        var result = MessageBox.Show(text,
                                     caption,
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question,
                                     defaultOption.ToEnum<MessageBoxDefaultButton>());
        return result == DialogResult.Yes;
    }

    public bool? AskYesNoCancel(string text, string caption, int defaultOption = 0)
    {
        var result = MessageBox.Show(text,
                                     caption,
                                     MessageBoxButtons.YesNoCancel,
                                     MessageBoxIcon.Question,
                                     defaultOption.ToEnum<MessageBoxDefaultButton>());
        return result switch
        {
            DialogResult.Yes => true,
            DialogResult.No => false,
            _ => null
        };
    }

    public bool AskForFileToSave(string title, 
                                 string initialFilePath, 
                                 string filter, 
                                 string extension,
                                 [NotNullWhen(true)] out string? filePath)
    {
        return AskFile<SaveFileDialog>(title, 
                                       initialFilePath, 
                                       filter, 
                                       extension,
                                       out filePath);
    }

    public bool AskForFileToOpen(string title, 
                                 string initialFilePath, 
                                 string filter, 
                                 string extension,
                                 [NotNullWhen(true)] out string? filePath)
    {
        return AskFile<OpenFileDialog>(title, 
                                       initialFilePath, 
                                       filter, 
                                       extension,
                                       out filePath);
    }

    private static bool AskFile<TFileDialog>(string title,
                                             string initialFilePath,
                                             string filter,
                                             string extension,
                                             [NotNullWhen(true)] out string? filePath)
        where TFileDialog : FileDialog, new()
    {
        var dialog = new TFileDialog
        {
            Title = title,
            Filter = filter,
            DefaultExt = extension,
            InitialDirectory = Path.GetDirectoryName(initialFilePath),
            FileName = Path.GetFileName(initialFilePath),
        };
        var accepted = dialog.ShowDialog() == DialogResult.OK;
        filePath = accepted ? dialog.FileName : null;
        return accepted;
    }
}

