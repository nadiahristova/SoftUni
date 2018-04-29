namespace AsynchronousTimer
{
    using System;

    class TestTimer
    {
        static void Main(string[] args)
        {
            Action<int> countMillisecs = (ms) => { Console.WriteLine($"{ms}ms passed..."); };

            AsyncTimer timer = new AsyncTimer(countMillisecs, 5, 1000);
            timer.Start();
        }
    }
}
