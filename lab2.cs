using System;
using System.Threading;
using System.Diagnostics;

namespace Threads_lab2
{
    class Program
    {
        static int[] arr;
        static int totalSum;
        static int num;

        static void getSum(object o)
        {
            //Визначити суму позитивних елементів масиву, індекси яких кратні заданому числу (num)
            int start = ((int[])o)[0];
            int end = ((int[])o)[1];
            int sum = 0;

            for (int i = start; i < end; i++)
            {                
                if (arr[i] > 0 && arr[i] % num == 0)
                {
                    sum += arr[i];
                }
                
            }
            totalSum += sum;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int arrNum = 1, threadNum = 1;

            try
            {
                Console.Write("Введіть число: ");
                num = int.Parse(Console.ReadLine());

                Console.Write("Кількість елементів масиву: ");
                arrNum = int.Parse(Console.ReadLine());

                Console.Write("Кількість потоків: ");
                threadNum = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Введено невірні дані");
            }

            Random rand = new Random();
            arr = new int[arrNum];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next(-20, 20);         
            
            if (threadNum == 1)
            {
                Stopwatch sw = new Stopwatch();              
                sw.Start();

                Thread thr = new Thread(() => { getSum(new int[] { 0, arrNum}); });
                thr.Start();         
                thr.Join();

                sw.Stop();  
                Console.WriteLine("Витрачено часу (мс): {0}", sw.ElapsedMilliseconds);
                Console.WriteLine("Сума: {0}", totalSum);
            }          
            else
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                int elemPerThread = arrNum / threadNum;
                int startIndex = -elemPerThread;
                int endIndex = 0;

                Thread[] arrThr = new Thread[threadNum];
                for (int i = 0; i < threadNum; i++)
                {                   
                    arrThr[i] = new Thread(() => { getSum(new int[] { startIndex += elemPerThread, endIndex += elemPerThread}); });
                    arrThr[i].Start();
                }

                for (int j = 0; j < threadNum; j++)
                    arrThr[j].Join();

                stopwatch.Stop();
                Console.WriteLine("Витрачено часу (мс): {0}", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("Сума: {0}", totalSum);
            }

            Console.ReadLine();
        }
    }
}
