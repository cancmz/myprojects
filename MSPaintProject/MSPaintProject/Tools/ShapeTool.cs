using MsPaintProject.Commands;
using MsPaintProject.Shapes;
using System;
using System.Drawing;

namespace MsPaintProject.Tools
{
    public class ShapeTool : IDrawingTool
    {
        private Point start;
        private Pen pen;
        private ShapeType shapeType;

        public ShapeTool(Pen pen, ShapeType shapeType)
        {
            this.pen = pen;
            this.shapeType = shapeType;
        }
        public ShapeType ShapeType
        {
            get { return shapeType; }
        }
        public void UpdatePen(Pen newPen)
        {
            pen = newPen;
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

            return new DrawShapeCommand(r, pen, shapeType);
        }
    }
}
