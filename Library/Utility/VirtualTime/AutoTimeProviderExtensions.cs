namespace Twidlle.Library.VirtualTime;

public static class AutoTimeProviderExtensions
{
    /// <summary> Возвращает натуральное временное расстояние, соответствующее заданному числу виртуальных дней. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualDays"> Число виртуальных дней. </param>
    public static TimeSpan NaturalTimeSpanFromDays(this IAutoTimeProvider timeProvider, double virtualDays) => 
        TimeSpan.FromDays(virtualDays / timeProvider.TimeFlowFactor);

    /// <summary> Возвращает натуральное временное расстояние, соответствующее заданному числу виртуальных часов. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualHours"> Число виртуальных часов. </param>
    public static TimeSpan NaturalTimeSpanFromHours(this IAutoTimeProvider timeProvider, double virtualHours) => 
        TimeSpan.FromHours(virtualHours / timeProvider.TimeFlowFactor);

    /// <summary> Возвращает натуральное временное расстояние, соответствующее заданному числу виртуальных минут. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualMinutes"> Число виртуальных минут. </param>
    public static TimeSpan NaturalTimeSpanFromMinutes(this IAutoTimeProvider timeProvider, double virtualMinutes) => 
        TimeSpan.FromMinutes(virtualMinutes / timeProvider.TimeFlowFactor);

    /// <summary> Возвращает натуральное временное расстояние, соответствующее заданному числу виртуальных секунд. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualSeconds"> Число виртуальных секунд. </param>
    public static TimeSpan NaturalTimeSpanFromSeconds(this IAutoTimeProvider timeProvider, double virtualSeconds) =>
        TimeSpan.FromSeconds(virtualSeconds / timeProvider.TimeFlowFactor);

    /// <summary> Возвращает натуральное временное расстояние, соответствующее заданному числу виртуальных миллисекунд. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualMilliseconds"> Число виртуальных миллисекунд. </param>
    public static TimeSpan NaturalTimeSpanFromMilliseconds(this IAutoTimeProvider timeProvider, double virtualMilliseconds) => 
        TimeSpan.FromMilliseconds(virtualMilliseconds / timeProvider.TimeFlowFactor);

    /// <summary> Возвращает натуральное временное расстояние, соответствующее заданному числу виртуальных тиков. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualTicks"> Число виртуальных тиков. </param>
    public static TimeSpan NaturalTimeSpanFromTicks(this IAutoTimeProvider timeProvider, long virtualTicks) => 
        TimeSpan.FromTicks((long)(virtualTicks / timeProvider.TimeFlowFactor));

    /// <summary> Преобразует временное расстояние в натуральном времени во временное расстояние в виртуальном времени. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="naturalTimeSpan"> Временное расстояние в натуральном времени. </param>
    public static TimeSpan ToVirtualTimeSpan(this IAutoTimeProvider timeProvider, TimeSpan naturalTimeSpan) => 
        TimeSpan.FromMilliseconds(naturalTimeSpan.TotalMilliseconds * timeProvider.TimeFlowFactor);

    /// <summary> Преобразует временное расстояние в виртуальном времени во временное расстояние в натуральном времени. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualTimeSpan"> Временное расстояние в виртуальном времени. </param>
    public static TimeSpan ToNaturalTimeSpan(this IAutoTimeProvider timeProvider, TimeSpan virtualTimeSpan) =>
        TimeSpan.FromMilliseconds(virtualTimeSpan.TotalMilliseconds / timeProvider.TimeFlowFactor);

    /// <summary> Приостанавливает исполнение текущего потока в течение виртуального времени,
    /// соответствующего заданному натуральному времени. </summary>
    /// <param name="timeProvider"> Поставщик виртуального времени. </param>
    /// <param name="virtualTimeSpan"></param>
    public static void Sleep(this IAutoTimeProvider timeProvider, TimeSpan virtualTimeSpan) =>
        Thread.Sleep(timeProvider.ToNaturalTimeSpan(virtualTimeSpan));

    public static void SleepSeconds(this IAutoTimeProvider timeProvider, double virtualSeconds) =>
        timeProvider.Sleep(TimeSpan.FromSeconds(virtualSeconds));

    public static void SleepMinutes(this IAutoTimeProvider timeProvider, double virtualMinutes) =>
        timeProvider.Sleep(TimeSpan.FromMinutes(virtualMinutes));
}