namespace Twidlle.Library.WinForms;

/// <summary>
/// Помечает обработчик события как выполняющий длительную работу (что требует отображения курсора ожидания). 
/// </summary>
public sealed class WaitCursor : IDisposable
{
    public WaitCursor() =>
        Cursor.Current = Cursors.WaitCursor;


    public void Dispose() =>
        Cursor.Current = Cursors.Default;
}
