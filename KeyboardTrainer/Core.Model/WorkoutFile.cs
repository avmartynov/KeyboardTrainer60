using System.Xml.Serialization;

namespace Twidlle.KeyboardTrainer.Core.Model;

[XmlRoot("Workout")]
public class WorkoutFile
{
    public string WorkoutTypeCode { get; set; } = "English letters";

    public string LocalLanguageCode { get; set; } = "en-US";

    public WorkoutState WorkoutState { get; set; } = new WorkoutState();
}
