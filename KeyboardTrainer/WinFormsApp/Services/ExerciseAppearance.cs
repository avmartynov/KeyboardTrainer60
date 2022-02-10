namespace Twidlle.KeyboardTrainer.WinFormsApp.Services;

public class ExerciseAppearance
{
    public int    FontSize           { get; init; } = 12;
    public string FontName           { get; init; } = "Verdana";
    public string TextColor          { get; init; } = "#2F4F4F";
    public string BackgroundColor    { get; init; } = "#FFFFFF";
    public string LocalTextColor     { get; init; } = "#2F4F4F";
    public string HeadColor          { get; init; } = "#FFD700";
    public string CurrentCharColor   { get; init; } = "#ADFF2F";
    public string IncorrectCharColor { get; init; } = "#FF0000";
    public string TailColor          { get; init; } = "#FFFFFF";
}
