namespace Twidlle.Library.Testing.AppControl;

public static class WinAppControl
{
    /// <summary>
    /// Стартует приложение,
    /// дожидается, когда приложение перейдёт в состояние ожидания,
    /// находится в состоянии ожидания заданное число секунд и
    /// завершает приложение.
    /// </summary>
    public static void StartWaitForInputIdleFinish(this WinAppStartInfo startInto, int waitAfterIdleSeconds = 0)
    {
        using var appControl = new WinAppController(startInto);
        appControl.StartWaitForInputIdle();
        Thread.Sleep(TimeSpan.FromSeconds(waitAfterIdleSeconds));
    }
}
