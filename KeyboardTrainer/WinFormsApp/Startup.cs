using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Core.Services;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.KeyboardTrainer.Forms.Presenters;
using Twidlle.KeyboardTrainer.WinFormsApp.Forms;
using Twidlle.KeyboardTrainer.WinFormsApp.Properties;
using Twidlle.KeyboardTrainer.WinFormsApp.Services;
using Twidlle.Library.DependencyInjection;
using Twidlle.Library.Hosting;
using Twidlle.Library.Utility;
using Twidlle.Library.VirtualTime;
using Twidlle.Library.WinForms;

namespace Twidlle.KeyboardTrainer.WinFormsApp;

internal static class Startup
{
    private static readonly string _appAssemblyName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);

    public static void ConfigureNLog() =>
        NLogExtensions.Configure($"{_appAssemblyName}.Logging.json");

    public static IHostBuilder Configure(this IHostBuilder host)
    {
        ThrowIfNull(host);

        host.ConfigureHostConfiguration(ConfigureHostConfiguration);
        host.ConfigureAppConfiguration(ConfigureAppConfiguration);
        host.ConfigureServices(ConfigureServices);

        return host;
    }

    private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        ThrowIfNull(host);
        ThrowIfNull(services);
        
        services.AddLogging(x => x.ClearProviders().SetMinimumLevel(LogLevel.Trace).AddNLog());
        services.ConfigureCoreServices(host);
        services.ConfigurePresentationServices(host);
    }

    private static void ConfigureHostConfiguration(IConfigurationBuilder config)
    {
        ThrowIfNull(config);

        config.AddEnvironmentVariables($"{_appAssemblyName}_");
    }

    private static void ConfigureAppConfiguration(HostBuilderContext host, IConfigurationBuilder builder)
    {
        ThrowIfNull(host);
        ThrowIfNull(builder);

        var envName = host.HostingEnvironment.EnvironmentName;

        builder.AddJsonFile($"{_appAssemblyName}.Workouts.json");
        builder.AddJsonFile($"{_appAssemblyName}.Workouts.{envName}.json", optional: true);

        builder.AddJsonFile($"{_appAssemblyName}.Languages.json");
        builder.AddJsonFile($"{_appAssemblyName}.Languages.{envName}.json", optional: true);

        builder.AddJsonFile($"{_appAssemblyName}.Appearance.json");
        builder.AddJsonFile($"{_appAssemblyName}.Appearance.{envName}.json", optional: true);

        builder.AddCommandLineArg();
    }

    private static void ConfigureCoreServices(this IServiceCollection services, HostBuilderContext host)
    {
        services.AddSingleton<IEnumerable<WorkoutType>>    (host.Configuration, sectionName: "WorkoutTypes");
        services.AddSingleton<IEnumerable<WorkoutLanguage>>(host.Configuration, sectionName: "WorkoutLanguages");

        services.AddSingleton<ITimeProvider, NaturalTimeProvider>();
        services.AddSingleton<IRandomGenerator, PseudoRandomGenerator>();

        services.AddScoped<IWorkoutRun, WorkoutRun>();
        services.AddScoped<IExerciseRun, ExerciseRun>();
        services.AddScoped<IExerciseGenerator, ExerciseGenerator>();
    }

    private static void ConfigurePresentationServices(this IServiceCollection services, HostBuilderContext host)
    {
        services.AddSingleton<ExerciseAppearance>  (host.Configuration);
        services.AddSingleton<ApplicationStartInfo>(host.Configuration);

        services.AddLocalization();
        services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
        services.AddSingleton<IApplicationControl, ApplicationControl>();
        services.AddSingleton<ICommonDialogs, CommonDialogs>();
        services.AddSingleton<IClipboardUtility, ClipboardUtility>();
        services.AddSingleton<ISpeaker, Speaker>();
        services.AddSingleton<IExercisePainter, ExercisePainter>();
        services.AddSingleton(new UserSettings().CopyFrom(Settings.Default));

        services.AddScoped<IAboutForm, AboutForm>();
        services.AddSelfScopeProvider<IAboutFormPresenter, AboutFormPresenter>();

        services.AddScoped<IWorkoutForm, WorkoutForm>();
        services.AddSelfScopeProvider<IWorkoutFormPresenter, WorkoutFormPresenter>();

        services.AddScoped<IWorkoutSummaryForm, WorkoutSummaryForm>();
        services.AddScopedProvider<IWorkoutSummaryFormPresenter, WorkoutSummaryFormPresenter>();

        services.AddScoped<IPreferencesForm, PreferencesForm>();
        services.AddSelfScopeProvider<IPreferencesFormPresenter, PreferencesFormPresenter>();

        services.AddScoped<IMainForm, MainForm>();
        services.AddSelfScopeProvider<IMainFormPresenter, MainFormPresenter>();
    }

    private static void AddCommandLineArg(this IConfigurationBuilder builder)
    {
        const string key = $"{nameof(ApplicationStartInfo)}:{nameof(ApplicationStartInfo.WorkoutFilePath)}";
        var val = Environment.GetCommandLineArgs().Skip(1).FirstOrDefault() ?? "";

        builder.AddInMemoryCollection(new[] { new KeyValuePair<string, string>(key, val) }); 
    }
}