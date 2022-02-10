namespace Twidlle.Library.WinForms.Mvp;

/// <summary>
///  Базовый интерфейс формы c моделью.
/// </summary>
public interface IView<out TFormModel>
{
    /// <summary> Данные формы. </summary>
    public TFormModel Model { get; }

    /// <summary> Отображает в управляющих элементах формы изменившиеся данные. </summary>
    void RedrawModel() { }

    /// <summary> Данные формы изменены пользователем. </summary>
    event Action ModelChanged
    {
        // ReSharper disable once ValueParameterNotUsed
        add { }
        // ReSharper disable once ValueParameterNotUsed
        remove { }
    }
}
