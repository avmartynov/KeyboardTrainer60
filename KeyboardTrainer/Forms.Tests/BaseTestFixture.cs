using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Core.Services;
using Twidlle.Library.DependencyInjection;
using Twidlle.Library.Testing;
using Twidlle.Library.Testing.Diagnostics;
using Twidlle.Library.Testing.VirtualDateTime;
using Twidlle.Library.Utility;
using Twidlle.Library.VirtualTime;

namespace Twidlle.KeyboardTrainer.Forms.Tests
{
    public class BaseTestFixture : TestFixture
    {
        private const string _startTimeInstant = "2022-05-02 21:43:05 +03:00";

        protected static void SetCurrentCulture(string cultureCode)
        {
            var cultureInfo = CultureInfo.CreateSpecificCulture(cultureCode);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("KeyboardTrainer.Workouts.json");
            builder.AddJsonFile("KeyboardTrainer.Languages.json");

            return builder.Build();
        }

        private static void ConfigureCoreServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IEnumerable<WorkoutType>>(config, sectionName: "WorkoutTypes");
            services.AddSingleton<IEnumerable<WorkoutLanguage>>(config, sectionName: "WorkoutLanguages");

            var timeProvider = new AutoTimeProvider(new AutoTimeProviderSettings
            {
                Start = DateTimeOffset.Parse(_startTimeInstant, CultureInfo.InvariantCulture),
                TimeFlowFactor = 10.0
            });
            services.AddSingleton<ITimeProvider>(timeProvider);
            services.AddSingleton<IRandomGenerator, DeterministicRandomGenerator>();

            services.AddScoped<IWorkoutRun, WorkoutRun>();
            services.AddScoped<IExerciseRun, ExerciseRun>();
            services.AddScoped<IExerciseGenerator, ExerciseGenerator>();
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            ThrowIfNull(services);

            var configuration = BuildConfiguration();

            services.AddLogging(x => x.ClearProviders().AddNLog(configuration));

            ConfigureCoreServices(services, configuration);

            ConfigurePresentationServices(services);
        }

        protected virtual void ConfigurePresentationServices(IServiceCollection services)
        {
            services.AddLocalization();
            services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
        }
    }
}
