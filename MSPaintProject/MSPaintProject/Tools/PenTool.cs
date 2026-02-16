using MsPaintProject.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MsPaintProject.Tools
{
    public class PenTool : IDrawingTool
    {
        private List<DrawLineCommand> previewSegments = new List<DrawLineCommand>();
        private Point lastPoint;
        private Pen pen;
        private PenStrokeCommand currentStroke;
        private const int MinDistance = 2;

        public PenTool(Pen pen)
        {
            this.pen = pen;
        }

        public void OnMouseDown(Point p)
        {
            lastPoint = p;
            currentStroke = new PenStrokeCommand();
        }

        public void OnMouseMove(Point p)
        {
            if (Math.Abs(p.X - lastPoint.X) < MinDistance &&
                Math.Abs(p.Y - lastPoint.Y) < MinDistance)
                return;
            DrawLineCommand segment =
                new DrawLineCommand(lastPoint, p, pen);

            currentStroke.AddSegment(segment);
            previewSegments.Add(segment);

            lastPoint = p;
        }
        public void Preview(Graphics g)
        {
            foreach (var seg in previewSegments)
                seg.Execute(g);
        }


        public IDrawCommand OnMouseUp(Point p)
        {
            if (currentStroke.IsEmpty)
                return null;

            previewSegments.Clear();
            return currentStroke;
        }

    }
}
