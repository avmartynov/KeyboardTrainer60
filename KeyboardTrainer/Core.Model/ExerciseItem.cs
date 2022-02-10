namespace Twidlle.KeyboardTrainer.Core.Model;

/// <summary>
/// Одна позиция (один элемент) тестовой строки.
/// </summary>
/// <remarks>Immutable class.</remarks>
public abstract class ExerciseItem
{
    /// <summary> Используется двумя способами:
    /// 1. как отображаемый текст и
    /// 2. как имя локализуемой строки для произнесения
    /// Буквы произносятся на языке слова (локальном или en-US) 
    /// Все остальные символы и функциональные клавиши произносятся на языке пользовательского интерфейса.</summary>
    public string DisplayText { get; }

    protected ExerciseItem(string displayText)
    {
        ThrowIfNull(displayText);

        DisplayText = displayText;
    }

    public override string ToString() => 
        DisplayText;
}

/// <summary>
/// Элемент тестовой строки - символ (буква, пунктуация, специальный символ,
/// но не функциональная клавиша).
/// </summary>
/// <remarks>Immutable class.</remarks>
public class CharacterItem : ExerciseItem
{
    public char Character => DisplayText[0];

    public CharacterItem(char c) : base(new string(c, 1)) 
    { }
}

/// <summary>
/// Элемент тестовой строки - буква.
/// </summary>
/// <remarks>Immutable class.</remarks>
public class LetterItem : CharacterItem
{
    public bool IsLocal { get; }

    public string VoiceLanguage { get; protected set; }

    public char Letter => Character;

    public LetterItem(char c, bool isLocal, string voiceLanguage) : base(c)
    {
        IsLocal = isLocal;
        VoiceLanguage = voiceLanguage;
    }

    public override string ToString() =>
        $"{{{base.ToString()}, {IsLocal}, {VoiceLanguage}}}";
}


/// <summary>
/// Элемент тестовой строки - функциональная клавиша.
/// </summary>
/// <remarks>Immutable class.</remarks>
public class FuncKeyItem : ExerciseItem
{
    public KeyCode Key { get; }

    public FuncKeyItem(KeyCode key, string displayText) : base(displayText) =>
        Key = key;

    public override string ToString() => 
        $"{{{base.ToString()}, {Key}}}";
}

