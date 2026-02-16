using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MsPaintProject.Commands;

namespace MsPaintProject.Managers
{
    public class CommandManager
    {
        private Stack<IDrawCommand> undoStack = new Stack<IDrawCommand>();
        private Stack<IDrawCommand> redoStack = new Stack<IDrawCommand>();

        public void Execute(IDrawCommand cmd, Graphics g)
        {
            cmd.Execute(g);
            undoStack.Push(cmd);
            redoStack.Clear();
        }

        public void Undo(Graphics g)
        {
            if (undoStack.Count == 0) return;
            redoStack.Push(undoStack.Pop());
            Redraw(g);
        }

        public void Redo(Graphics g)
        {
            if (redoStack.Count == 0) return;
            undoStack.Push(redoStack.Pop());
            Redraw(g);
        }

        private void Redraw(Graphics g)
        {
            g.Clear(Color.White);
            foreach (var cmd in undoStack.Reverse())
                cmd.Execute(g);
        }
    }
}
