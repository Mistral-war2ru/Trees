using System;
using System.Drawing;
using System.Windows.Forms;

namespace Trees
{
    public partial class Form1 : Form
    {
        Tree tree;
        Random rand = new Random();
        int move_x = 0, move_y = 0, size = 32;
        bool redraw;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            pictureBox1.Width = Width - 16;
            pictureBox1.Height = Height - pictureBox1.Location.Y - 39;
            if (redraw)
            {
                SolidBrush white = new SolidBrush(Color.White);
                Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                g.FillRectangle(white, 0, 0, pictureBox1.Width, pictureBox1.Height);
                white.Dispose();
                tree.recalc();
                tree.Draw(g, move_x + pictureBox1.Width / 2, 4 + move_y);
                redraw = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tree = new Tree(rand.Next(-10, 10), 0, size);
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'f')
            {
                move_x = 0;
                move_y = 0;
                tree = new Tree(rand.Next(-10, 10), 0, size);
                for (int i = 0; i < 100; i++) tree.Add(rand.Next(-100, 100));
                tree.recalc();
                move_x = -tree.dx;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'q')
            {
                move_x = 0;
                move_y = 0;
                tree = new Tree(rand.Next(-10, 10), 0, size);
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'e')
            {
                tree.Add(rand.Next(-100, 100));
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'c')
            {
                for (int i = 0; i < 100; i++) tree.Add(rand.Next(-100, 100));
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'r')
            {
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'w')
            {
                move_y -= tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 's')
            {
                move_y += tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'a')
            {
                move_x -= tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'd')
            {
                move_x += tree.node_size * 5;
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'z')
            {
                if (size > 1) size--;
                tree.chsz(size);
                redraw = true;
                Invalidate();
            }
            if (e.KeyChar == 'x')
            {
                size++;
                tree.chsz(size);
                redraw = true;
                Invalidate();
            }
        }
    }
}
