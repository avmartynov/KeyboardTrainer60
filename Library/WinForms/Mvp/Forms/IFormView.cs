namespace Twidlle.Library.WinForms.Mvp.Forms;

/// <summary>
///  Базовый интерфейс плавающей формы.
/// </summary>
public interface IFormView<out TFormModel> : IView<TFormModel>, IDisposable
{
    /// <summary> Стартует показ плавающей формы. </summary>
    /// <remarks> Обычно это вызов Form.Show(). </remarks>
    void StartForm();

    /// <summary> Пользователь закрыл плавающую форму. </summary>
    event EventHandler Disposed;
}