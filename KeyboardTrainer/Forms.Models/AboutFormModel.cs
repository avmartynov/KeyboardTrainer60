using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Models;

public class AboutFormModel
{
    public string? Title { get; set; }
    public string? Product { get; set; }
    public string? Version { get; set; }
    public string? CompanyName { get; set; }
    public string? CopyrightYear { get; set; }
}

public interface IAboutForm : IDialogView<AboutFormModel>
{
    event Action CopyDiagnosticsInfo;
    event Action OpenConfigDirectory;
}

public interface IAboutFormPresenter : IDialogPresenter
{
}
