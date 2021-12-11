using System;

namespace Processes
{
    public class ProcessChain
    {
        public event Action<ProcessChain> OnProcessComplete;

        public SubProcess[] subProcesses;
        private bool started;

        public ProcessChain()
        {
            Program.OnUpdate += Loop;
            OnProcessComplete += DestroyProcess;
        }

        private void Loop()
        {
            if (!started) return;

            for (int i = 0; i < subProcesses.Length; i++)
            {
                if (subProcesses[i] != null && !subProcesses[i].Equals(null) && !subProcesses[i].Invoke())
                    return;

                if (i == subProcesses.Length - 1)
                    OnProcessComplete?.Invoke(this);
            }
        }

        public void Start()
        {
            if (started) return;

            started = true;
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