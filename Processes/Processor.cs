using System.Collections.Generic;

namespace Processes
{
    public abstract class Processor //Have anything you want to be able to use processes derive from this
    {
        public List<ProcessChain> processes;

        public void StartProcess(ProcessChain process)
        {
            if (processes == null)
                processes = new List<ProcessChain>();

            processes.Add(process);
            process.Start();
        }

        public void EndProcess(string processToEnd)
        {
            for (int i = processes.Count - 1; i >= 0; i--)
            {
                if (processToEnd != string.Empty && processes[i] != null && !processes[i].Equals(null) && processes[i].name.Equals(processToEnd))
                    processes[i] = null;
            }
        }

        public void EndAllProcesses()
        {
            for (int i = 0; i < processes.Count; i++)
            {
                if (processes[i] != null && !processes[i].Equals(null))
                    processes[i] = null;
            }
        }
    }
}
