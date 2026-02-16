using System.Drawing;

namespace MsPaintProject.Commands
{
    public class DrawTextCommand : IDrawCommand
    {
        private string text;
        private Point location;
        private Font font;
        private Brush brush;

        public DrawTextCommand(string text, Point location, Font font, Brush brush)
        {
            this.text = text;
            this.location = location;
            this.font = font;
            this.brush = brush;
        }

        public void Execute(Graphics g)
        {
            g.DrawString(text, font, brush, location);
        }
    }
}
