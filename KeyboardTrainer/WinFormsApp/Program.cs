using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.KeyboardTrainer.WinFormsApp.Properties;
using Twidlle.Library.DependencyInjection;
using Twidlle.Library.Utility;

namespace Twidlle.KeyboardTrainer.WinFormsApp;

internal static class Program
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    static Program() => 
        Startup.ConfigureNLog();

    [STAThread]
    private static void Main()
    {
        try
        {
            _logger.Info("Start...");

            SetCurrentCulture(Settings.Default.UILanguage);
            ApplicationConfiguration.Initialize();

            Application.ThreadException += (_, e) => e.Exception.Handle();
            TaskScheduler.UnobservedTaskException += (_, e) => e.Exception.HandleFatal();
            AppDomain.CurrentDomain.UnhandledException += (_, e) => ((Exception)e.ExceptionObject).HandleFatal();

            using (var host = new HostBuilder().Configure().Build())
            {
                var mainFormProvider = host.Services.GetRequiredService<IServiceProvider<IMainFormPresenter>>();
                var mainForm = mainFormProvider.GetService();
                mainForm.ShowDialog();

                var userPreferences = host.Services.GetRequiredService<UserSettings>();
                Settings.Default.CopyFrom(userPreferences);
                Settings.Default.Save();
            }

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            _logger.Info($"Successful finish.{Environment.NewLine}");
        }
        catch (Exception e)
        {
            e.HandleFatal();
        }
        finally
        {
            LogManager.Shutdown();
        }
    }

    private static void SetCurrentCulture(string cultureName)
    {
        var cultureInfo = CultureInfo.CreateSpecificCulture(cultureName);

        Application.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }

    /// <summary> Обработка ошибок при обработке оконных сообщений. </summary>
    private static void Handle(this Exception e)
    {
        _logger.Error(e, $"Window message handle error: {Environment.NewLine}");

        Clipboard.SetText(e.ToString());

        var message = string.Format(Resources.UIExceptionMessageFormat, e.Message);
        var result = MessageBox.Show(message,
                                     Application.ProductName,
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Error,
                                     MessageBoxDefaultButton.Button1);
        if (result != DialogResult.Yes)
        {
            Application.Exit();
        }
    }

    /// <summary> Обработка ошибок в стартовом и завершающем коде приложения. </summary>
    private static void HandleFatal(this Exception e)
    {
        _logger.Error(e, $"Fatal error: {Environment.NewLine}");
        _logger.Info($"Finish after fatal error.{Environment.NewLine}");

        Clipboard.SetText(e.ToString());

        var message = string.Format(Resources.FatalExceptionMessageFormat, e.Message);
        MessageBox.Show(message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
    }
}

