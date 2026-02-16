using MsPaintProject.Commands;
using MsPaintProject.Managers;
using MsPaintProject.Shapes;
using MsPaintProject.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MsPaintProject
{
    public partial class Form1 : Form
    {
        Bitmap canvasBitmap;
        Graphics canvasGraphics;
        Bitmap previewBitmap;
        Graphics previewGraphics;
        IDrawingTool currentTool;
        CommandManager commandManager = new CommandManager();
        Pen currentPen = new Pen(Color.Black, 2);
        bool isDrawingShape = false;
        Point shapeStartPoint;
        DateTime lastRenderTime = DateTime.MinValue;
        const int RenderIntervalMs = 16;
        Rectangle lastSizeRect = Rectangle.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            lblThickness.Text = $"{trackBarThickness.Value} px";
        }

        public Form1()
        {
  
            InitializeComponent();

            canvasBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            previewBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            canvasGraphics = Graphics.FromImage(canvasBitmap);
            previewGraphics = Graphics.FromImage(previewBitmap);

            canvasGraphics.Clear(Color.White);
            previewGraphics.Clear(Color.Transparent);

            pictureBox1.Image = canvasBitmap;

            currentTool = new PenTool(currentPen);

            pictureBox1.MouseDown += Canvas_MouseDown;
            pictureBox1.MouseMove += Canvas_MouseMove;
            pictureBox1.MouseUp += Canvas_MouseUp;

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.DoubleBuffered = true;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                commandManager.Undo(canvasGraphics);
                pictureBox1.Refresh();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                commandManager.Redo(canvasGraphics);
                pictureBox1.Refresh();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        private Pen CreatePen(Color color, float width)
        {
            Pen pen = new Pen(color, width);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            return pen;
        }
        void Canvas_MouseDown(object s, MouseEventArgs e)
        {
            shapeStartPoint = e.Location;
            isDrawingShape = true;
            currentTool.OnMouseDown(e.Location);
        }
        private Rectangle CreateRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y)
            );
        }
        private void DrawSizeInfo(Graphics g, Rectangle r, Point mousePos)
        {
            string text = $"{r.Width} x {r.Height}";

            Font font = new Font("Arial", 9);
            SizeF size = g.MeasureString(text, font);

            int padding = 4;
            RectangleF bg = new RectangleF(
                mousePos.X + 10,
                mousePos.Y - size.Height - 10,
                size.Width + padding * 2,
                size.Height + padding * 2
            );

            using (Brush bgBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
            using (Pen borderPen = new Pen(Color.Black))
            {
                g.FillRectangle(bgBrush, bg);
                g.DrawRectangle(borderPen, Rectangle.Round(bg));
                g.DrawString(text, font, Brushes.Black,
                    bg.X + padding, bg.Y + padding);
            }
        }
        private Cursor CreateEraserCursor(int size)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (Pen p = new Pen(Color.Black, 2))
                {
                    g.DrawEllipse(p, 1, 1, size - 3, size - 3);
                }
            }

            return new Cursor(bmp.GetHicon());
        }

        void Canvas_MouseMove(object s, MouseEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastRenderTime).TotalMilliseconds < RenderIntervalMs)
                return;

            lastRenderTime = now;
            if (e.Button != MouseButtons.Left)
                return;

            if (currentTool is ShapeTool)
            {
                previewGraphics.Clear(Color.Transparent);
            }


            if (currentTool is PenTool penTool)
            {
                penTool.OnMouseMove(e.Location);
                penTool.Preview(previewGraphics);
                pictureBox1.Invalidate();
                return;
            }

            if (currentTool is EraserTool eraserTool)
            {
                eraserTool.OnMouseMove(e.Location);
                eraserTool.Preview(previewGraphics);
                pictureBox1.Invalidate();
                return;
            }
            if (currentTool is ShapeTool shapeTool)
            {
                Rectangle r = CreateRectangle(shapeStartPoint, e.Location);

                using (Pen previewPen = CreatePen(currentPen.Color, currentPen.Width))
                {
                    previewPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    DrawShapeCommand previewCmd =
                        new DrawShapeCommand(r, previewPen, shapeTool.ShapeType);

                    previewCmd.Execute(previewGraphics);
                    if (r != lastSizeRect)
                    {
                        DrawSizeInfo(previewGraphics, r, e.Location);
                        lastSizeRect = r;
                    }
                }

                pictureBox1.Invalidate();
            }
            else if (currentTool is LineTool lineTool)
            {
                lineTool.OnMouseMove(e.Location);
                previewGraphics.Clear(Color.Transparent);
                lineTool.Preview(previewGraphics);
                pictureBox1.Invalidate();
                return;
            }

        }

        void Canvas_MouseUp(object s, MouseEventArgs e)
        {

            previewGraphics.Clear(Color.Transparent);
            if (currentTool is ShapeTool shapeTool)
            {
                shapeTool.UpdatePen(currentPen);
            }
            IDrawCommand cmd = currentTool.OnMouseUp(e.Location);

            if (cmd != null)
            {
                commandManager.Execute(cmd, canvasGraphics);
            }

            pictureBox1.Invalidate();


            pictureBox1.Invalidate();
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            currentTool = new PenTool(currentPen);
            this.Cursor = Cursors.Default;
        }


        private void btnRectangle_Click(object sender, EventArgs e)
        {
            currentTool = new ShapeTool(currentPen, ShapeType.Rectangle);
            this.Cursor = Cursors.Default;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            currentTool = new ShapeTool(currentPen, ShapeType.Ellipse);
            this.Cursor = Cursors.Default;
        }

        private void btnTxt_Click(object sender, EventArgs e)
        {
            currentTool = new TextTool(currentPen.Color);
            this.Cursor = Cursors.Default;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            commandManager.Undo(canvasGraphics);
            pictureBox1.Refresh();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            commandManager.Redo(canvasGraphics);
            pictureBox1.Refresh();
        }
        private void trackBarThickness_Scroll(object sender, EventArgs e)
        {
            int thickness = trackBarThickness.Value;

            currentPen = CreatePen(currentPen.Color, thickness);


            lblThickness.Text = $"{thickness} px";

            if (currentTool is PenTool)
                currentTool = new PenTool(currentPen);
            else if (currentTool is ShapeTool shapeTool)
                currentTool = new ShapeTool(currentPen, shapeTool.ShapeType);
            else if (currentTool is LineTool lineTool)
                lineTool.UpdatePen(currentPen);
            else if (currentTool is EraserTool eraserTool)
            {
                eraserTool.UpdatePen(currentPen);
                this.Cursor = CreateEraserCursor((int)(currentPen.Width * 3.5f));
            }


        }

        private void SetPenColor(Color color)
        {
            currentPen = CreatePen(color, currentPen.Width);

            if (currentTool is PenTool)
                currentTool = new PenTool(currentPen);
            else if (currentTool is RectangleTool)
                currentTool = new RectangleTool(currentPen);
            else if (currentTool is EllipseTool)
                currentTool = new EllipseTool(currentPen);
            else if (currentTool is TextTool)
                currentTool = new TextTool(color);
            else if (currentTool is LineTool lineTool)
                lineTool.UpdatePen(currentPen);

        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Black);
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Red);
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Blue);
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Green);
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Yellow);
        }

        private void btnPurple_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Purple);
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.White);
        }

        private void btnOrange_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Orange);
        }

        private void btnPink_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Pink);
        }

        private void btnBrown_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Brown);
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Gray);
        }

        private void btnTrq_Click(object sender, EventArgs e)
        {
            SetPenColor(Color.Turquoise);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(canvasBitmap, 0, 0);
            e.Graphics.DrawImageUnscaled(previewBitmap, 0, 0);
        }

        private void btnTri_Click(object sender, EventArgs e)
        {
            currentTool = new ShapeTool(currentPen, ShapeType.Triangle);
            this.Cursor = Cursors.Default;
        }

        private void btnHrt_Click(object sender, EventArgs e)
        {
            currentTool = new ShapeTool(currentPen, ShapeType.Heart);
            this.Cursor = Cursors.Default;
        }

        private void btnStr_Click(object sender, EventArgs e)
        {
            currentTool = new ShapeTool(currentPen, ShapeType.Star);
            this.Cursor = Cursors.Default;
        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            currentTool = new EraserTool(currentPen);
            this.Cursor = CreateEraserCursor((int)(currentPen.Width * 3.5f));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Bitmap Image (*.bmp)|*.bmp";
                sfd.DefaultExt = "bmp";
                sfd.AddExtension = true;
                sfd.Title = "Save Drawing as BMP";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    canvasBitmap.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
            MessageBox.Show("Resminiz .bmp uzantısıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu uygulama Nesneye Yönelik Programlama dersi kapsamında ödev olarak Ahmet Can Çömez tarafından geliştirilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            currentTool = new LineTool(currentPen);
            this.Cursor = Cursors.Default;

        }
    }
}
