namespace Twidlle.Library.WinForms.Mvp.Forms;

/// <summary>
///  Интерфейс предъявителя плавающей формы.
/// </summary>
public interface IFormPresenter
{
    /// <summary>
    /// Стартует отображение плавающей формы.
    /// </summary>
    /// <returns>
    /// True - если предъявитель решил показывать диалоговое окно,
    /// False - если решил не показывать.
    /// </returns>
    bool StartForm();
}