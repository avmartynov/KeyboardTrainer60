namespace Twidlle.KeyboardTrainer.Forms.Models;

public class UserSettings
{
    public string UILanguage { get; set; } = "en-US";

    public bool OpenLastFile { get; set; }
    public bool UseVoice { get; set; }

    public string? LastFile { get; set; }
    public string  LastLocalWorkoutLanguage { get; set; } = "en-US";
    public string  LastWorkoutType { get; set; } = "EngLet";
}
