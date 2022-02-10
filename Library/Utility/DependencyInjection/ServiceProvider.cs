namespace Twidlle.Library.DependencyInjection;

/// <summary>
/// Поставщик сервиса определённого типа (конкретная фабрика). 
/// </summary>
/// <typeparam name="TService"> Тип создаваемого сервиса. </typeparam>
public class ServiceProvider<TService> : IServiceProvider<TService> where TService : class
{
    private readonly IServiceProvider _services;

    public ServiceProvider(IServiceProvider services)
    {
        ThrowIfNull(services);

        _services = services;
    }

    public TService GetService() =>
        _services.GetRequiredService<TService>();
}
