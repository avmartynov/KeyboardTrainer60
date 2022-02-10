using System.Diagnostics;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Twidlle.Library.Testing.Diagnostics;
using Twidlle.Library.Testing.VirtualDateTime;
using Twidlle.Library.VirtualTime;
using Xunit;

namespace Library.Testing.Tests.VirtualTime;

[Collection("AllTests")]
public class AutoTimeProviderTests : TestFixture
{
    private const int    _timeFlowFactor = 3;
    private const double _doTimeSpanSeconds = 10;
    private const double _timerPeriodSeconds = 10;

    private readonly IServiceProvider _services;

    public AutoTimeProviderTests()
    {
        _services = new ServiceCollection()
            .AddLogging()
            .AddSingleton<ILoggerFactory>(new LoggerFactory(new[] { new NLogLoggerProvider() }))

            .AddSingleton(new AutoTimeProviderSettings { TimeFlowFactor = _timeFlowFactor })
            .AddTransient<IAutoTimeProvider, AutoTimeProvider>()
            .AddTransient<ITimeProvider, AutoTimeProvider>()

            .AddSingleton(new AbcSettings { DurationSeconds = _doTimeSpanSeconds })
            .AddTransient<Abc>()

            .AddSingleton(new XyzSettings { TimerPeriodSeconds = _timerPeriodSeconds })
            .AddTransient<Xyz>()

            .BuildServiceProvider();
    }

    /// <summary> Проверка убыстрения виртуального времени при Thread.Sleep. </summary>
    [Fact]
    public void SleepingTest() => Invoke(() =>
    {
        var sw = Stopwatch.StartNew();

        _services.GetRequiredService<Abc>().Do();

        sw.Elapsed.TotalSeconds.Should()
            .BeApproximatelyPercent(_doTimeSpanSeconds / _timeFlowFactor, maxDeltaPercent: 20);
    });

    /// <summary> Проверка убыстрения виртуального времени при работе таймеров. </summary>
    [Fact]
    public void TimerTest() => Invoke(() =>
    {
        var sw = Stopwatch.StartNew();
        var finish = new AutoResetEvent(initialState: false);

        var xyz = _services.GetRequiredService<Xyz>();
        xyz.Tick += () => finish.Set();

        if (!finish.WaitOne(TimeSpan.FromSeconds(20)))
            throw new InvalidOperationException("Таймер не сработал");

        var actual = sw.Elapsed.TotalSeconds;
        const double expected = _timerPeriodSeconds / _timeFlowFactor;

        actual.Should().BeApproximatelyPercent(expected, maxDeltaPercent: 20);
    });
}

