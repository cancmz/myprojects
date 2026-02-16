using System.Collections.Generic;
using System.Drawing;

namespace MsPaintProject.Commands
{
    public class EraseStrokeCommand : IDrawCommand
    {
        private readonly List<Point> points;
        private readonly Pen pen;

        public EraseStrokeCommand(List<Point> points, Pen pen)
        {
            this.points = new List<Point>(points);
            this.pen = (Pen)pen.Clone();
        }

        public void Execute(Graphics g)
        {
            if (points.Count < 2) return;
            using (Pen erasePen = (Pen)pen.Clone())
            {
                erasePen.Color = Color.White;
                g.DrawLines(erasePen, points.ToArray());
            }

        }

    }
}
