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
public class ManualTimeProviderTests  : TestFixture
{
    private readonly IServiceProvider _services;

    public ManualTimeProviderTests()
    {
        _services = new ServiceCollection()
            .AddLogging()
            .AddSingleton<ILoggerFactory>(new LoggerFactory(new[] { new NLogLoggerProvider() }))

            .AddSingleton<ManualTimeProvider>()
            .AddSingleton<ITimeProvider>(services => services.GetRequiredService<ManualTimeProvider>())

            .AddTransient<Abc>()
            .AddTransient<Xyz>()
            .AddTransient<Foo>()

            .BuildServiceProvider();
    }

    /// <summary> Проверка ручного управления временем. </summary>
    [Fact]
    public void TimeMoveTest() => Invoke(() =>
    {
        var time = _services.GetRequiredService<ManualTimeProvider>();
        var abc  = _services.GetRequiredService<Foo>();
        var xyz  = _services.GetRequiredService<Foo>();

        time.SetCurrentTime(DateTimeOffset.Parse("2022-01-01"));
        abc.GetTime().Should().Be(time.Now);
        xyz.GetTime().Should().Be(time.Now);

        time.AddDays(2);

        abc.GetTime().Should().Be(time.Now);
        xyz.GetTime().Should().Be(time.Now);

        time.AddHours(30);

        abc.GetTime().Should().Be(time.Now);
        xyz.GetTime().Should().Be(time.Now);

        time.AddMinutes(400);

        abc.GetTime().Should().Be(time.Now);
        xyz.GetTime().Should().Be(time.Now);

        time.AddSeconds(500);

        abc.GetTime().Should().Be(time.Now);
        xyz.GetTime().Should().Be(time.Now);

        time.AddMilliseconds(6000);

        abc.GetTime().Should().Be(time.Now);
        xyz.GetTime().Should().Be(time.Now);

        time.AddTicks(6000);

        abc.GetTime().Should().Be(time.Now);
        xyz.GetTime().Should().Be(time.Now);
    });
}

public class Foo
{
    private readonly ITimeProvider _timeProvider;

    public Foo(ITimeProvider timeProvider) => 
        _timeProvider = timeProvider;

    public DateTimeOffset GetTime() =>
        _timeProvider.Now;
}