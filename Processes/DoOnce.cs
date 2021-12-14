using System;

namespace Processes
{
    //Performs an action without interrupting the process chain
    public sealed class DoOnce : Process
    {
        private bool hasBeenInvoked = false;
        private Action action;

        public override bool Invoke()
        {
            if (action != null && !action.Equals(null) && !hasBeenInvoked)
                action();

            hasBeenInvoked = true;
            return true;
        }

        public DoOnce(Action action)
        {
            this.action = action;
        }
    }
}