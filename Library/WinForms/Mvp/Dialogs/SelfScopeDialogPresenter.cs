using Microsoft.Extensions.Logging;
using Twidlle.Library.DependencyInjection;

namespace Twidlle.Library.WinForms.Mvp.Dialogs;

/// <summary>
/// Базовый класс предъявителя формы, работающего в собственной области видимости.
/// </summary>
/// <typeparam name="TFormView">Интерфейс предъявляемой формы. </typeparam>
/// <typeparam name="TFormModel">  </typeparam>
public abstract class SelfScopeDialogPresenter<TFormView, TFormModel> 
    : DialogPresenter<TFormView, TFormModel>, ISelfScopeService
    where TFormView : class, IDialogView<TFormModel>
{
    private IDisposable? _serviceScope;
    private readonly ILogger _logger;

    protected SelfScopeDialogPresenter(TFormView formView, ILogger logger) 
        : base(formView)
    {
        ThrowIfNull(logger);

        _logger = logger;
    }

    public void Dispose()
    {
        if (_serviceScope == null)
        {
            return;
        }

        var scope = _serviceScope;
        _serviceScope = null;

        scope.Dispose();
        _logger.LogDebug("Service scope disposed.");
    }

    public void SetServiceScope(IDisposable serviceScope)
    {
        ThrowIfNull(serviceScope);

        _serviceScope = serviceScope;

        _logger.LogDebug("Service scope has been taken.");
    }

    public override bool ShowDialog()
    {
        try
        {
            return base.ShowDialog();
        }
        finally
        {
            Dispose();
        }
    }
}