namespace Twidlle.Library.WinForms.Mvp.Dialogs;

/// <summary>
/// Базовый класс предъявителя модальной формы.
/// </summary>
/// <typeparam name="TFormView"> Интерфейс предъявляемой формы. </typeparam>
/// <typeparam name="TFormModel">  </typeparam>/// 
public abstract class DialogPresenter<TFormView, TFormModel> : IDialogPresenter
    where TFormView : class, IDialogView<TFormModel>
{
    protected TFormView FormView { get; }

    protected DialogPresenter(TFormView formView)
    {
        ThrowIfNull(formView);

        FormView = formView;
    }

    protected virtual bool InitializeForm() =>
        true;

    public virtual bool ShowDialog()
    {
        if (!InitializeForm())
            return false;
        
        FormView.RedrawModel();
        FormView.ShowDialogForm();
        return true;
    }
}