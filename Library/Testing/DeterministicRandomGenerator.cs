using Twidlle.Library.Utility;

namespace Twidlle.Library.Testing;

public class DeterministicRandomGenerator : RandomGenerator
{
    public DeterministicRandomGenerator() : base(576574) { }
}
