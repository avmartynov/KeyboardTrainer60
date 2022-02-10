namespace Twidlle.Library.VirtualTime;

/// <summary>
/// Поставщик виртуального времени в системе.
/// </summary>
public interface ITimeProvider
{
    /// <summary> Текущий момент виртуального времени. </summary>
    DateTimeOffset Now { get; }
}