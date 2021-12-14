using System;

namespace Processes
{
    //Performs the specified action on each update the specified number of times, interrupting the process chain each time it executes
    //Setting iterationsLeft to -1 means the action will be executed an infinite number of times
    public sealed class DoOnUpdate : Process
    {
        int iterationsLeft;
        Action action;

        public override bool Invoke()
        {
            if (iterationsLeft > 0 || iterationsLeft == -1)
            {
                if (action != null && !action.Equals(null))
                    action.Invoke();
                iterationsLeft--;
                return false;
            }

            return true;
        }

        public DoOnUpdate(int numIterations, Action action)
        {
            iterationsLeft = numIterations;
            this.action = action;
        }
    }
}