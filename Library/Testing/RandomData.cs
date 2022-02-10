using Twidlle.Library.Utility;

namespace Twidlle.Library.Testing;

/// <summary>
/// Процедуры для генерирования случайных тестовых данных.
/// </summary>
public static class RandomData
{
    /// <summary> Возвращает случайную строку символов заданной длинны. </summary>
    /// <param name="randomGenerator"> </param>
    /// <param name="size">Размер строки.</param>
    public static string NextSymbolString(this IRandomGenerator randomGenerator, int size) =>
        new (Enumerable.Range(0, size).Select(_ => randomGenerator.NextSymbol()).ToArray());

    /// <summary> Возвращает случайную строку цифр заданной длинны. </summary>
    /// <param name="randomGenerator"> </param>
    /// <param name="size">Размер строки.</param>
    public static string NextDigitString(this IRandomGenerator randomGenerator, int size) =>
        new (Enumerable.Range(0, size).Select(_ => randomGenerator.NextDigit()).ToArray());


    /// <summary> Возвращает случайный символ. </summary>
    /// <param name="randomGenerator"> </param>
    private static char NextSymbol(this IRandomGenerator randomGenerator)
    {
        var minValue = char.ConvertToUtf32(" ", 0);
        var maxValue = char.ConvertToUtf32("~", 0);
        var utf32Code = randomGenerator.Next(minValue, maxValue);
        var s = char.ConvertFromUtf32(utf32Code);
        return s[0];
    }


    /// <summary> Возвращает случайную цифру. </summary>
    /// <param name="randomGenerator"> </param>
    private static char NextDigit(this IRandomGenerator randomGenerator)
    {
        var minValue = char.ConvertToUtf32("0", 0);
        var maxValue = char.ConvertToUtf32("9", 0);
        var utf32Code = randomGenerator.Next(minValue, maxValue);
        var s = char.ConvertFromUtf32(utf32Code);
        return s[0];
    }
}