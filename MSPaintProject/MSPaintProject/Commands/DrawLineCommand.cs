using System.Drawing;

namespace MsPaintProject.Commands
{
    public class DrawLineCommand : IDrawCommand
    {
        private Point start, end;
        private Pen pen;

        public DrawLineCommand(Point start, Point end, Pen pen)
        {
            this.start = start;
            this.end = end;
            this.pen = (Pen)pen.Clone();
        }

        public void Execute(Graphics g)
        {
            g.DrawLine(pen, start, end);
        }
    }
}
