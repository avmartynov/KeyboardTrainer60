namespace Twidlle.Library.VirtualTime;

/// <summary>
/// Поставщик виртуального времени в системе с автоматическим, равномерным движением времени.
/// </summary>
public interface IAutoTimeProvider : ITimeProvider 
{
    /// <summary> Скорость виртуального времени относительно естественного времени. </summary>
    double TimeFlowFactor { get; }
}