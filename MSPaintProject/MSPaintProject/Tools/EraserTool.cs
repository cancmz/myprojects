using MsPaintProject.Commands;
using System.Collections.Generic;
using System.Drawing;

namespace MsPaintProject.Tools
{
    public class EraserTool : IDrawingTool
    {
        private Pen eraserPen;
        private readonly List<Point> points = new List<Point>();

        public EraserTool(Pen basePen)
        {
            eraserPen = (Pen)basePen.Clone();
            eraserPen.Color = Color.White;
            eraserPen.Width = basePen.Width * 3.5f;
        }

        public void OnMouseDown(Point p)
        {
            points.Clear();
            points.Add(p);
        }

        public void OnMouseMove(Point p)
        {
            points.Add(p);
        }

        public IDrawCommand OnMouseUp(Point p)
        {
            if (points.Count < 2)
                return null;

            return new EraseStrokeCommand(points, eraserPen);
        }
        public void UpdatePen(Pen basePen)
        {
            eraserPen = (Pen)basePen.Clone();
            eraserPen.Color = Color.White;
            eraserPen.Width = basePen.Width * 3.5f;
        }

        public void Preview(Graphics g)
        {
            if (points.Count < 2) return;
            g.DrawLines(eraserPen, points.ToArray());
        }
    }
}
