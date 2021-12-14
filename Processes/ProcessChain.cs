
namespace Processes
{
    public sealed class ProcessChain //Process Chains can be started by calling Processor.StartProcess and passing in the result of the ProcessChain.CreateProcess method
    {
        public Process[] processes { get; private set; }
        public string name { get; private set; }
        private bool started;

        private ProcessChain(string name, Process[] processes)
        {
            this.name = name;
            this.processes = processes;
            Program.OnUpdate += Loop;
        }

        public static ProcessChain CreateProcess(string name, Process[] processes)
        {
            ProcessChain newProcess = new ProcessChain(name, processes);
            return newProcess;
        }

        private void Loop()
        {
            if (!started) return;

            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i] != null && !processes[i].Equals(null) && !processes[i].Invoke())
                    return;

                if (i == processes.Length - 1)
                    Destroy(this);
            }
        }

        public void Start()
        {
            if (started) return;

            started = true;
            Loop();
        }

        private void Destroy(ProcessChain process)
        {
            process = null;
        }
    }

    public abstract class Process
    {
        public abstract bool Invoke();
    }
}