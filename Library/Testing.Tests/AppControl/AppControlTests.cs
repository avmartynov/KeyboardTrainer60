using System.Reflection;
using Twidlle.Library.Testing.AppControl;
using Twidlle.Library.Testing.Diagnostics;
using Xunit;

namespace Library.Testing.Tests.AppControl;

[Collection("AllTests")]
public class WinAppControlTests : TestFixture
{
    private static readonly string _exeFilePath = $@"..\..\..\..\Testing.SampleApp\bin\Debug\net6.0-windows\Twidlle.Library.Testing.SampleApp.exe";

    [Fact, Trait("Category", "LongRun")]
    public void Тестовое_приложение_успешно_стартует() => Invoke(() =>
    {
        var app = new WinAppStartInfo(_exeFilePath);

        app.StartWaitForInputIdleFinish();
    });

    [Fact, Trait("Category", "LongRun")]
    public void Ошибка_при_старте_приложения_обнаружена() => Invoke(() =>
    {
        var app = new WinAppStartInfo(_exeFilePath) { Arguments = "1" };

        try
        {
            app.StartWaitForInputIdleFinish();
        }
        catch 
        {
            return;
        }

        Assert.True(false);
    });
}

