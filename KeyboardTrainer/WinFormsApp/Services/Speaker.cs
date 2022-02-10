using System.Media;
using System.Speech.Synthesis;
using Twidlle.KeyboardTrainer.Forms.Models;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Services;

public class Speaker : ISpeaker
{
    private readonly SpeechSynthesizer _synthesizer = new();

    public void StartSpeak(string languageCode, string text)
    {
        SpeakSsml(languageCode, text, async: true);
    }

    public void Speak(string languageCode, string text)
    {
        SpeakSsml(languageCode, text);
    }


    public void PlayAsterisk()
    {
        _synthesizer.SpeakAsyncCancelAll();
        SystemSounds.Asterisk.Play();
    }


    private void SpeakSsml(string  languageCode, string content, bool async = false)
    {
        var ssml = "<speak version=\"1.0\" " +
                          "xmlns=\"http://www.w3.org/2001/10/synthesis\" " +
                          "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                          "xsi:schemaLocation=\"http://www.w3.org/2001/10/synthesis " +
                          "http://www.w3.org/TR/speech-synthesis/synthesis.xsd\" " +
                         $"xml:lang=\"{languageCode}\" >" +
                        $"{content}" +
                   "</speak>";

        _synthesizer.SpeakAsyncCancelAll();

        if (async)
        {
            _synthesizer.SpeakSsmlAsync(ssml);
        }
        else
        {
            _synthesizer.SpeakSsml(ssml);
        }
    }
}
