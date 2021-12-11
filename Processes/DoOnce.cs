using System;

namespace Processes
{
    class DoOnce : SubProcess
    {
        private bool hasBeenInvoked = false;
        private Action action;

        public override bool Invoke()
        {
            if (action != null && !action.Equals(null) && !hasBeenInvoked)
                action.Invoke();

            hasBeenInvoked = true;
            return true;
        }

        public DoOnce(Action action)
        {
            this.action = action;
        }
    }
}