using Microsoft.Extensions.Logging;
using Twidlle.Library.VirtualTime;

namespace Library.Testing.Tests.VirtualTime;

public class Abc
{
    private readonly IAutoTimeProvider _timeProvider;
    private readonly AbcSettings _settings;
    private readonly ILogger _logger;

    public Abc(AbcSettings settings, 
               IAutoTimeProvider timeProvider, 
               ILogger<Abc> logger)
    {
        _settings = settings;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public void Do()
    {
        _logger.LogInformation("Do start.");

        _timeProvider.SleepSeconds(_settings.DurationSeconds);

        _logger.LogInformation("Do finish.");
    }
}