using Microsoft.Extensions.Logging;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.Library.Utility;

namespace Twidlle.KeyboardTrainer.Core.Services;

/// <summary>
/// Генератор данных для одного выполнения упражнения.
/// </summary>
public class ExerciseGenerator : IExerciseGenerator
{
    private readonly IRandomGenerator _randomGenerator;
    private readonly ILogger _logger;

    public ExerciseGenerator(IRandomGenerator randomGenerator,
                             ILogger<ExerciseGenerator> logger)
    {
        ThrowIfNull(randomGenerator);
        ThrowIfNull(logger);

        _randomGenerator = randomGenerator;
        _logger = logger;
    }

    public IReadOnlyList<ExerciseItem> CreateExercise(WorkoutType workoutType, WorkoutLanguage localLanguage)
    {
        var items = new List<ExerciseItem>();

        while (items.Count < workoutType.ExerciseLength)
        {
            var maxWordLength = workoutType.ExerciseLength - items.Count;
            if (maxWordLength < workoutType.MinWordLength)
            {
                break;
            }

            maxWordLength = Math.Min(maxWordLength, workoutType.MaxWordLength);

            if (items.Any())
            {
                var space = new CharacterItem(' ');
                items.Add(space);
            }

            if (_randomGenerator.Next(100) < workoutType.FunctionalKeysPercent)
            {
                var funcKey = NextFuncItem(_randomGenerator);
                items.Add(funcKey);
            }
            else
            {
                var wordItems = NextWord(localLanguage, workoutType, maxWordLength);
                items.AddRange(wordItems);
            }
        }
        _logger.LogTrace($"Exercise string:{Environment.NewLine} {items.ToJsonIndented()}");

        return items;
    }

    private IEnumerable<ExerciseItem> NextWord(WorkoutLanguage localLanguage, 
                                               WorkoutType workoutType, 
                                               int maxWordLength)
    {
        var isWordLocal       = _randomGenerator.Next(99) < workoutType.LocalWordPercent;
        var isWordCapitalized = _randomGenerator.Next(99) < workoutType.CapitalizedWordPercent;
        var isWordCapsLock    = _randomGenerator.Next(99) < workoutType.CapsLockWordPercent;
        var isWordWithPoint   = _randomGenerator.Next(99) < workoutType.DecimalPointNumberPercent;

        var wordLen = _randomGenerator.Next(workoutType.MinWordLength, maxWordLength + 1);

        var pointPosition = -1;
        if (isWordWithPoint && 3 <= wordLen)
            pointPosition = _randomGenerator.Next(1, wordLen - 2);

        var wordLanguage = isWordLocal ? localLanguage : _englishUs;

        var firstKey = NextCharItem(wordLanguage, 
                                   workoutType, 
                                   localLanguage, 
                                   isWordCapsLock || isWordCapitalized);

        var wordKeys = new List<ExerciseItem> { firstKey };
        var position = 0;

        while (wordKeys.Count < wordLen)
        {
            position++;

            if (position == pointPosition)
            {
                var item = new CharacterItem(workoutType.DecimalPointCharacter[0]);

                wordKeys.Add(item);
            }
            else
            {
                var item = NextCharItem(wordLanguage, 
                                        workoutType, 
                                        localLanguage, 
                                        isWordCapsLock);
                wordKeys.Add(item);
            }
        }
        return wordKeys;
    }


    private ExerciseItem NextCharItem(WorkoutLanguage wordLanguage, 
                                      WorkoutType     workoutType, 
                                      WorkoutLanguage localLanguage,
                                      bool            upperChar)
    {
        var digitBorder       = workoutType.DigitsPercent;
        var punctuationBorder = workoutType.PunctuationPercent   + digitBorder;
        var specSymbolBorder  = workoutType.SpecialSymbolPercent + punctuationBorder;
        var randSymbolBorder  = workoutType.RandomSymbolPercent  + punctuationBorder;

        var randSymbols = wordLanguage.Code == localLanguage.Code 
                            ? workoutType.LocalRandomSymbols 
                            : workoutType.RandomSymbols;

        var typeValue = _randomGenerator.Next(100);
        var ch = typeValue < digitBorder       ?                   DIGITS[_randomGenerator.Next(                  DIGITS.Length)]
               : typeValue < punctuationBorder ? wordLanguage.Punctuation[_randomGenerator.Next(wordLanguage.Punctuation.Length)]
               : typeValue < specSymbolBorder  ?     wordLanguage.Symbols[_randomGenerator.Next(    wordLanguage.Symbols.Length)]
               : typeValue < randSymbolBorder  ?              randSymbols[_randomGenerator.Next(             randSymbols.Length)] 
               :                                     wordLanguage.Letters[_randomGenerator.Next(    wordLanguage.Letters.Length)];
        var isCharLetter = randSymbolBorder <= typeValue;

        if (isCharLetter)
        {
            ch = upperChar ? char.ToUpper(ch) : ch;
            return new LetterItem(ch, wordLanguage.Code == localLanguage.Code, localLanguage.Code);
        }
        return new CharacterItem(ch);
    }


    private static ExerciseItem NextFuncItem(IRandomGenerator randomGenerator)
    {
        var funcKeys    = _functionalKeyNames.Keys.ToArray();
        var key         = funcKeys[randomGenerator.Next(funcKeys.Length)];
        var displayText = _functionalKeyNames[key];

        return new FuncKeyItem(key, displayText);
    }

    private static readonly WorkoutLanguage _englishUs = new();

    private static readonly Dictionary<KeyCode, string> _functionalKeyNames = new()
    {
        { KeyCode.F1,         "F1"       },
        { KeyCode.F2,         "F2"       },
        { KeyCode.F3,         "F3"       },
        { KeyCode.F4,         "F4"       },
        { KeyCode.F5,         "F5"       },
        { KeyCode.F6,         "F6"       },
        { KeyCode.F7,         "F7"       },
        { KeyCode.F8,         "F8"       },
        { KeyCode.F9,         "F9"       },
        { KeyCode.F10,        "F10"      },
        { KeyCode.F11,        "F11"      },
        { KeyCode.F12,        "F12"      },

        { KeyCode.Escape,     "Escape"   },
        { KeyCode.Tab,        "Tab"      },
        { KeyCode.CapsLock,   "CapsLock" },
        { KeyCode.ControlKey, "Ctrl"     },

        { KeyCode.Back,       "Back"     },
        { KeyCode.Enter,      "Enter"    },
        { KeyCode.Apps,       "Menu"     },

        { KeyCode.Insert,     "Insert"   },
        { KeyCode.Delete,     "Delete"   },
        { KeyCode.Home,       "Home"     },
        { KeyCode.End,        "End"      },
        { KeyCode.PageDown,   "PageDown" },
        { KeyCode.PageUp,     "PageUp"   },

        { KeyCode.Scroll,     "Scroll"   },
        { KeyCode.Pause,      "Pause"    },
        { KeyCode.NumLock,    "NumLock"  },

//          { Keys.PrintScreen, "<PrintScreen> " },
//          { Keys.Windows,     "<Windows>"      },
//          { Keys.Alt,         "<Alt>"          },
//          { Keys.Shift,       "<Shift>"        },
    };

    private const string DIGITS = "0123456789";
}


