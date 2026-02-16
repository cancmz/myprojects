using MsPaintProject.Commands;
using System.Drawing;

namespace MsPaintProject.Tools
{
    public class LineTool : IDrawingTool
    {
        private Point startPoint;
        private Point currentPoint;
        private Pen pen;

        public LineTool(Pen pen)
        {
            this.pen = (Pen)pen.Clone();
        }

        public void OnMouseDown(Point p)
        {
            startPoint = p;
            currentPoint = p;
        }

        public void OnMouseMove(Point p)
        {
            currentPoint = p;
        }
        public void UpdatePen(Pen newPen)
        {
            pen = (Pen)newPen.Clone();
        }

        public IDrawCommand OnMouseUp(Point p)
        {
            currentPoint = p;
            return new DrawLineCommand(startPoint, currentPoint, pen);
        }

        public void Preview(Graphics g)
        {
            g.DrawLine(pen, startPoint, currentPoint);
        }
    }
}
