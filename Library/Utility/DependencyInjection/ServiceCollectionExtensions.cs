using Microsoft.Extensions.Configuration;
using Twidlle.Library.DependencyInjection;

namespace Twidlle.Library.DependencyInjection;

/// <summary>
/// Дополнительные процедуры регистрации компонент в IOC-контейнере 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует провайдер сервиса в IOC-контейнере.
    /// </summary>
    /// <typeparam name="TImplementation"> Тип реализации сервиса. </typeparam>
    /// <typeparam name="TService"> Тип интерфейса сервиса.</typeparam>
    /// <param name="services"> Collection of service descriptors. </param>
    /// 
    public static void AddScopedProvider<TService, TImplementation>(this IServiceCollection services)
        where TImplementation : class, TService
        where TService : class
    {
        services.AddScoped<IServiceProvider<TService>, ServiceProvider<TService>>();
        services.AddScoped<TService, TImplementation>();
    }

    /// <summary>
    /// Регистрирует в IOC-контейнере провайдер сервиса c собственной областью видимости..
    /// </summary>
    /// <typeparam name="TImplementation"> Тип реализации сервиса. </typeparam>
    /// <typeparam name="TService"> Тип интерфейса сервиса.</typeparam>
    /// <param name="services"> Collection of service descriptors. </param>
    /// 
    public static void AddSelfScopeProvider<TService, TImplementation>(this IServiceCollection services)
        where TImplementation : class, TService, ISelfScopeService
        where TService : class
    {
        services.AddScoped<IServiceProvider<TService>, SelfScopeServiceProvider<TImplementation>>();
        services.AddScoped<TImplementation>();
    }

    /// <summary>
    /// Регистрирует в IOC-контейнере конфигурационные данные.
    /// </summary>
    /// <typeparam name="TConfigData"> Тип конфигурационных данных. </typeparam>
    /// <param name="services"> Collection of service descriptors. </param>
    /// <param name="configuration"> Конфигурация. </param>
    /// <param name="sectionName"> Имя секции конфигурации. По умолчанию используется имя типа конфигурационных данных. </param>
    /// 
    public static void AddSingleton<TConfigData>(this IServiceCollection services, 
                                              IConfiguration configuration,
                                              string? sectionName = null) 
        where TConfigData : class
    {
        services.AddSingleton(configuration.GetSection(sectionName ?? typeof(TConfigData).Name).Get<TConfigData>());
    }
}