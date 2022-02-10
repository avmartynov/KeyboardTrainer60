global using Microsoft.Extensions.DependencyInjection;

global using static System.ArgumentNullException;

using System.Reflection;
using System.Runtime.InteropServices;
using Twidlle.Library.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} Utility Library")]
[assembly: AssemblyDescription("")]
[assembly: Guid("ca53d3a2-51dd-41e8-adb1-d568fc4d9f35")]

[assembly: AssemblyProduct(ProductInfo.Name)]
[assembly: AssemblyCopyright(ProductInfo.Copyright)]
[assembly: AssemblyConfiguration(ProductInfo.BuildConfiguration)]
[assembly: AssemblyCompany(CompanyInfo.Name)]
[assembly: AssemblyTrademark(CompanyInfo.Trademark)]
[assembly: AssemblyVersion(ProductInfo.Version)]
[assembly: AssemblyFileVersion(ProductInfo.Version)]
[assembly: AssemblyInformationalVersion(ProductInfo.Version)]
[assembly: ComVisible(false)]