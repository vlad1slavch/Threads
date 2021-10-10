using System;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace _lab3
{
    class Sort : iSortEngine
    {
        //private object o = null;
        private bool _sorted = false;
        private int[] arr;
        private Graphics g;
        private int MaxVal;
        //Brush WhiteBrush = new SolidBrush(Color.White);
        //Brush BlackBrush = new SolidBrush(Color.Black);
        public void asc(int[] arr_In, Graphics g_In, int MaxVal_In)
        {
            arr = arr_In;
            g = g_In;
            MaxVal = MaxVal_In;

            while(!_sorted)
            {
                for (int i = 0; i < arr.Count() - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        //Swap(i, i + 1);
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        //
                        //o = new int[] { i, i+1 };
                        Thread.Sleep(1);
                    }
                }
                _sorted = IsSorted();
            }
        }

        public void desc(int[] arr_In, Graphics g_In, int MaxVal_In)
        {
            arr = arr_In;
            g = g_In;
            MaxVal = MaxVal_In;

            while (!_sorted)
            {
                for (int i = arr.Count() - 1; i > 0; i--)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        //Swap(i, i - 1);
                        int temp = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = temp;
                        //
                        //o = new int[] { i, i - 1 };
                        Thread.Sleep(1);
                    }
                }
                _sorted = IsSorted();               
            }
        }

        public void Draw() 
        {
            while (!_sorted)
            {
                g.Clear(Color.Black);
                for (int i = 0; i < 80; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.White), i * 10, MaxVal - arr[i], 5, MaxVal);   
                }
                Thread.Sleep(50);
            }
        }
        private bool IsSorted()
        {
            for (int i = 0; i < arr.Count() - 1; i++)
            {
                if (arr[i] > arr[i + 1]) return false;
            }
            return true;
        }
    }
}

//if (o != null)
//{
//    int i = ((int[])o)[0];
//    int p = ((int[])o)[1];

//    g.FillRectangle(BlackBrush, i*10, 0, 5, MaxVal);
//    g.FillRectangle(BlackBrush, p*10, 0, 5, MaxVal);

//    g.FillRectangle(WhiteBrush, i*10, MaxVal - arr[i], 5, MaxVal);
//    g.FillRectangle(WhiteBrush, p*10, MaxVal - arr[p], 5, MaxVal);

//    o = null;
//    Thread.Sleep(10);
//}