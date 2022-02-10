namespace Twidlle.Library.WinForms.Mvp.Dialogs;

/// <summary>
///  Интерфейс предъявителя диалогового окна.
/// </summary>
public interface IDialogPresenter
{
    /// <summary>
    /// Отображает диалоговое окно и ожидает его закрытия.
    /// </summary>
    /// <returns>
    /// True - если предъявитель решил показывать диалоговое окно,
    /// False - если решил не показывать.
    /// </returns>
    bool ShowDialog();
}