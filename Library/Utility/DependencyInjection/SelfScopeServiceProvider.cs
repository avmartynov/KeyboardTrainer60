namespace Twidlle.Library.DependencyInjection;

/// <summary>
/// Поставщик сервиса определённого типа (конкретная фабрика) в отдельной (собственной) области видимости. 
/// </summary>
/// <typeparam name="TSelfScopeService"> Тип создаваемого сервиса. </typeparam>
public class SelfScopeServiceProvider<TSelfScopeService> : IServiceProvider<TSelfScopeService> 
    where TSelfScopeService : ISelfScopeService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SelfScopeServiceProvider(IServiceScopeFactory scopeFactory)
    {
        ThrowIfNull(scopeFactory);

        _scopeFactory = scopeFactory;
    }

    public TSelfScopeService GetService()
    {
        var serviceScope = _scopeFactory.CreateScope();
        var selfScopeService = serviceScope.ServiceProvider.GetRequiredService<TSelfScopeService>();
        selfScopeService.SetServiceScope(serviceScope);
        return selfScopeService;
    }
}
