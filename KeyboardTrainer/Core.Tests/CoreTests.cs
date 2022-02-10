using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Core.Services;
using Twidlle.Library.Testing;
using Twidlle.Library.Testing.Diagnostics;
using Twidlle.Library.Testing.VirtualDateTime;
using Twidlle.Library.VirtualTime;
using Xunit;

namespace Twidlle.KeyboardTrainer.Core.Tests;

[Collection("AllTests")]
public class CoreTests : TestFixture
{
    const string _workoutFilePath1 = "Test1.kbt";
    const string _workoutFilePath2 = "Test2.kbt";

    [Fact, Trait("Category", "Unit")]
    public void _1_Конфигурация_корректно_зачитывается_из_файлов() => Invoke(() =>
    {
        var workoutTypes     = LoadWorkoutTypes().ToList();
        var workoutLanguages = LoadWorkoutLanguages().ToList();

        workoutLanguages.Count.Should().Be(9);
        workoutLanguages.Single(x => x.Code == "uk").Name.Should().Be("Український");
        workoutLanguages.Single(x => x.Code == "ru").Name.Should().Be("Русский");

        workoutTypes.Count.Should().Be(27);
    });

    [Fact, Trait("Category", "Unit")]
    public void _2_Тренировка_по_умолчанию_имеет_корректные_свойства() => Invoke(() =>
    {
        var workoutRun = CreateWorkoutRun();
        workoutRun.Initialize("EngLet", "ru");

        var es = workoutRun.ExerciseRun.ExerciseString;

        workoutRun.WorkoutType.Code.Should().Be("EngLet");
        workoutRun.LocalLanguage.Code.Should().Be("ru");
        es.Select(x => x.DisplayText).Aggregate((a, x) => a + x).Should().Be("vdtmz zci hmliox lskn nepn rhp");
        workoutRun.WorkoutState.ExerciseCount.Should().Be(0);
        workoutRun.WorkoutState.FilePath.Should().Be(null);
    });

    [Fact, Trait("Category", "Unit")]
    public void _3_Сброс_упражнения_порождает_новую_строку_упражнения() => Invoke(() =>
    {
        var workoutRun = CreateWorkoutRun();
        workoutRun.Initialize("EngLet", "ru");

        var es1 = workoutRun.ExerciseRun.ExerciseString.ToList();
        workoutRun.ResetExercise();
        var es2 = workoutRun.ExerciseRun.ExerciseString.ToList();

        es1.Select(x => x.DisplayText).Aggregate((a, x) => a + x).Should().Be("vdtmz zci hmliox lskn nepn rhp");
        es2.Select(x => x.DisplayText).Aggregate((a, x) => a + x).Should().Be("otc lwqx mrczy ledrk nzh dtcbua");
    });

    [Fact, Trait("Category", "Unit")]
    public void _4_Записанный_файл_читается_и_его_путь_запоминается() => Invoke(() =>
    {
        var workoutRun = CreateWorkoutRun();
        workoutRun.Initialize("EngLet", "ru");

        workoutRun.Save(_workoutFilePath1);
        var filePath1 = workoutRun.WorkoutState.FilePath;

        workoutRun.Save(_workoutFilePath2);
        var filePath2 = workoutRun.WorkoutState.FilePath;

        workoutRun.Load(_workoutFilePath1);
        var filePath3 = workoutRun.WorkoutState.FilePath;
        var se3 = workoutRun.ExerciseRun.ExerciseString.ToList();

        filePath1.Should().Be(_workoutFilePath1);
        filePath2.Should().Be(_workoutFilePath2);
        filePath3.Should().Be(_workoutFilePath1);
        se3.Select(x => x.DisplayText).Aggregate((a, x) => a + x).Should().Be("ue jjtur xzbk giybzq am wtyftp");
    });

    [Fact, Trait("Category", "Unit")]
    public void _5_Выполнение_упражнения_без_ошибок_набора_обрабатывается() => Invoke(() =>
    {
        var timeProvider = CreateAutoTimeProvider();
        var workoutRun = CreateWorkoutRun(timeProvider);
        workoutRun.Initialize("EngLet", "ru");

        var s3 = workoutRun.ExerciseRun.ExerciseString.Select(x => x.DisplayText).Aggregate((a, x) => a + x);

        foreach (var c in s3)
        {
            workoutRun.OnCharPressed(c);
            timeProvider.SleepSeconds(0.2);

            workoutRun.ExerciseRun.WrongTyping.Should().Be(false);
        }

        workoutRun.WorkoutState.ExerciseCount.Should().Be(1);
        workoutRun.WorkoutState.LastErrorFraction.Should().Be(0.0);
        workoutRun.WorkoutState.LastCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 20);
    });

    [Fact, Trait("Category", "Unit")]
    public void _6_Выполнение_упражнения_c_ошибками_набора_обрабатывается() => Invoke(() =>
    {
        var timeProvider = CreateAutoTimeProvider();
        var workoutRun = CreateWorkoutRun(timeProvider);
        workoutRun.Initialize("EngLet", "ru");

        var es = workoutRun.ExerciseRun.ExerciseString;

        foreach (var c in es.Select(x => (x as CharacterItem)?.Character ?? throw new InvalidOperationException()))
        {
            if (c != 'i')
                workoutRun.OnCharPressed(c);
            else
            {
                workoutRun.OnCharPressed('a');
                workoutRun.ExerciseRun.WrongTyping.Should().Be(true);
                timeProvider.SleepSeconds(1.0);

                workoutRun.OnCharPressed('i');
            }
            timeProvider.SleepSeconds(0.2);

            workoutRun.ExerciseRun.WrongTyping.Should().Be(false);
        }

        workoutRun.WorkoutState.ExerciseCount.Should().Be(1);
        workoutRun.WorkoutState.LastErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.LastCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);
    });

    [Fact, Trait("Category", "Unit")]
    public void _7_Несколько_выполнений_упражнения_регистрируются_в_статистике() => Invoke(() =>
    {
        var timeProvider = CreateAutoTimeProvider();
        var workoutRun = CreateWorkoutRun(timeProvider);
        workoutRun.Initialize("EngLet", "ru");

        RunExercise(workoutRun, timeProvider);

        workoutRun.WorkoutState.ExerciseCount.Should().Be(1);
        workoutRun.WorkoutState.LastErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.LastCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);

        RunExercise(workoutRun, timeProvider);

        workoutRun.WorkoutState.ExerciseCount.Should().Be(2);
        workoutRun.WorkoutState.LastErrorFraction.Should().Be(0.0);
        workoutRun.WorkoutState.LastCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);
        workoutRun.WorkoutState.AverageErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.AverageCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);

        RunExercise(workoutRun, timeProvider);

        workoutRun.WorkoutState.ExerciseCount.Should().Be(3);
        workoutRun.WorkoutState.LastErrorFraction.Should().Be(0.0);
        workoutRun.WorkoutState.LastCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);
        workoutRun.WorkoutState.AverageErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.AverageCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);

        RunExercise(workoutRun, timeProvider);

        workoutRun.WorkoutState.ExerciseCount.Should().Be(4);
        workoutRun.WorkoutState.LastErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.LastCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);
        workoutRun.WorkoutState.AverageErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.AverageCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);

        workoutRun.Save(_workoutFilePath1);

        workoutRun.ResetState();

        workoutRun.WorkoutState.ExerciseCount.Should().Be(0);
        workoutRun.WorkoutState.LastErrorFraction.Should().Be(0.0);
        workoutRun.WorkoutState.AverageErrorFraction.Should().Be(0.0);
        workoutRun.WorkoutState.BestExerciseErrorFraction.Should().Be(0.0);

        workoutRun.Load(_workoutFilePath1);

        workoutRun.WorkoutState.ExerciseCount.Should().Be(4);
        workoutRun.WorkoutState.LastErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.LastCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);
        workoutRun.WorkoutState.AverageErrorFraction.Should().BeGreaterThan(0.0);
        workoutRun.WorkoutState.AverageCharPerMinute.Should().BeApproximatelyPercent(200, maxDeltaPercent: 30);
    });

    // //////////////////////

    private static IEnumerable<WorkoutType> LoadWorkoutTypes()
    {
        var configBuilder = new ConfigurationBuilder();

        configBuilder.AddJsonFile("KeyboardTrainer.Workouts.json");

        var config = configBuilder.Build();

        return config.GetSection("WorkoutTypes").Get<IList<WorkoutType>>();
    }

    private static IEnumerable<WorkoutLanguage> LoadWorkoutLanguages()
    {
        var configBuilder = new ConfigurationBuilder();

        configBuilder.AddJsonFile("KeyboardTrainer.Languages.json");

        var config = configBuilder.Build();

        return config.GetSection("WorkoutLanguages").Get<IList<WorkoutLanguage>>();
    }

    private static IAutoTimeProvider CreateAutoTimeProvider() =>
        new AutoTimeProvider(new AutoTimeProviderSettings { Start = DateTimeOffset.Now, TimeFlowFactor = 10.0 });

    private static WorkoutRun CreateWorkoutRun(IAutoTimeProvider? autoTimeProvider = null)
    {
        var workoutLanguages = LoadWorkoutLanguages();
        var workoutTypes     = LoadWorkoutTypes();
        var timeProvider     = autoTimeProvider ?? CreateAutoTimeProvider();
        var randomGenerator  = new DeterministicRandomGenerator();

        var exerciseStringLoggerMock = new Mock<ILogger<ExerciseGenerator>>();
        var exerciseString = new ExerciseGenerator(randomGenerator, exerciseStringLoggerMock.Object);
        var exerciseRun = new ExerciseRun(timeProvider, exerciseString);

        return new WorkoutRun(workoutTypes, workoutLanguages, timeProvider, exerciseRun);
    }

    private static void RunExercise(IWorkoutRun workoutRun, IAutoTimeProvider timeProvider)
    {
        var es = workoutRun.ExerciseRun.ExerciseString;

        foreach (var c in es.Select(x => (x as CharacterItem)?.Character ?? throw new InvalidOperationException()))
        {
            if (c != 'i')
                workoutRun.OnCharPressed(c);
            else
            {
                workoutRun.OnCharPressed('a');
                workoutRun.ExerciseRun.WrongTyping.Should().Be(true);
                timeProvider.SleepSeconds(1.0);

                workoutRun.OnCharPressed('i');
            }
            timeProvider.SleepSeconds(0.2);

            workoutRun.ExerciseRun.WrongTyping.Should().Be(false);
        }
    }
}

