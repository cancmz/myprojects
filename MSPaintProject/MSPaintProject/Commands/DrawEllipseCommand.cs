using System.Drawing;

namespace MsPaintProject.Commands
{
    public class DrawEllipseCommand : IDrawCommand
    {
        private Rectangle rect;
        private Pen pen;

        public DrawEllipseCommand(Rectangle rect, Pen pen)
        {
            this.rect = rect;
            this.pen = (Pen)pen.Clone();
        }

        public void Execute(Graphics g)
        {
            g.DrawEllipse(pen, rect);
        }
    }
}
