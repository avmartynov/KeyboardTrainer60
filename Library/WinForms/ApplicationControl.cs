using Twidlle.Library.WinForms;

namespace Twidlle.Library.WinForms;

public class ApplicationControl : IApplicationControl
{
    public void RestartApplication() =>
        Application.Restart();

    public void ExitApplication() =>
        Application.Exit();
}
