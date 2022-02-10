using System.Linq.Expressions;
using Twidlle.Library.Utility;

namespace Twidlle.Library.WinForms;

/// <summary> Компонент сохраняет и восстанавливает положение формы на экране. </summary>
/// <remarks> 
/// Как использовать для формы AbcForm:
/// 1. В Settings надо добавить свойство с именем "AbcForm".
/// 
/// 2. В конструкторе формы AbcForm до Initialize надо вставить: 
///    FormRestoreManager.Initialize(this, Settings.Default, s => s.AbcForm);
/// 
/// 3. Убедиться (обеспечить), что при завершении приложения
///     (например, в MainForm_FormClosed) вызывается Settings.Default.Save();
/// </remarks>
public static class FormRestoreManager
{
    public static void RestoreFormLocation<TSettings>(this Form form,
                                             TSettings settings,
                                             Expression<Func<TSettings, string?>> formStateProperty)
    {
        settings = settings ?? throw new ArgumentNullException(nameof(settings));

        form.RestoreFormLocation(() => settings.GetProperty(formStateProperty)!, 
                                  s => settings.SetProperty(formStateProperty, s));
    }

    public static void RestoreFormLocation(this Form form,
                                           Func<string> loadFormState,
                                           Action<string> saveFormState)
    {
        form.Load += (_, _) => OnLoad(form, loadFormState());
        form.FormClosing += (_, _) => saveFormState(GetFormState(form).ToJson());
    }


    private static void OnLoad(Form form, string formStateLoaded)
    {
        try
        {
            // При первом вызове формы используем состояние формы по умолчанию.
            if (!string.IsNullOrEmpty(formStateLoaded))
                SetFormState(form, formStateLoaded.JsonTo<FormState>());
        }
        catch (Exception)
        {
            // Stay default form state.
        }
    }


    private static FormState GetFormState(Form form)
    {
        return new () 
        {
            Size = form.WindowState == FormWindowState.Normal ? form.Size : form.RestoreBounds.Size,
            Location = form.WindowState == FormWindowState.Normal ? form.Location : form.RestoreBounds.Location,
            State = form.WindowState
        };
    }


    private static void SetFormState(Form form, FormState formState)
    {
        if (form.FormBorderStyle == FormBorderStyle.Sizable ||
            form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
        {
            form.Size = formState.Size;
        }
        form.Location = formState.Location;
        form.WindowState = formState.State;
    }


    private class FormState
    {
        public Size Size { get; init; }
        public Point Location { get; init; }
        public FormWindowState State { get; init; }
    }
}
