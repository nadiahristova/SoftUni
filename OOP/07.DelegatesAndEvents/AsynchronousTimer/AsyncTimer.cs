namespace AsynchronousTimer
{
    using System;
    using System.Threading;

    class AsyncTimer
    {
        private int ticks;
        private int interval;

        public AsyncTimer(Action<int> action, int ticks, int t)
        {
            this.Action = action;
            this.Ticks = ticks;
            this.Interval = t;
        }
        
        public Action<int> Action { get; private set; }

        public int Ticks
        {
            get { return this.ticks; }
            private set
                {
                    if (value < 0)
                        throw new ArgumentException("Tcks", "Value cannot be negative");

                    this.ticks = value;
                }
        }

        public int Interval
        {
            get { return this.interval; }
            private set
                {
                    if (value < 0)
                        throw new ArgumentException("Interval", "Value cannot be negative");

                    this.interval = value;
                }
        }

        public void Start()
        {
            Action(0);

            for (int tick = 1; tick <= this.Ticks; tick++)
            {
                Thread.Sleep(this.Interval);
                Action(tick * this.Interval);
            }            
        }
    }
}
