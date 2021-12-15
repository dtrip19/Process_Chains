using System.Collections.Generic;

namespace Processes
{
    public abstract class Processor //Have anything you want to be able to use processes derive from this
    {
        private List<ProcessChain> processes;

        public void StartProcess(ProcessChain process)
        {
            if (processes == null)
                processes = new List<ProcessChain>();

            processes.Add(process);
            process.OnProcessComplete += RemoveProcessFromList;
            process.Start();
        }

        public void EndProcess(string processToEnd)
        {
            for (int i = processes.Count - 1; i >= 0; i--)
            {
                if (processToEnd != string.Empty && processes[i] != null && !processes[i].Equals(null) && processes[i].name.Equals(processToEnd))
                {
                    processes[i] = null;
                    processes.RemoveAt(i);
                }
            }
        }

        public void EndAllProcesses()
        {
            for (int i = 0; i < processes.Count; i++)
            {
                if (processes[0] != null && !processes[0].Equals(null))
                {
                    processes[i] = null;
                    processes.RemoveAt(0);
                }
            }
        }

        void RemoveProcessFromList(ProcessChain process)
        {
            for (int i = 0; i < processes.Count; i++)
            {
                if (processes[i].Equals(process))
                    processes.RemoveAt(i);
            }
        }
    }
}
