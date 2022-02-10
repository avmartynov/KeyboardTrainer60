using Twidlle.Library.WinForms;

namespace Twidlle.Library.WinForms;

public class ClipboardUtility : IClipboardUtility
{
    public void Copy(string text) =>
        Clipboard.SetText(text);
}