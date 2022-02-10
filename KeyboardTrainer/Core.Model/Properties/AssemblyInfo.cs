global using static System.ArgumentNullException;

using System.Reflection;
using System.Runtime.InteropServices;
using Twidlle.KeyboardTrainer.Core.Model.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} KeyboardTrainer Core Abstractions")]
[assembly: AssemblyDescription("")]
[assembly: Guid("b7cbba76-1d24-4d97-b85d-8ef166a8dbc1")]

[assembly: AssemblyProduct(ProductInfo.Name)]
[assembly: AssemblyCopyright(ProductInfo.Copyright)]
[assembly: AssemblyConfiguration(ProductInfo.BuildConfiguration)]
[assembly: AssemblyCompany(CompanyInfo.Name)]
[assembly: AssemblyTrademark(CompanyInfo.Trademark)]
[assembly: AssemblyVersion(ProductInfo.Version)]
[assembly: AssemblyFileVersion(ProductInfo.Version)]
[assembly: AssemblyInformationalVersion(ProductInfo.Version)]
[assembly: ComVisible(false)]