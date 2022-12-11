using System;
using System.Drawing;
using System.Windows.Forms;

namespace Trees
{
    public class Tree
    {
        public int node_size = 32;

        public int x;
        public int dx = 0;
        public Tree left;
        public Tree right;

        public Tree(int xx, int dxx,int s)
        {
            x = xx;
            dx = dxx;
            node_size = s;
        }

        public void Add(int xx)
        {
            if (xx <= x)
            {
                if (left == null)
                {
                    Tree t = new Tree(xx, dx - node_size, node_size);
                    left = t;
                }
                else left.Add(xx);
            }
            else
            {
                if (right == null)
                {
                    Tree t = new Tree(xx, dx + node_size, node_size);
                    right = t;
                }
                else right.Add(xx);
            }
        }

        private int max_dx()
        {
            int m = dx;
            if (left != null)
            {
                int mm = left.max_dx();
                m = mm > m ? mm : m;
            }
            if (right != null)
            {
                int mm = right.max_dx();
                m = mm > m ? mm : m;
            }
            return m;
        }

        private int min_dx()
        {
            int m = dx;
            if (left != null)
            {
                int mm = left.min_dx();
                m = mm < m ? mm : m;
            }
            if (right != null)
            {
                int mm = right.min_dx();
                m = mm < m ? mm : m;
            }
            return m;
        }

        private void move(int xx)
        {
            dx += xx;
            if (left != null) left.move(xx);
            if (right != null) right.move(xx);
        }

        private bool calc()
        {
            bool c = false;
            if (left != null)
            {
                if (left.calc()) c = true;
                int mx = left.max_dx();
                if (mx >= dx)
                {
                    dx += mx - dx + node_size;
                    c = true;
                }
            }
            if (right != null)
            {
                int mn = right.min_dx();
                if (mn <= dx)
                {
                    right.move(dx - mn + node_size);
                    c = true;
                }
                if (right.calc()) c = true;
            }
            return c;
        }

        public void recalc()
        {
            while (calc());
        }

        public void chsz(int s)
        {
            node_size = s;
            if (left != null) left.chsz(s);
            if (right != null) right.chsz(s);
        }

        public void Draw(Graphics g, int x, int y)
        {
            SolidBrush white = new SolidBrush(Color.White);
            SolidBrush black_text = new SolidBrush(Color.Black);
            Pen black = new Pen(Color.Black);
            Font drawFont = new Font("Arial", 8);
            StringFormat drawFormat = new StringFormat();
            int yy = y + node_size * 2;
            if (left != null)
            {
                g.DrawLine(black, x + dx, y + node_size / 2, x + left.dx, yy + node_size / 2);
                left.Draw(g, x, yy);
            }
            if (right != null)
            {
                g.DrawLine(black, x + dx, y + node_size / 2, x + right.dx, yy + node_size / 2);
                right.Draw(g, x, yy);
            }
            g.FillEllipse(white, x + dx - node_size / 2, y - node_size / 32, node_size, node_size);
            g.DrawEllipse(black, x + dx - node_size / 2, y - node_size / 32, node_size, node_size);
            g.DrawString(this.x.ToString(), drawFont, black_text, x + dx - node_size / 4, y + node_size / 4, drawFormat);

            black.Dispose();
            white.Dispose();
            black_text.Dispose();
            drawFont.Dispose();
            drawFormat.Dispose();
        }
    }

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
