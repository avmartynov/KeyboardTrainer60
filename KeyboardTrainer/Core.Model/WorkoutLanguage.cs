namespace Twidlle.KeyboardTrainer.Core.Model;

public class WorkoutLanguage
{
    public string Code { get; set; } = "en-US";

    public string Name { get; set; } = "English(US)";

    public string Letters { get; set; } = "abcdefghijklmnopqrstuvwxyz";

    public string Punctuation { get; set; } = @".,;:?!`'""-()";

    public string Symbols { get; set; } = @"~@#$%^&*_+=[]{}|\/<>";

    public override string ToString() => 
        Code;
}

