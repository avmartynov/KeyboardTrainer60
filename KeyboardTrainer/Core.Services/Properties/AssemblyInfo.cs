global using static System.ArgumentNullException;

using System.Reflection;
using System.Runtime.InteropServices;
using Twidlle.KeyboardTrainer.Core.Model.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} KeyboardTrainer Core Services")]
[assembly: AssemblyDescription("")]
[assembly: Guid("37dc056f-4472-4505-a68b-e8a1b243984d")]

[assembly: AssemblyProduct(ProductInfo.Name)]
[assembly: AssemblyCopyright(ProductInfo.Copyright)]
[assembly: AssemblyConfiguration(ProductInfo.BuildConfiguration)]
[assembly: AssemblyCompany(CompanyInfo.Name)]
[assembly: AssemblyTrademark(CompanyInfo.Trademark)]
[assembly: AssemblyVersion(ProductInfo.Version)]
[assembly: AssemblyFileVersion(ProductInfo.Version)]
[assembly: AssemblyInformationalVersion(ProductInfo.Version)]
[assembly: ComVisible(false)]