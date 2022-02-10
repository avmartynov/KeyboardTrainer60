using Microsoft.Extensions.Logging;
using Twidlle.Library.DependencyInjection;

namespace Twidlle.Library.WinForms.Mvp.Forms;

/// <summary>
/// Базовый класс предъявителя формы, работающего в собственной области видимости.
/// </summary>
/// <typeparam name="TFormView"> Интерфейс предъявляемой формы. </typeparam>
/// <typeparam name="TFormModel">  </typeparam>
public abstract class SelfScopeFormPresenter<TFormView, TFormModel> : FormPresenter<TFormView, TFormModel>, ISelfScopeService
    where TFormView : class, IFormView<TFormModel>
{
    private IDisposable? _serviceScope;
    private readonly ILogger _logger;

    protected SelfScopeFormPresenter(TFormView formView, ILogger logger) 
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

        FormView.Disposed += (_, _) => Dispose();

        _logger.LogDebug("Service scope has been taken.");
    }
}