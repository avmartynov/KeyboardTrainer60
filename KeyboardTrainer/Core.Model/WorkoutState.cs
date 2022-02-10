using System.Xml.Serialization;

namespace Twidlle.KeyboardTrainer.Core.Model;

public class WorkoutState
{
    public DateTimeOffset StartDateTime { get; init; }

    public DateTimeOffset LastPerformedDateTime { get; init; }

    public int ExerciseCount { get; set; }

    public int BestExerciseCharPerMinute { get; set; }
                  
    public double BestExerciseErrorFraction { get; set; }
                  
    public int  LastCharPerMinute { get; set; }
                  
    public double LastErrorFraction { get; set; }

    public double AverageCharPerMinute { get; set; }

    public double AverageErrorFraction { get; set; }

    [XmlIgnore]
    public string? FilePath { get; set; }

    [XmlIgnore]
    public bool Changed { get; set; }

    public override string ToString() =>
        $"ExerciseCount: {ExerciseCount}, Changed:{Changed}, FilePath:{FilePath}";
}
