using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Models;

public class PreferencesFormModel
{
    public bool   OpenLastFile   { get; set; }
    public bool   VoiceEnable    { get; set; }
    public string UILanguageCode { get; set; } = "en-US";
    public KeyValuePair<string, string>[] UILanguages { get; set; } = default!;
}

public interface IPreferencesForm : IDialogView<PreferencesFormModel>
{
    event Action Accept;
}

public interface IPreferencesFormPresenter : IDialogPresenter
{
}