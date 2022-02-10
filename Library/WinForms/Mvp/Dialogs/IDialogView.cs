namespace Twidlle.Library.WinForms.Mvp.Dialogs;

/// <summary>
///  Базовый интерфейс модальной формы.
/// </summary>
public interface IDialogView<out TFormModel> : IView<TFormModel>, IDisposable
{
    /// <summary> Показывает модальную диалоговую форму и дожидается её закрытия. </summary>
    /// <remarks> Это или вызов Form.ShowDialog() или Application.Run().</remarks>
    void ShowDialogForm();
}
