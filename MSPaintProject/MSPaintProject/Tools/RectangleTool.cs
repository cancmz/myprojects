using MsPaintProject.Commands;
using System;
using System.Drawing;

namespace MsPaintProject.Tools
{
    public class RectangleTool : IDrawingTool
    {
        private Point start;
        private Pen pen;

        public RectangleTool(Pen pen)
        {
            this.pen = pen;
        }

        public void OnMouseDown(Point p)
        {
            start = p;
        }

        public void OnMouseMove(Point p) { }

        public IDrawCommand OnMouseUp(Point p)
        {
            Rectangle r = new Rectangle(
                Math.Min(start.X, p.X),
                Math.Min(start.Y, p.Y),
                Math.Abs(start.X - p.X),
                Math.Abs(start.Y - p.Y)
            );
            return new DrawRectangleCommand(r, pen);
        }
    }
}
