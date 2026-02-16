using System.Collections.Generic;
using System.Drawing;

namespace MsPaintProject.Commands
{
    public class PenStrokeCommand : IDrawCommand
    {
        private List<IDrawCommand> segments = new List<IDrawCommand>();

        public void AddSegment(IDrawCommand cmd)
        {
            segments.Add(cmd);
        }

        public bool IsEmpty => segments.Count == 0;

        public void Execute(Graphics g)
        {
            foreach (var cmd in segments)
                cmd.Execute(g);
        }
    }
}
