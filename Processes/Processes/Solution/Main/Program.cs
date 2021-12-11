using System;

namespace Processes
{
    public class Program
    {
        public static event Action OnUpdate;

        private static void DestroyProcess(ProcessChain process)
        {
            process = null;
        }

        static void Main()
        {
            ProcessChain.OnProcessComplete += DestroyProcess;

            ProcessChain process = new ProcessChain();
            process.subProcesses.Add(new DoOnce(() => Console.WriteLine("New DoOnce created")));
            process.subProcesses.Add(new WaitForSeconds(3));
            process.subProcesses.Add(new DoOnUpdate(5, () => Console.WriteLine("DoOnUpdate action performed")));
            process.subProcesses.Add(new WaitForSeconds(3));
            process.subProcesses.Add(new DoOnce(() => Console.WriteLine("Reached the end of the process")));
            process.IterateRemotely();

            while (true)
            {
                OnUpdate?.Invoke();
            }
        }
    }
}