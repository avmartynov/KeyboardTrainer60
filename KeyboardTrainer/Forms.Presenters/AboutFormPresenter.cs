using System.Configuration;
using Microsoft.Extensions.Logging;
using Twidlle.KeyboardTrainer.Core.Model.Properties;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.Library.Utility;
using Twidlle.Library.WinForms;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Presenters
{
    public sealed class AboutFormPresenter : SelfScopeDialogPresenter<IAboutForm, AboutFormModel>, IAboutFormPresenter
    {
        public AboutFormPresenter(IAboutForm formView,
                                  IClipboardUtility clipboard,
                                  ILogger<AboutFormPresenter> logger) 
            : base(formView, logger)
        {
            formView.Model.Title         = ProductInfo.Name;
            formView.Model.Product       = ProductInfo.Name;
            formView.Model.Version       = ProductInfo.Version;
            formView.Model.CompanyName   = CompanyInfo.Name;
            formView.Model.CopyrightYear = ProductInfo.Year;

            formView.CopyDiagnosticsInfo += () => clipboard.Copy(FormExtensions.GetDiagnosticsInfo());
            formView.OpenConfigDirectory += OpenUserConfigFileFolder;
        }

        private static void OpenUserConfigFileFolder()
        {
            var filePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            var dirName = Path.GetDirectoryName(filePath);
            if (dirName == null)
                throw new InvalidOperationException("There is no config directory.");

            var dir = new DirectoryInfo(dirName);
            if (!dir.Exists)
                throw new InvalidOperationException("There is no config directory.");

            dir.Open();
        }
    }
}
