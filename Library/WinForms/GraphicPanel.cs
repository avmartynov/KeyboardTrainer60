namespace Twidlle.Library.WinForms;

public class GraphicPanel : Panel
{
    public GraphicPanel()
    {
        this.SetStyle(ControlStyles.UserPaint,             true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint,  true);
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        this.SetStyle(ControlStyles.ResizeRedraw,          true);
        this.UpdateStyles();            
    }
}

