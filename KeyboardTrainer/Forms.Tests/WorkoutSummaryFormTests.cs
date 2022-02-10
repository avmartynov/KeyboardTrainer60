using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.KeyboardTrainer.Forms.Presenters;
using Twidlle.Library.DependencyInjection;
using Twidlle.Library.WinForms;
using Xunit;

namespace Twidlle.KeyboardTrainer.Forms.Tests;

[Collection("AllTests")]
public class WorkoutSummaryFormTests : BaseTestFixture
{
    [Theory] 
    [InlineData("ru-RU")]
    [InlineData("en-US")]
    public void Модель_данных_формы_заполнена_Команда_Copy_работает(string cultureCode) => Invoke(() =>
    {
        const string expectedSummaryRu = @"Тренировка KeyboardTrainer
--------------------------------------------------------
                Дата тренировки: 2022-05-02 21:43 +03:00 - 2022-05-02 21:43 +03:00
                План тренировки: Буквы английского языка
                   Местный язык: Русский
           Выполнено упражнений: 0
   Результат лучшего упражнения: 0 симв./мин, 0,00 % ошибок
 Результат упражнения в среднем:  симв./мин, 0,00 % ошибок
Результат последнего упражнения: 0 симв./мин, 0,00 % ошибок";

        const string expectedSummaryEn = @"KeyboardTrainer workout
----------------------------------------------------------
               Workout date: 2022-05-02 21:43 +03:00 - 2022-05-02 21:43 +03:00
               Workout plan: English letters
             Local language: Русский
         Executed exercises: 0
    Best result of exercise: 0 char/min, 0.000% errors
Average result of exercises:  char/min, 0.000% errors
    Result of last exercise: 0 char/min, 0.000% errors";

        SetCurrentCulture(cultureCode);

        var services = new ServiceCollection();
        ConfigureServices(services);

        var clipboardMock = new Mock<IClipboardUtility>();
        services.AddSingleton(clipboardMock.Object);

        var serviceProvider = services.BuildServiceProvider();

        var formPresenter = serviceProvider.GetRequiredService<IWorkoutSummaryFormPresenter>();
        var workoutRun = serviceProvider.GetRequiredService<IWorkoutRun>();
        workoutRun.Initialize("EngLet", "ru");
        formPresenter.ShowDialog(workoutRun);


        var expectedSummary = Thread.CurrentThread.CurrentCulture.Name == "ru-RU" ? expectedSummaryRu : expectedSummaryEn;
        clipboardMock.Verify(x => x.Copy(expectedSummary), Times.Once);
    });


    protected override void ConfigurePresentationServices(IServiceCollection services)
    {
        base.ConfigurePresentationServices(services);

        services.AddScoped<IWorkoutSummaryForm, WorkoutSummaryFormMock>();
        services.AddScopedProvider<IWorkoutSummaryFormPresenter, WorkoutSummaryFormPresenter>();
    }
}

internal class WorkoutSummaryFormMock : IWorkoutSummaryForm
{
    public WorkoutSummaryFormModel Model { get; } = new();

    public event Action? CopyWorkoutSummary;

    public void ShowDialogForm()
    {
        Model.WorkoutType.Should().Be(Thread.CurrentThread.CurrentCulture.Name == "ru-RU" ? "Буквы английского языка" : "English letters");
        Model.Language.Should().Be("Русский");
        Model.ExerciseCount.Should().Be("0");

        CopyWorkoutSummary?.Invoke();
    }

    public void Dispose()
    {
    }
}

