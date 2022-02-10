using Twidlle.Library.VirtualTime;

namespace Twidlle.Library.Testing.VirtualDateTime;

/// <summary> Поставщик времени в системе с автоматическим
/// ускоренным или замедленным движением времени, основанный
/// на масштабировании естественного течения времени. </summary>
public class AutoTimeProvider : IAutoTimeProvider
{
    private readonly DateTimeOffset _startTime;
    private readonly DateTimeOffset _startVirtualTime;

    public double TimeFlowFactor { get; }

    public AutoTimeProvider(AutoTimeProviderSettings settings)
    {
        _startTime = DateTimeOffset.Now;
        _startVirtualTime = settings.Start;
        TimeFlowFactor = settings.TimeFlowFactor;
    }

    public DateTimeOffset Now =>
        _startVirtualTime + TimeSpan.FromMilliseconds((DateTimeOffset.Now - _startTime).TotalMilliseconds * TimeFlowFactor);
}

public class AutoTimeProviderSettings
{
    public double TimeFlowFactor { get; init; } = 1.0;
    public DateTimeOffset Start { get; init; } = DateTimeOffset.Now;
}
