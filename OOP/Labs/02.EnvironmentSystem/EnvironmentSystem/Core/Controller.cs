namespace EnvironmentSystem.Core
{
    using Interfaces;
    using System;

    public class Controller : IController
    {
        public event EventHandler Pause;

        public bool Paused;

        public void ProcessInput()
        {
            if(Console.KeyAvailable == true)
            {
                if (Console.ReadKey(true).KeyChar == ' ')
                {
                    if (this.Pause != null)
                        this.Pause(this, EventArgs.Empty);
                }
            }            
        }
    }
}
