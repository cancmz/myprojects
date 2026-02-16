using System.Drawing;

namespace MsPaintProject.Commands
{
    public interface IDrawCommand
    {
        void Execute(Graphics g);
    }
}
