using System;
using System.Threading;
using System.Windows.Forms;

namespace Clock
{
    
    public partial class Form1 : Form
    {
        Thread thread1;
        Thread thread2;
        Thread thread3;
        public Form1()
        {
            InitializeComponent();

            thread1 = new Thread(() =>
            {
                int ms = 0,
                    minToUpdate = 0,
                    secToUpdate = 0,
                    msToUpdate = 0;

                while (true)
                {
                    label1.Invoke((MethodInvoker)delegate ()
                    {
                        label1.Text = addZero(DateTime.Now.Hour);
                        minToUpdate = 60 - DateTime.Now.Minute - 1;
                        secToUpdate = 60 - DateTime.Now.Second - 1;
                        msToUpdate = 1000 - DateTime.Now.Millisecond;
                        ms = minToUpdate * 1000 * 60 + secToUpdate * 1000 + msToUpdate;                 
                    });
                    Thread.Sleep(ms);
                }
            });

            thread2 = new Thread(() =>
            {
                int ms = 0,
                    secToUpdate = 0,
                    msToUpdate = 0;
                while (true)
                {
                    label2.Invoke((MethodInvoker)delegate ()
                    {
                        label2.Text = addZero(DateTime.Now.Minute);
                        secToUpdate = 60 - DateTime.Now.Second - 1;
                        msToUpdate = 1000 - DateTime.Now.Millisecond;
                        ms = secToUpdate * 1000 + msToUpdate;                    
                    });
                    Thread.Sleep(ms);
                }
            });

            thread3 = new Thread(() =>
            {
                int ms = 0;
                while (true)
                {
                    label3.Invoke((MethodInvoker)delegate ()
                    {
                        label3.Text = addZero(DateTime.Now.Second);
                        ms = 1000 - DateTime.Now.Millisecond;
                    });
                    Thread.Sleep(ms);
                }               
            });
        }

        private string addZero(int time)
        {            
            if (time < 10) return "0" + time;
            else return time.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            thread1.Start(); 
            thread2.Start(); 
            thread3.Start();
        }
    }
    
}
