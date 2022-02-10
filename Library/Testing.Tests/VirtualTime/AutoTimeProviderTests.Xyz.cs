using Microsoft.Extensions.Logging;
using Twidlle.Library.VirtualTime;

namespace Library.Testing.Tests.VirtualTime;

public sealed class Xyz : IDisposable
{
    private readonly Timer _timer;
    private readonly ILogger _logger;

    public event Action? Tick;

    public Xyz(XyzSettings settings,
        IAutoTimeProvider timeProvider,
        ILogger<Xyz> logger)
    {
        _logger = logger;
        var period = timeProvider.ToNaturalTimeSpan(TimeSpan.FromSeconds(settings.TimerPeriodSeconds));
        _timer = new Timer(_ => Tick?.Invoke(), null, period, period);
        _logger.LogTrace("Started.");
    }

    public void Dispose()
    {
        ((IDisposable)_timer).Dispose();
        _logger.LogTrace("Disposed.");
    }
}

