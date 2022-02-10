namespace Twidlle.Library.VirtualTime;

/// <summary>
/// Поставщик виртуального времени, совпадающего с натуральным временем
/// (с природным, естественным, настоящим временем).
/// </summary>
public class NaturalTimeProvider : IAutoTimeProvider
{
    public DateTimeOffset Now => 
        DateTimeOffset.Now;

    public double TimeFlowFactor => 
        1.0;
}

