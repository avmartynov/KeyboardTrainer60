global using static System.ArgumentNullException;

using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;
using System.Runtime.Versioning;
using Twidlle.KeyboardTrainer.Core.Model.Properties;

[assembly: AssemblyTitle($"{CompanyInfo.Name} Keyboard Trainer Application")]
[assembly: AssemblyDescription("")]
[assembly: Guid("31b46d3f-59ba-4186-90ff-e27ef7220d7f")]

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