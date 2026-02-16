using MsPaintProject.Shapes;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MsPaintProject.Commands
{
    public class DrawShapeCommand : IDrawCommand
    {
        private Rectangle bounds;
        private Pen pen;
        private ShapeType shapeType;

        public DrawShapeCommand(Rectangle bounds, Pen pen, ShapeType shapeType)
        {
            this.bounds = bounds;
            this.pen = (Pen)pen.Clone();
            this.shapeType = shapeType;
        }

        public void Execute(Graphics g)
        {
            switch (shapeType)
            {
                case ShapeType.Rectangle:
                    g.DrawRectangle(pen, bounds);
                    break;

                case ShapeType.Ellipse:
                    g.DrawEllipse(pen, bounds);
                    break;

                case ShapeType.Triangle:
                    DrawTriangle(g);
                    break;

                case ShapeType.Star:
                    DrawStar(g);
                    break;

                case ShapeType.Heart:
                    DrawHeart(g);
                    break;
                case ShapeType.Line:
                    g.DrawLine(
                        pen,
                        bounds.Left,
                        bounds.Top,
                        bounds.Right,
                        bounds.Bottom
                    );
                    break;
            }
        }

        private void DrawTriangle(Graphics g)
        {
            Point top = new Point(bounds.Left + bounds.Width / 2, bounds.Top);
            Point left = new Point(bounds.Left, bounds.Bottom);
            Point right = new Point(bounds.Right, bounds.Bottom);

            g.DrawPolygon(pen, new[] { top, right, left });
        }

        private void DrawStar(Graphics g)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                PointF[] pts = new PointF[10];
                float cx = bounds.Left + bounds.Width / 2f;
                float cy = bounds.Top + bounds.Height / 2f;
                float outerR = bounds.Width / 2f;
                float innerR = outerR / 2.5f;

                for (int i = 0; i < 10; i++)
                {
                    double angle = Math.PI / 5 * i - Math.PI / 2;
                    float r = (i % 2 == 0) ? outerR : innerR;
                    pts[i] = new PointF(
                        cx + (float)(Math.Cos(angle) * r),
                        cy + (float)(Math.Sin(angle) * r)
                    );
                }

                g.DrawPolygon(pen, pts);
            }
        }

        private void DrawHeart(Graphics g)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                float x = bounds.X;
                float y = bounds.Y;
                float w = bounds.Width;
                float h = bounds.Height;

                path.AddBezier(
                    x + w / 2, y + h,
                    x + w * 1.1f, y + h * 0.6f,
                    x + w * 0.8f, y,
                    x + w / 2, y + h * 0.3f
                );

                path.AddBezier(
                    x + w / 2, y + h * 0.3f,
                    x + w * 0.2f, y,
                    x - w * 0.1f, y + h * 0.6f,
                    x + w / 2, y + h
                );

                g.DrawPath(pen, path);
            }
        }
    }
}
