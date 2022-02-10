using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.Library.WinForms;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Presenters
{
    public class PreferencesFormPresenter : SelfScopeDialogPresenter<IPreferencesForm, PreferencesFormModel>, IPreferencesFormPresenter
    {
        public PreferencesFormPresenter(IPreferencesForm formView, 
                                        ICommonDialogs commonDialogs,
                                        IApplicationControl applicationControl,
                                        UserSettings userSettings,
                                        IStringLocalizer<PreferencesFormPresenter> localizer,
                                        ILogger<PreferencesFormPresenter> logger) 
            : base(formView, logger)
        {
            ThrowIfNull(commonDialogs);
            ThrowIfNull(applicationControl);
            ThrowIfNull(userSettings);
            ThrowIfNull(localizer);

            formView.Model.OpenLastFile = userSettings.OpenLastFile;
            formView.Model.VoiceEnable  = userSettings.UseVoice;
            formView.Model.UILanguageCode = Thread.CurrentThread.CurrentCulture.Name;
            formView.Model.UILanguages = GetAvailableCultures(typeof(PreferencesFormPresenter))
                    .Select(x => new KeyValuePair<string, string>(x.Name, x.NativeName)).ToArray();

            formView.Accept += () =>
            {
                var languageUIChanged = Thread.CurrentThread.CurrentCulture.Name != FormView.Model.UILanguageCode;

                userSettings.OpenLastFile = FormView.Model.OpenLastFile;
                userSettings.UseVoice     = FormView.Model.VoiceEnable;
                userSettings.UILanguage   = FormView.Model.UILanguageCode;

                if (languageUIChanged)
                {
                    var needRestart = commonDialogs.AskYesNo(localizer["RestartQuestion"], localizer["RestartDialogCaption"]);
                    if (needRestart)
                        applicationControl.RestartApplication();
                }
            };
        }

        private static IList<CultureInfo> GetAvailableCultures(Type localizedType)
        {
            var result = new List<CultureInfo>();

            var rm = new ResourceManager(localizedType);

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                if (culture.Equals(CultureInfo.InvariantCulture)) //do not use "==", won't work
                {
                    continue; 
                }
                try
                {
                    if (rm.GetResourceSet(culture, true, false) != null)
                    {
                        result.Add(culture);
                    }
                }
                catch (CultureNotFoundException)
                {
                    // NOP
                }
            }
            return result;
        }
    }
}

