namespace Twidlle.Library.DependencyInjection;

/// <summary>
/// Интерфейс компонента с собственной областью видимости.
/// </summary>
/// <remarks> Компонент должен удалить свою область видимости при собственном удалении. </remarks>
public interface ISelfScopeService : IDisposable
{
    /// <summary>
    /// Устанавливает владение областью видимости.
    /// </summary>
    void SetServiceScope(IDisposable serviceScope);
}

