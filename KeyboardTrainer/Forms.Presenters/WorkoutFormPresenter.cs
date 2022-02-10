using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.Library.Utility;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Presenters;

public class WorkoutFormPresenter: SelfScopeDialogPresenter<IWorkoutForm, WorkoutFormModel>, IWorkoutFormPresenter
{
    private readonly IEnumerable<WorkoutType>     _workoutTypes;
    private readonly IEnumerable<WorkoutLanguage> _workoutLanguages;
    private readonly UserSettings           _userSettings;
    private readonly IStringLocalizer       _workoutTypeLocalizer;

    public WorkoutFormPresenter(IWorkoutForm view, 
                                IEnumerable<WorkoutType> workoutTypes,
                                IEnumerable<WorkoutLanguage> workoutLanguages,
                                UserSettings userSettings,
                                IStringLocalizer<WorkoutTypeLocalization> workoutTypeLocalizer,
                                ILogger<WorkoutTypeLocalization> logger) 
        : base(view, logger)
    {
        ThrowIfNull(workoutTypes);
        ThrowIfNull(workoutLanguages);
        ThrowIfNull(userSettings);
        ThrowIfNull(workoutTypeLocalizer);

        _workoutTypes = workoutTypes;
        _workoutLanguages = workoutLanguages;
        _userSettings = userSettings;
        _workoutTypeLocalizer = workoutTypeLocalizer;
    }

    public void ShowDialog(IWorkoutRun workoutRun)
    {
        ThrowIfNull(workoutRun);

        FormView.Model.SelectedWorkoutTypeCode = workoutRun.WorkoutType.Code;
        FormView.Model.WorkoutTypes = _workoutTypes.Select(x =>
        {
            var t = new WorkoutType().CopyFrom(x);
            t.Name = _workoutTypeLocalizer.GetWorkoutTypeName(t);
            t.Description = _workoutTypeLocalizer.GetWorkoutTypeDescription(t).JoinLines();
            return t;
        }).ToArray();

        FormView.Model.SelectedLanguageCode = workoutRun.LocalLanguage.Code;
        FormView.Model.Languages = _workoutLanguages.Where(x => x.Code != "en").ToArray();

        FormView.Accept += () =>
        {
            workoutRun.Initialize(FormView.Model.SelectedWorkoutTypeCode, FormView.Model.SelectedLanguageCode);

            _userSettings.LastWorkoutType = workoutRun.WorkoutType.Code;
            _userSettings.LastLocalWorkoutLanguage = workoutRun.LocalLanguage.Code;
        };
        base.ShowDialog();
    }
}