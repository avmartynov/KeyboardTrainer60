using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.KeyboardTrainer.Forms.Presenters;
using Twidlle.Library.DependencyInjection;
using Twidlle.Library.WinForms;
using Xunit;

namespace Twidlle.KeyboardTrainer.Forms.Tests;

[Collection("AllTests")]
public class MainFormTests : BaseTestFixture
{
    [Theory]
    [InlineData("ru-RU")]
    [InlineData("en-US")]
    public void Модель_заполнена(string cultureCode) => Invoke(() =>
    {
        SetCurrentCulture(cultureCode);

        var services = new ServiceCollection();
        ConfigureServices(services);

        var clipboardMock = new Mock<IClipboardUtility>();
        services.AddSingleton(clipboardMock.Object);

        var serviceProvider = services.BuildServiceProvider();

        var formPresenter = serviceProvider.GetRequiredService<IMainFormPresenter>();
        formPresenter.ShowDialog();
    });

    protected override void ConfigurePresentationServices(IServiceCollection services)
    {
        base.ConfigurePresentationServices(services);

        services.AddScoped<IMainForm, MainFormMock>();
        services.AddScopedProvider<IMainFormPresenter, MainFormPresenter>();

        services.AddSingleton(new Mock<ApplicationStartInfo>().Object);
        services.AddSingleton(new Mock<UserSettings>().Object);
        services.AddSingleton(new Mock<ICommonDialogs>().Object);
        services.AddSingleton(new Mock<ISpeaker>().Object);
        services.AddSingleton(new Mock<IExercisePainter>().Object);

        services.AddSingleton(new Mock<IServiceProvider<IAboutFormPresenter>>().Object);
        services.AddSingleton(new Mock<IServiceProvider<IWorkoutSummaryFormPresenter>>().Object);
        services.AddSingleton(new Mock<IServiceProvider<IPreferencesFormPresenter>>().Object);
        services.AddSingleton(new Mock<IServiceProvider<IWorkoutFormPresenter>>().Object);
    }
}

internal class MainFormMock : IMainForm
{
    public MainFormModel Model { get; } = new();

    public void ShowDialogForm()
    {
        FormShown?.Invoke();

        var expectedTitle = Thread.CurrentThread.CurrentCulture.Name == "ru-RU"
            ? "[Тренировка без имени]  - Twidlle Keyboard Trainer"
            : "[Noname workout]  - Twidlle Keyboard Trainer";

        Model.Title.Should().Be(expectedTitle);
    }

    public event Action? FormShown;
    public event Func<bool>? CloseRequest { add { } remove { } }
    public event Action<char>? KeyPressed { add { } remove { } }
    public event Func<int, bool>? KeyDownSuppressRequest { add { } remove { } }
    public event Action<Graphics, Rectangle>? PaintExercise { add { } remove { } }
    public event Action? NewWorkoutFile { add { } remove { } }
    public event Action? LoadWorkoutFile { add { } remove { } }
    public event Action? SaveWorkoutFile { add { } remove { } }
    public event Action? SaveWorkoutFileAs { add { } remove { } }
    public event Action? ShowWorkoutProperties { add { } remove { } }
    public event Action? NewExercise { add { } remove { } }
    public event Action? ResetWorkout { add { } remove { } }
    public event Action? EditOptions { add { } remove { } }
    public event Action? NavigateToHomePage { add { } remove { } }
    public event Action? ShowAboutForm { add { } remove { } }

    public void Dispose()
    {
    }
}

