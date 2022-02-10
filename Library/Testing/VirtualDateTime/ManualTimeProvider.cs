using Twidlle.Library.VirtualTime;

namespace Twidlle.Library.Testing.VirtualDateTime;

/// <summary>
/// Поставщик виртуального времени с произвольным управлением течением времени.
/// </summary>
public class ManualTimeProvider: ITimeProvider
{
    public DateTimeOffset Now { get; private set; } = DateTimeOffset.Now;

    public void SetNaturalCurrentTime() =>
        SetCurrentTime(DateTimeOffset.Now);

    public void SetCurrentTime(DateTimeOffset dateTime) => 
        Now = dateTime;

    public void Add(TimeSpan span) => 
        Now += span;

    public void AddDays(double days) => 
        Now += TimeSpan.FromDays(days);

    public void AddHours(double hours) => 
        Now += TimeSpan.FromHours(hours);

    public void AddMilliseconds(double milliseconds) => 
        Now += TimeSpan.FromMilliseconds(milliseconds);

    public void AddMinutes(double minutes) => 
        Now += TimeSpan.FromMinutes(minutes);

    public void AddSeconds(double seconds) => 
        Now += TimeSpan.FromSeconds(seconds);

    public void AddTicks(long ticks) => 
        Now += TimeSpan.FromTicks(ticks);
}

