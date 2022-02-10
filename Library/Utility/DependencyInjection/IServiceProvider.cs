namespace Twidlle.Library.DependencyInjection;

/// <summary>
///  Интерфейс поставщика сервиса определённого типа (конкретная фабрика). 
/// </summary>
public interface IServiceProvider<out TService>
{
    TService GetService();
}
