using System;

namespace Processes
{
    //Interupts the process chain until a specified amount of seconds have passed since it was first reached
    public class WaitForSeconds : SubProcess
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
            if (timeSinceStart > seconds)
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
