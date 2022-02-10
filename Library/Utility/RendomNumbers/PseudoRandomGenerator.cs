namespace Twidlle.Library.Utility;

public class PseudoRandomGenerator : RandomGenerator
{
    public PseudoRandomGenerator() 
        : base((int)(DateTimeOffset.Now.Ticks % int.MaxValue))
    {}
}