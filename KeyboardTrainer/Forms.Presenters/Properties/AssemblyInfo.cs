global using static System.ArgumentNullException;

using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Twidlle.Library.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} KeyboardTrainer Presentation Services")]
[assembly: AssemblyDescription("")]
[assembly: Guid("7d0ae1f4-5a21-4bec-a7b4-24e0d4777b00")]

[assembly: AssemblyProduct(ProductInfo.Name)]
[assembly: AssemblyCopyright(ProductInfo.Copyright)]
[assembly: AssemblyConfiguration(ProductInfo.BuildConfiguration)]
[assembly: AssemblyCompany(CompanyInfo.Name)]
[assembly: AssemblyTrademark(CompanyInfo.Trademark)]
[assembly: AssemblyVersion(ProductInfo.Version)]
[assembly: AssemblyFileVersion(ProductInfo.Version)]
[assembly: AssemblyInformationalVersion(ProductInfo.Version)]
[assembly: ComVisible(false)]

[assembly: NeutralResourcesLanguage("en-US")]
[assembly: TargetPlatform("Windows7.0")]
[assembly: SupportedOSPlatform("Windows7.0")]