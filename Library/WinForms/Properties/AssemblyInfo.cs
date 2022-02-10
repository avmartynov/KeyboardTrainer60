global using static System.ArgumentNullException;

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Twidlle.Library.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} WinForms Library")]
[assembly: AssemblyDescription("")]
[assembly: Guid("ebef80ac-246b-4b4c-ab75-a9609d35049f")]

[assembly: AssemblyProduct(ProductInfo.Name)]
[assembly: AssemblyCopyright(ProductInfo.Copyright)]
[assembly: AssemblyConfiguration(ProductInfo.BuildConfiguration)]
[assembly: AssemblyCompany(CompanyInfo.Name)]
[assembly: AssemblyTrademark(CompanyInfo.Trademark)]
[assembly: AssemblyVersion(ProductInfo.Version)]
[assembly: AssemblyFileVersion(ProductInfo.Version)]
[assembly: AssemblyInformationalVersion(ProductInfo.Version)]
[assembly: ComVisible(false)]

[assembly: TargetPlatform("Windows7.0")]
[assembly: SupportedOSPlatform("Windows7.0")]
