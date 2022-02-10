using Twidlle.Library.Properties;
using Twidlle.Library.Testing.AppControl;
using Twidlle.Library.Testing.Diagnostics;
using Xunit;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Tests;

[Collection("AllTests")]
public class ApplicationSmokeTest : TestFixture
{
    private const string _exeFilePath = 
        $@"..\..\..\..\WinFormsApp\bin\{ProductInfo.BuildConfiguration}\net6.0-windows\KeyboardTrainer.exe";

    [Fact, Trait("Category", "LongRun")]
    public void Приложение_успешно_стартует() => Invoke(() =>
    {
        var app = new WinAppStartInfo(_exeFilePath);
        app.StartWaitForInputIdleFinish();
    });
}

