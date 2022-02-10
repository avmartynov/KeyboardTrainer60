global using static System.ArgumentNullException;

using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Twidlle.Library.Testing.Diagnostics;
using Xunit;

[assembly: ComVisible(false)]
[assembly: Guid("c71741ac-ceac-4b73-ba0a-987c113854b6")]
[assembly: SupportedOSPlatform("Windows7.0")]

[CollectionDefinition("AllTests")]
public class AllTestsCollection : ICollectionFixture<TestRun>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

