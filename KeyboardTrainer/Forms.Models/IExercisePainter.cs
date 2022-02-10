using System.Drawing;
using Twidlle.KeyboardTrainer.Core.Model;

namespace Twidlle.KeyboardTrainer.Forms.Models;

/// <summary>
/// Отображение (рисовка) состояния исполнения упражнения.
/// </summary>
public interface IExercisePainter
{
    void Draw(Graphics graphics, Rectangle stageRectangle, IExerciseRun exercise);
}