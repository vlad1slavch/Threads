using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace _lab3
{
    public partial class Form1 : Form
    {
        int[] arr;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }
        private void PrintArray()
        {
            g.Clear(Color.Black);
            for (int i = 0; i < 80; i++)
            {
                g.FillRectangle(new SolidBrush(Color.White), i*10, panel1.Height - arr[i], 5, panel1.Height);  
            }
        }
        private void reset_btn_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            int NumEntries = 80;
            int MaxVal = panel1.Height;
            arr = new int[NumEntries];

            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, panel1.Width, MaxVal);          
            Random r = new Random();

            for (int i = 0; i < NumEntries; i++)
            {
                arr[i] = r.Next(0, MaxVal);
            }

            for (int i = 0; i < NumEntries; i++)
            {
                //g.FillRectangle(new System.Drawing.SolidBrush(Color.White), i, MaxVal - arr[i], 1, MaxVal);
                g.FillRectangle(new SolidBrush(Color.White), i*10, MaxVal - arr[i], 5, MaxVal);                
            }

        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            iSortEngine se = new Sort();

            //Thread t1 = new Thread(() => { se.asc(arr, g, panel1.Height); });
            //Thread t2 = new Thread(() => { se.desc(arr, g, panel1.Height); });
            //Thread t3 = new Thread(() => { se.Draw(); });//

            ////t1.Priority = ThreadPriority.Normal;
            ////t2.Priority = ThreadPriority.Normal;
            ////t3.Priority = ThreadPriority.Highest;
            // t1.Start();
            //t1.Join();

            //t2.Start();
            //t2.Join();

            //t3.Start();
            //t3.Join();

            Parallel.Invoke(() => se.asc(arr, g, panel1.Height),        //ascending sorting
                            () => se.desc(arr, g, panel1.Height),       //descending sorting
                            () => { Thread.Sleep(10); se.Draw(); });    //drawing array

            Thread.Sleep(10);
            PrintArray();
        }
    }
}
