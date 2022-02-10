using System.Runtime.InteropServices;
using Twidlle.Library.Testing.Diagnostics;
using Xunit;

[assembly: ComVisible(false)]
[assembly: Guid("619ddec7-a735-442b-a9d8-bc2e1bfee4cb")]

namespace Library.Testing.Tests;

[CollectionDefinition("AllTests")]
public class AllTestsCollection : ICollectionFixture<TestRun>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

