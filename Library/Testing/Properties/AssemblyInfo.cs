global using static System.ArgumentNullException;

using System.Reflection;
using System.Runtime.InteropServices;
using Twidlle.Library.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} Testing Library")]
[assembly: AssemblyDescription("")]
[assembly: Guid("d1735971-2e39-4216-bdb3-7cf32346d508")]

[assembly: AssemblyProduct(ProductInfo.Name)]
[assembly: AssemblyCopyright(ProductInfo.Copyright)]
[assembly: AssemblyConfiguration(ProductInfo.BuildConfiguration)]
[assembly: AssemblyCompany(CompanyInfo.Name)]
[assembly: AssemblyTrademark(CompanyInfo.Trademark)]
[assembly: AssemblyVersion(ProductInfo.Version)]
[assembly: AssemblyFileVersion(ProductInfo.Version)]
[assembly: AssemblyInformationalVersion(ProductInfo.Version)]
[assembly: ComVisible(false)]
