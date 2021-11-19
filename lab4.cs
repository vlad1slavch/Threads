using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace lab4
{
    public enum RectDirection
    {
        LEFT, RIGHT, UP, DOWN
    }
    
    public partial class Form1 : Form
    {
        Thread thread1;
        Thread thread2;
        Thread thread3;
        Graphics g;
        Rectangle r = new Rectangle(20, 20, 30, 30);
        Pen redPen = new Pen(Color.Red);
        RectDirection rd;    

        public Form1()
        {
            InitializeComponent();

            thread1 = new Thread(new ThreadStart(MoveRightLeft));
            thread2 = new Thread(new ThreadStart(MoveUp));
            thread3 = new Thread(new ThreadStart(MoveDown));
            rd = RectDirection.RIGHT;
        }
        private void MoveRightLeft()
        {           
            if (r.X >= pictureBox1.Width - r.Width)
            {
                rd = RectDirection.LEFT;
            }
            else
            if (r.X <= r.Width)
            {
                rd = RectDirection.RIGHT;
            }

            if (rd == RectDirection.RIGHT)
            {
                r.X += 10;
            }
            else
            if (rd == RectDirection.LEFT)
            {
                r.X -= 10;
            }

            pictureBox1.Invoke((MethodInvoker)delegate ()
            {
                pictureBox1.Refresh();
            });

            g.DrawRectangle(redPen, r);
            Thread.Sleep(10);
            MoveRightLeft();
        }

        private void MoveUp()
        {
            while (true)
            {
                SharedRes.mtx.WaitOne();
                SharedRes.rd = RectDirection.UP;

                while (r.Y >= 2 * r.Height)
                {
                    r.Y -= 10;
                    Thread.Sleep(10);
                }
                SharedRes.mtx.ReleaseMutex();
            }
        }

        private void MoveDown()
        {           
           while (true)
           {
                SharedRes.mtx.WaitOne();
                SharedRes.rd = RectDirection.UP;

                while (r.Y <= pictureBox1.Height - 2 * r.Height)
                {
                    r.Y += 10;
                    Thread.Sleep(10);
                }
                SharedRes.mtx.ReleaseMutex();
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {          
            g = pictureBox1.CreateGraphics();
            g.DrawRectangle(redPen, r);

            thread1.Start();
            thread2.Start();
            thread3.Start();     
        }
       
    }
    class SharedRes
    {
        public static Mutex mtx = new Mutex();
        public static RectDirection rd;
    }
}
