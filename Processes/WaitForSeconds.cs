using System;

namespace Processes
{
    //Interupts the process chain until a specified amount of seconds has passed since the chain first reached this process
    public sealed class WaitForSeconds : Process
    {
        bool firstTime = true;
        float seconds;
        DateTime startTime;

        public override bool Invoke()
        {
            if (firstTime)
            {
                Console.WriteLine("Waiting for " + seconds + " seconds");
                startTime = DateTime.Now;
                firstTime = false;
            }

            var timeSinceStart = (DateTime.Now - startTime).TotalSeconds;
            if (timeSinceStart >= seconds)
                return true;
            else
                return false;
        }

        public WaitForSeconds(float seconds)
        {
            this.seconds = seconds;
        }
    }
}