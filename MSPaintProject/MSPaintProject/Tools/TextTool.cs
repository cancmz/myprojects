using Microsoft.VisualBasic;
using MsPaintProject.Commands;
using MsPaintProject.Tools;
using System.Drawing;

public class TextTool : IDrawingTool
{
    private Brush brush;
    private Font font = new Font("Arial", 12);

    public TextTool(Color color)
    {
        brush = new SolidBrush(color);
    }

    public void OnMouseDown(Point p) { }
    public void OnMouseMove(Point p) { }

    public IDrawCommand OnMouseUp(Point p)
    {
        string text = Interaction.InputBox("Metni giriniz:", "Metin");
        if (string.IsNullOrWhiteSpace(text)) return null;

        return new DrawTextCommand(text, p, font, brush);
    }
}
