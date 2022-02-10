using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Core.Model.Properties;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.Library.DependencyInjection;
using Twidlle.Library.Utility;
using Twidlle.Library.VirtualTime;
using Twidlle.Library.WinForms;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Presenters;

public class MainFormPresenter : SelfScopeDialogPresenter<IMainForm, MainFormModel>, IMainFormPresenter
{
    private readonly ApplicationStartInfo _startupInfo;
    private readonly UserSettings _userSettings;
    private readonly ICommonDialogs _commonDialogs;
    private readonly ISpeaker _speaker;
    private readonly ITimeProvider _timeProvider;
    private readonly IWorkoutRun _workoutRun;
    private readonly IStringLocalizer _speechLocalizer;
    private readonly IStringLocalizer _formLocalizer;

    private const string FileExtension = "ktr";

    public MainFormPresenter(IMainForm            form,
                             ApplicationStartInfo startupInfo,
                             UserSettings         userSettings,
                             ICommonDialogs       commonDialogs,
                             ITimeProvider        timeProvider,
                             ISpeaker             speaker,
                             IExercisePainter     painter,
                             IWorkoutRun          workoutRun,
                             IServiceProvider<IAboutFormPresenter>          aboutFormPresenterProvider,
                             IServiceProvider<IWorkoutSummaryFormPresenter> workoutSummaryFormPresenterProvider,
                             IServiceProvider<IPreferencesFormPresenter>    preferencesFormPresenterProvider,
                             IServiceProvider<IWorkoutFormPresenter>        workoutFormPresenterProvider,
                             IStringLocalizer<MainFormPresenter>            formLocalizer,
                             IStringLocalizer<SpeechLocalization>           speechLocalizer,
                             ILogger<MainFormPresenter>                     logger) : 
        base(form, logger)
    {
        ThrowIfNull(form);
        ThrowIfNull(startupInfo);
        ThrowIfNull(userSettings);
        ThrowIfNull(commonDialogs);
        ThrowIfNull(timeProvider);
        ThrowIfNull(speaker);
        ThrowIfNull(painter);
        ThrowIfNull(workoutRun);
        ThrowIfNull(aboutFormPresenterProvider);
        ThrowIfNull(workoutSummaryFormPresenterProvider); 
        ThrowIfNull(preferencesFormPresenterProvider);
        ThrowIfNull(workoutFormPresenterProvider);
        ThrowIfNull(formLocalizer);
        ThrowIfNull(speechLocalizer);

        _startupInfo     = startupInfo;
        _userSettings    = userSettings;
        _commonDialogs   = commonDialogs;
        _timeProvider    = timeProvider;
        _speaker         = speaker;
        _workoutRun      = workoutRun;
        _formLocalizer   = formLocalizer;
        _speechLocalizer = speechLocalizer;

        _workoutRun.NewExercise      += OnNewExercise;
        _workoutRun.NextExerciseStep += OnNextExerciseStep;
        _workoutRun.WorkoutSaved     += OnNextExerciseStep;

        _workoutRun.Initialize(_userSettings.LastWorkoutType, _userSettings.LastLocalWorkoutLanguage);

        FormView.FormShown              += OnFormShown;
        FormView.CloseRequest           += () => !_workoutRun.WorkoutState.Changed || AskAndSaveWorkout();
        FormView.KeyPressed             += keyChar => _workoutRun.OnCharPressed(keyChar);
        FormView.KeyDownSuppressRequest += keyCode => _workoutRun.OnKeyPressed((KeyCode)keyCode);
        FormView.PaintExercise          += (g, r) => painter.Draw(g, r, _workoutRun.ExerciseRun);
        FormView.NewWorkoutFile         += () => OnNewWorkout(workoutFormPresenterProvider);
        FormView.LoadWorkoutFile        += OnLoadWorkout;
        FormView.SaveWorkoutFile        += OnSaveWorkout;
        FormView.SaveWorkoutFileAs      += () => AskPathAndSaveWorkout(_userSettings.LastFile);
        FormView.ShowWorkoutProperties  += () => workoutSummaryFormPresenterProvider.GetService().ShowDialog(_workoutRun);
        FormView.NewExercise            += () => _workoutRun.ResetExercise();
        FormView.ResetWorkout           += () => _workoutRun.ResetState();
        FormView.EditOptions            += () => preferencesFormPresenterProvider.GetService().ShowDialog();
        FormView.NavigateToHomePage     += () => new Uri(_formLocalizer["HelpUrl"]).NavigateTo();
        FormView.ShowAboutForm          += () => aboutFormPresenterProvider.GetService().ShowDialog();
    }

    private void OnFormShown()
    {
        try
        {
            if (!string.IsNullOrEmpty(_startupInfo.WorkoutFilePath))
            {
                LoadWorkout(_startupInfo.WorkoutFilePath);
            }
            else
            {
                if (_userSettings.OpenLastFile)
                {
                    LoadWorkout(_userSettings.LastFile);
                }
                else
                {
                    NewWorkout();
                }
            }
        }
        catch (Exception x)
        {
            x.ShowMessageBox();
            NewWorkout();
        }
    }

    /// <summary> IWorkoutRunView.NewExercise </summary>
    private void OnNewExercise()
    {
        FormView.Model.LastResult = _workoutRun.WorkoutState.LastCharPerMinute.ToString();
        FormView.Model.BestResult = _workoutRun.WorkoutState.BestExerciseCharPerMinute.ToString();
        FormView.Model.ExerciseCount = (_workoutRun.WorkoutState.ExerciseCount + 1).ToString();

        if (_userSettings.UseVoice)
        {
            SpeakNewExercise();
        }

        OnNextExerciseStep();

        FormView.RedrawModel();
    }

    private void OnNextExerciseStep()
    {
        FormView.Model.Title = FormatTitle();

        if (_userSettings.UseVoice)
        {
            SpeakNextExerciseStep();
        }

        FormView.RedrawModel();
    }

    private void OnNewWorkout(IServiceProvider<IWorkoutFormPresenter> workoutFormPresenterProvider)
    {
        if (_workoutRun.WorkoutState.Changed)
        {
            if (!AskAndSaveWorkout())
            {
                return;
            }
        }
        NewWorkout(workoutFormPresenterProvider);
    }

    private void OnLoadWorkout()
    {
        if (_workoutRun.WorkoutState.Changed)
        {
            if (!AskAndSaveWorkout())
            {
                return;
            }
        }
        AskPathAndLoadWorkout();
    }

    private void OnSaveWorkout()
    {
        if (!string.IsNullOrEmpty(_workoutRun.WorkoutState.FilePath))
        {
            _workoutRun.Save();
        }
        else
        {
            var dir = string.IsNullOrEmpty(_userSettings.LastFile)
                ? DefaultFileDirectory
                : Path.GetDirectoryName(_userSettings.LastFile) ?? DefaultFileDirectory;

            var workoutFilePath = Path.Combine(dir, GetDefaultFileName());

            AskPathAndSaveWorkout(workoutFilePath);
        }
    }

    private void LoadWorkout(string? filePath)
    {
        filePath ??= GetDefaultFilePath();

        _workoutRun.Load(filePath);

        _userSettings.LastFile = filePath;

        OnNewExercise();
    }

    private void NewWorkout()
    {
        _workoutRun.Initialize(_userSettings.LastWorkoutType, _userSettings.LastLocalWorkoutLanguage);

        OnNewExercise();
    }

    private void NewWorkout(IServiceProvider<IWorkoutFormPresenter> workoutFormPresenterProvider)
    {
        var presenter = workoutFormPresenterProvider.GetService();
        presenter.ShowDialog(_workoutRun);

        OnNewExercise();
    }

    private bool AskAndSaveWorkout()
    {
        var needSave = _commonDialogs.AskYesNoCancel(_formLocalizer["AskSaveFile"], "");
        if (needSave == null)
        {
            return false;
        }

        if (!needSave.Value)
        {
            return true;
        }

        if (!string.IsNullOrEmpty(_workoutRun.WorkoutState.FilePath))
        {
            _workoutRun.Save();
        }
        else
        {
            AskPathAndSaveWorkout(GetDefaultFileName());
        }
        return true;
    }

    private void AskPathAndSaveWorkout(string? initialPath)
    {
        var initialFilePath = initialPath ?? GetDefaultFilePath();

        if (_commonDialogs.AskForFileToSave(_formLocalizer["FileSaveTitle"], 
                                            initialFilePath,
                                            _formLocalizer["FileFilter"], 
                                            "*.ktr",
                                            out var filePath))
        {
            SaveFile(filePath);
        }
    }

    private void AskPathAndLoadWorkout()
    {
        var initialFilePath = _userSettings.LastFile ?? GetDefaultFilePath();

        if (_commonDialogs.AskForFileToOpen(_formLocalizer["FileOpenTitle"], 
                                            initialFilePath,
                                            _formLocalizer["FileFilter"], 
                                            "*.ktr",
                                            out var filePath))
        {
            LoadWorkout(filePath);
        }
    }

    private void SaveFile(string filePath)
    {
        _userSettings.LastFile = filePath;
        _workoutRun.Save(filePath);
    }

    private void SpeakNewExercise()
    {
        _speaker.Speak(Thread.CurrentThread.CurrentCulture.Name, _formLocalizer["NewExerciseVoiceText"]);
    }

    private void SpeakNextExerciseStep()
    {
        if (_workoutRun.ExerciseRun.WrongTyping)
        {
            _speaker.PlayAsterisk();
        }

        var item = _workoutRun.ExerciseRun.ExerciseString[_workoutRun.ExerciseRun.CurrentPosition];

        var voiceLanguage = (item as LetterItem)?.VoiceLanguage ?? Thread.CurrentThread.CurrentCulture.Name;

        _speaker.StartSpeak(voiceLanguage, _speechLocalizer[item.DisplayText]);
    }

    private static string DefaultFileDirectory
    {
        get
        {
            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // C:\Users\<user>\Documents\Keyboard Workouts
            var dir = Path.Combine(myDocs, "Keyboard Workouts");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return dir;
        }
    }

    private string FormatTitle()
    {
        var running = _workoutRun.ExerciseRun.Running ? "(" + _formLocalizer["RunningState"] + ")" : "";
        var changed = _workoutRun.WorkoutState.Changed ? "*" : "";
        var fileName = Path.GetFileNameWithoutExtension(_workoutRun.WorkoutState.FilePath ?? _formLocalizer["NoNameFileName"]);

        return $"{fileName}{changed} {running} - {ProductInfo.Name}";
    }

    private string GetDefaultFilePath()
    {
        var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var dir = Path.Combine(myDocs, ProductInfo.Name);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        return Path.Combine(dir, GetDefaultFileName());
    }

    private string GetDefaultFileName() =>
        _timeProvider.Now.ToString("yyyy-MM-dd_HHmm." + FileExtension);
}

