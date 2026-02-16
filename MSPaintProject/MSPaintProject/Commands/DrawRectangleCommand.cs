using System.Drawing;

namespace MsPaintProject.Commands
{
    public class DrawRectangleCommand : IDrawCommand
    {
        private Rectangle rect;
        private Pen pen;

        public DrawRectangleCommand(Rectangle rect, Pen pen)
        {
            this.rect = rect;
            this.pen = (Pen)pen.Clone();
        }

        public void Execute(Graphics g)
        {
            g.DrawRectangle(pen, rect);
        }
    }
}
