using System.Drawing;
using MsPaintProject.Commands;

namespace MsPaintProject.Tools
{
    public interface IDrawingTool
    {
        void OnMouseDown(Point p);
        void OnMouseMove(Point p);
        IDrawCommand OnMouseUp(Point p);
    }
}
