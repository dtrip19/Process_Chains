using System;
using System.Collections.Generic;

namespace Processes
{
    public class ProcessChain
    {
        public static event Action<ProcessChain> OnProcessComplete;

        public List<SubProcess> subProcesses;
        private bool hasLooped;

        public ProcessChain()
        {
            subProcesses = new List<SubProcess>();
            Program.OnUpdate += Loop;
            OnProcessComplete += DestroyProcess;
        }

        private void Loop()
        {
            hasLooped = true;

            for (int i = 0; i < subProcesses.Count; i++)
            {
                if (subProcesses[i] == null || subProcesses[i].Equals(null))
                {
                    subProcesses.RemoveAt(i);
                    return;
                }

                if (!subProcesses[i].Invoke())
                    return;

                if (i == subProcesses.Count - 1)
                    OnProcessComplete?.Invoke(this);
            }
        }

        public void IterateRemotely() //Allows Iterate to be called remotely once separate from the Update loop to get things going immediately after initializing
        {
            if (!hasLooped)
                Loop();
        }

        private static void DestroyProcess(ProcessChain process)
        {
            process = null;
        }
    }

    public abstract class SubProcess
    {
        public abstract bool Invoke();
    }
}