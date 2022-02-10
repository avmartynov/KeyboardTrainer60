global using static System.ArgumentNullException;

using System.Runtime.InteropServices;
using Twidlle.Library.Testing.Diagnostics;
using Xunit;

[assembly: ComVisible(false)]
[assembly: Guid("2bbba136-f940-40b1-85e0-28009ba41aff")]

[CollectionDefinition("AllTests")]
public class AllTestsCollection : ICollectionFixture<TestRun>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}