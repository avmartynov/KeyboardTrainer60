using System.Runtime.InteropServices;
using Twidlle.Library.Testing.Diagnostics;
using Xunit;

[assembly: ComVisible(false)]
[assembly: Guid("daf9cc45-c8df-4e4a-b9ad-bea495073b80")]

[CollectionDefinition("AllTests")]
public class AllTestsCollection : ICollectionFixture<TestRun>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}