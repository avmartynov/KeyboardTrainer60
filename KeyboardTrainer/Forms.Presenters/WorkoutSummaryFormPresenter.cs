using Microsoft.Extensions.Localization;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.Library.WinForms;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Presenters
{
    public class WorkoutSummaryFormPresenter: 
        DialogPresenter<IWorkoutSummaryForm, WorkoutSummaryFormModel>, 
        IWorkoutSummaryFormPresenter
    {
        private readonly IClipboardUtility _clipboard;
        private readonly IStringLocalizer _formLocalizer;
        private readonly IStringLocalizer _workoutTypeLocalizer;

        public WorkoutSummaryFormPresenter(IWorkoutSummaryForm formView,
                                           IClipboardUtility clipboard,
                                           IStringLocalizer<WorkoutSummaryFormPresenter> formLocalizer,
                                           IStringLocalizer<WorkoutTypeLocalization> workoutTypeLocalizer) : 
            base(formView)
        {
            ThrowIfNull(clipboard);
            ThrowIfNull(formLocalizer);
            ThrowIfNull(workoutTypeLocalizer);

            _clipboard = clipboard;
            _formLocalizer = formLocalizer;
            _workoutTypeLocalizer = workoutTypeLocalizer;
        }

        public void ShowDialog(IWorkoutRun workoutRun)
        {
            ThrowIfNull(workoutRun);

            var ci = Thread.CurrentThread.CurrentCulture;

            FormView.Model.BestCharPerMinute = workoutRun.WorkoutState.BestExerciseCharPerMinute.ToString(ci);
            FormView.Model.BestErrors = workoutRun.WorkoutState.BestExerciseErrorFraction.ToString("P", ci);

            FormView.Model.AverageCharPerMinute = workoutRun.WorkoutState.AverageCharPerMinute.ToString(ci);
            FormView.Model.AverageErrors = workoutRun.WorkoutState.AverageErrorFraction.ToString("P", ci);

            FormView.Model.LastCharPerMinute = workoutRun.WorkoutState.LastCharPerMinute.ToString(ci);
            FormView.Model.LastErrors = workoutRun.WorkoutState.LastErrorFraction.ToString("P", ci);

            FormView.Model.WorkoutType = _workoutTypeLocalizer.GetWorkoutTypeName(workoutRun.WorkoutType);
            FormView.Model.Language = workoutRun.LocalLanguage.Name;
            FormView.Model.ExerciseCount = workoutRun.WorkoutState.ExerciseCount.ToString(ci);

            FormView.CopyWorkoutSummary += () => CopyWorkoutSummary(workoutRun);

            base.ShowDialog();
        }

        private void CopyWorkoutSummary(IWorkoutRun workoutRun)
        {
            var workoutTypeName = _workoutTypeLocalizer.GetWorkoutTypeName(workoutRun.WorkoutType);

            var text = string.Format(_formLocalizer["SummaryFormat"],  
                workoutRun.WorkoutState.StartDateTime,
                workoutTypeName,
                workoutRun.LocalLanguage.Name,
                workoutRun.WorkoutState.ExerciseCount,
                workoutRun.WorkoutState.BestExerciseCharPerMinute, workoutRun.WorkoutState.BestExerciseErrorFraction,
                workoutRun.WorkoutState.AverageCharPerMinute,      workoutRun.WorkoutState.AverageErrorFraction,
                workoutRun.WorkoutState.LastCharPerMinute,         workoutRun.WorkoutState.LastErrorFraction,
                workoutRun.WorkoutState.LastPerformedDateTime);

            _clipboard.Copy(text);
        }
    }
}
