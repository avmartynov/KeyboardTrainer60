namespace Twidlle.Library.WinForms.Mvp.Forms;

/// <summary>
/// Базовый класс предъявителя постоянной формы.
/// </summary>
/// <typeparam name="TFormView">Интерфейс предъявляемой формы. </typeparam>
/// <typeparam name="TFormModel"> </typeparam>/// 
public abstract class FormPresenter<TFormView, TFormModel> : IFormPresenter 
    where TFormView : class, IFormView<TFormModel>
{
    protected TFormView FormView { get; }

    protected FormPresenter(TFormView formView)
    {
        ThrowIfNull(formView);    

        FormView = formView;
    }

    protected virtual bool InitializeForm() => 
        true;

    public bool StartForm()
    {
        if (!InitializeForm())
            return false;

        FormView.RedrawModel();
        FormView.StartForm();
        return true;
    }
}