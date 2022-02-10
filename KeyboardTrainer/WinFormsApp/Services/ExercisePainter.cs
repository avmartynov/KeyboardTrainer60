using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Forms.Models;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Services;

public class ExercisePainter : IExercisePainter
{
    private readonly ExerciseAppearance _appearance;

    public ExercisePainter(ExerciseAppearance appearance)
    {
        ThrowIfNull(appearance);

        _appearance = appearance;
    }

    public void Draw(Graphics graphics, Rectangle stageRectangle, IExerciseRun exercise)
    {
        ThrowIfNull(graphics);
        ThrowIfNull(exercise);

        using var brushText = BrushFromHtml(_appearance.TextColor);
        using var brushTextLocal = BrushFromHtml(_appearance.LocalTextColor);

        using var brushHead = BrushFromHtml(_appearance.HeadColor);
        using var brushTail = BrushFromHtml(_appearance.TailColor);

        using var brushCurrent = BrushFromHtml(_appearance.CurrentCharColor);
        using var brushIncorrect = BrushFromHtml(_appearance.IncorrectCharColor);

        using var brushBackground = BrushFromHtml(_appearance.BackgroundColor);

        using var font = new Font(_appearance.FontName, _appearance.FontSize, FontStyle.Regular);

        graphics.FillRectangle(brushBackground, stageRectangle);
        graphics.DrawRectangle(new Pen(Color.DimGray), 0, 0, stageRectangle.Width - 1, stageRectangle.Height - 1);

        var center = new PointF(((float)stageRectangle.Width) / 2, ((float)stageRectangle.Height) / 2);

        var boundBox = GetTestStringSize(exercise.ExerciseString);

        var charLocation = new PointF(center.X - boundBox.Width / 2, center.Y - boundBox.Height / 2);

        for (var idxOfChar = 0; idxOfChar < exercise.ExerciseString.Count; idxOfChar++)
        {
            var keyItem = exercise.ExerciseString[idxOfChar];

            var sizeChar = GetKeyItemSize(keyItem.DisplayText);
            var rect = new RectangleF(charLocation, sizeChar);

            var brush = idxOfChar < exercise.CurrentPosition ? brushHead :
                        idxOfChar > exercise.CurrentPosition ? brushTail :
                        exercise.WrongTyping                 ? brushIncorrect :
                                                               brushCurrent;
            PaintKeyItem(keyItem, rect, brush);

            charLocation.X += sizeChar.Width;
        }

        static Brush BrushFromHtml(string s) => 
            new SolidBrush(ColorTranslator.FromHtml(s));

        void PaintKeyItem(ExerciseItem keyItem, RectangleF rectangle, Brush brushItemBackground)
        {
            var localLetter = (keyItem is LetterItem { IsLocal: true });

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(brushItemBackground, rectangle);
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.DrawString(keyItem.DisplayText, font, localLetter ? brushTextLocal : brushText, rectangle, _format);
        }

        SizeF GetTestStringSize(IEnumerable<ExerciseItem> testString)
        {
            return testString
                .Select(ki => GetKeyItemSize(ki.DisplayText))
                .Aggregate((current, charSize) =>
                    new SizeF
                    {
                        Width = current.Width + charSize.Width,
                        Height = Math.Max(current.Height, charSize.Height)
                    });
        }

        SizeF GetKeyItemSize(string? keyName)
        {
            return TextRenderer.MeasureText(graphics, keyName, font, new Size(),
                keyName?.Length > 1
                    ? TextFormatFlags.Default
                    : TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
        }
    }

    private static readonly StringFormat _format = new()
    {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center,
    };
}

