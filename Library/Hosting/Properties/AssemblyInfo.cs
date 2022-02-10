global using static System.ArgumentNullException;

using System.Reflection;
using System.Runtime.InteropServices;
using Twidlle.Library.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} Application Library")]
[assembly: AssemblyDescription("")]
[assembly: Guid("0e0ccf9f-03a9-4c2c-96b5-c12130f4ca55")]

[assembly: AssemblyProduct(ProductInfo.Name)]
[assembly: AssemblyCopyright(ProductInfo.Copyright)]
[assembly: AssemblyConfiguration(ProductInfo.BuildConfiguration)]
[assembly: AssemblyCompany(CompanyInfo.Name)]
[assembly: AssemblyTrademark(CompanyInfo.Trademark)]
[assembly: AssemblyVersion(ProductInfo.Version)]
[assembly: AssemblyFileVersion(ProductInfo.Version)]
[assembly: AssemblyInformationalVersion(ProductInfo.Version)]
[assembly: ComVisible(false)]
