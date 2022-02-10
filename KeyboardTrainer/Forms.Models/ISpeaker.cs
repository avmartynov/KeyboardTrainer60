namespace Twidlle.KeyboardTrainer.Forms.Models;

public interface ISpeaker
{
    public void StartSpeak(string languageCode, string text);

    public void Speak(string languageCode, string text);

    public void PlayAsterisk();
}