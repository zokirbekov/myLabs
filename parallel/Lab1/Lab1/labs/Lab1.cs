using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.labs
{
    // Program without async, thread and parallel takes 5.9 MB from RAM

    // Program with parallel takes 6.5 MB from RAM -  (best - 20001 (0) msec)
    // Program with async/await takes 6.4 MB from RAM - 40002 msec (best - 20001 msec)
    // Program with thread takes 7.1 MB from RAM - 110007 msec (best - 20001 msec)

    public class Lab1
    {
        private const int NUMBERS = 100;

        private long timeParallel = 0;
        private long timeThread = 0;

        private delegate void onLast(Action act, long time);
        private event onLast OnLastEvent;
    
        public void Start()
        {
            OnLastEvent += Lab1_OnLastEvent;

            //executeTask();
            //executeParallel();
            //executeThread();
        }

        private void Lab1_OnLastEvent(Action act, long time)
        {
            act();
        }

        private async Task<long> executeByTaskAsync(int n)
        {
            return await Task.Run(() =>
                 {
                     long date = DateTime.Now.Ticks;
                     isTub(n);
                     //if ()
                         //Console.WriteLine("Async/Await : {0}",n);
                     long delta = DateTime.Now.Ticks - date;
                     return delta;
                 });
        }

        private async void executeTask()
        {
            var time = 0L;
            for (int i = 2; i < NUMBERS; i++)
            {
                time += await executeByTaskAsync(i);
                if (i == NUMBERS - 1)
                    OnLastEvent(() =>
                    {
                        Console.WriteLine("Async/Await time : {0}", time);
                    },time);
            }
        }

        private void executeThread()
        {
            for (int i = 2; i < NUMBERS; i++)
            {
                ParameterizedThreadStart parameterized = (n) =>
                {
                    var date = DateTime.Now.Ticks;
                    isTub((int)n);
                    var delta = DateTime.Now.Ticks - date;
                    OnLastEvent.Invoke(() =>
                    {
                        timeThread += delta;
                        Console.WriteLine("Thread : {0}, time : {1}", n, timeThread);
                    }, 0);
                };
                Thread thread = new Thread(parameterized);
                thread.Start(i);
            }
        }

        private void executeParallel()
        {
            int qutoer = NUMBERS / 4;
            for (int i = 2; i < NUMBERS; i++)
            {
                Action<int> act = (k) =>
                {
                    int n = i + (k - 1) * qutoer;
                    if (n >= NUMBERS)
                        return;
                    var date = DateTime.Now.Ticks;
                    //if ()
                    isTub(n);
                        //Console.WriteLine("Parallel : {0}", n);
                    var delta = DateTime.Now.Ticks - date;
                    OnLastEvent.Invoke(() =>
                    {
                        timeParallel += delta;
                        Console.WriteLine("Parallel : {0}, time : {1}", n, timeParallel);
                    }, 0);
                };
                Parallel.Invoke(
                    () => { act(1);  },
                    () => { act(2); },
                    () => { act(3); },
                    () => { act(4); }
                );
            }
        }

        private bool isTub(int n)
        {
            for (int i = 2; i < Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        

    }
}
