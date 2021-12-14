using System;

namespace Processes
{
    //Performs the specified action on each update the specified number of times, interrupting the process chain each time it executes
    //Setting iterationsLeft to -1 means the action will be executed infitely until the condition is met as long as it is not null
    //Setting iterationsLeft to 0 will cause it to do nothing
    //Setting iterationsLeft to 1 or higher will cause the action to be executed as long as the condition is met or is null, up to iterationsLeft times
    public sealed class DoOnUpdate : Process
    {
        int iterationsLeft;
        Action action;
        Func<bool> condition;

        public override bool Invoke()
        {
            if (iterationsLeft == 0) return true;

            if (iterationsLeft > 0)
            {
                if (action != null && !action.Equals(null))
                {
                    if (condition == null || condition != null && condition())
                        action();
                }

                if (condition == null || condition != null && condition())
                    iterationsLeft--;

                return false;
            }
            else if (iterationsLeft == -1)
            {
                if (condition == null || condition != null && condition())
                {
                    if (action != null && !action.Equals(null))
                        action();

                    return false;
                }
            }

            return true;
        }

        public DoOnUpdate(int numIterations, Action action, Func<bool> condition)
        {
            iterationsLeft = numIterations;
            this.action = action;
            this.condition = condition;
        }
    }
}