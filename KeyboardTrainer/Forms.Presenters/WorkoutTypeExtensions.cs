using Microsoft.Extensions.Localization;
using Twidlle.KeyboardTrainer.Core.Model;

namespace Twidlle.KeyboardTrainer.Forms.Presenters;

public static class WorkoutTypeExtensions
{
    public static string GetWorkoutTypeName(this IStringLocalizer localizer, WorkoutType workoutType)
    {
        var localized = localizer[$"{workoutType.Code}.{nameof(WorkoutType.Name)}"];
        return localized.ResourceNotFound ? workoutType.Name : localized;
    }

    public static string GetWorkoutTypeDescription(this IStringLocalizer localizer, WorkoutType workoutType)
    {
        var localized = localizer[$"{workoutType.Code}.{nameof(WorkoutType.Description)}"];
        return localized.ResourceNotFound ? workoutType.Description : localized;
    }
}