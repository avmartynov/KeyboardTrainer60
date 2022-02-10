namespace Twidlle.KeyboardTrainer.Core.Model;

public class WorkoutType
{
    /// <summary> Идентификатор типа тренировки </summary>
    public string Code { get; set; } = "EngLet";

    /// <summary> Имя плана тренировки </summary>
    public string Name { get; set; } = "English letters";

    /// <summary> Подробное описание плана тренировки </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary> Длина одного упражнения (тестового текста). </summary>
    public int ExerciseLength { get; set; } = 30;

    /// <summary> Минимальная длина слова (1 или больше)</summary>
    public int MinWordLength { get; set; } = 2;

    /// <summary> Максимальная длина слова </summary>
    public int MaxWordLength { get; set; } = 6;


    // Особые символы

    /// <summary> Символ десятичной точки </summary>
    public string DecimalPointCharacter { get; set; } = ".";

    /// <summary> Произвольный набор символов </summary>
    public string RandomSymbols { get; set; } = string.Empty;

    /// <summary> Произвольный набор локальных символов </summary>
    public string LocalRandomSymbols { get; set; } = string.Empty;


    // Параметры слова

    /// <summary> Процент слов в местной раскладке </summary>
    public int LocalWordPercent { get; set; } = 0;

    /// <summary> Процент слов с заглавной буквы </summary>
    public int CapitalizedWordPercent { get; set; } = 0;

    /// <summary> Процент слов, набранных в режиме CapsLock среди слов не с заглавной буквы </summary>
    public int CapsLockWordPercent { get; set; } = 0;

    /// <summary> Процент чисел с десятичной точкой </summary>
    public int DecimalPointNumberPercent { get; set; } = 0;

    /// <summary> Процент слов-управляющих и функциональных клавиш </summary>
    public int FunctionalKeysPercent { get; set; } = 0;


    // Состав слов (Буквы, цифры, пунктуация, спецсимволы)

    /// <summary> Доля цифр </summary>
    public int DigitsPercent { get; set; } = 0;

    /// <summary> Доля знаков препинания </summary>
    public int PunctuationPercent { get; set; } = 0;

    /// <summary> Доля спец-символов </summary>
    public int SpecialSymbolPercent { get; set; } = 0;

    /// <summary>
    /// Доля символов из произвольного множества
    /// (т.е. из RandomSymbols или из LocalRandomSymbols)
    /// </summary>
    public int RandomSymbolPercent { get; set; } = 0;

    public override string ToString() => 
        Code;
}

