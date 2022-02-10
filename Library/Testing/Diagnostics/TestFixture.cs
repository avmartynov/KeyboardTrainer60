using System.Runtime.CompilerServices;

namespace Twidlle.Library.Testing.Diagnostics;

public class TestFixture
{
    private readonly TestFixtureFacade _facade = new();

    protected void Invoke(Action testMethodBody, [CallerMemberName] string? testMethodName = null) =>
        _facade.Execute(testMethodBody, testMethodName);
}