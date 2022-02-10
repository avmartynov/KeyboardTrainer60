using FluentAssertions;
using FluentAssertions.Numeric;

namespace Twidlle.Library.Testing.Diagnostics;

public static class AssertExtensions
{
    /// <summary>
    /// Актуальная величина не должна отличаться от ожидаемой на заданное число процентов от ожидаемой величины.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="expectedValue"> Ожидаемая величина. </param>
    /// <param name="maxDeltaPercent"> Максимально допустимое различие в процентах.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static AndConstraint<NumericAssertions<double>> BeApproximatelyPercent(this NumericAssertions<double> parent, 
                                                                                  double expectedValue, 
                                                                                  double maxDeltaPercent)
    {
        if (!parent.Subject.HasValue)
            throw new InvalidOperationException("Need actual value.");

        var actualValue = parent.Subject.Value;
        var deltaPercent = Math.Abs(expectedValue - actualValue) / expectedValue * 100.0;
        return deltaPercent.Should().BeLessThanOrEqualTo(maxDeltaPercent);
    }

    /// <summary>
    /// Актуальная величина не должна отличаться от ожидаемой на заданное число процентов от ожидаемой величины.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="expectedValue"> Ожидаемая величина. </param>
    /// <param name="maxDeltaPercent"> Максимально допустимое различие в процентах.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static AndConstraint<NumericAssertions<int>> BeApproximatelyPercent(this NumericAssertions<int> parent,
                                                                               double expectedValue,
                                                                               double maxDeltaPercent)
    {
        if (!parent.Subject.HasValue)
            throw new InvalidOperationException("Need actual value.");

        var actualValue = parent.Subject.Value;
        var deltaPercent = Math.Abs(expectedValue - actualValue) / expectedValue * 100.0;

        deltaPercent.Should().BeLessThanOrEqualTo(maxDeltaPercent);

        return actualValue.Should().Be(actualValue);
    }
}