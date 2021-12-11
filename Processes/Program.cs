using System;

namespace Processes
{
    public class Program
    {
        public static event Action OnUpdate;

        static void Main()
        {
            bool loop = true;

            ProcessChain process2 = new ProcessChain() { subProcesses = new SubProcess[]
            {
                new DoOnce(() => Console.WriteLine("Process2 started")),
                new WaitForSeconds(5),
                new DoOnUpdate(5, () => Console.WriteLine("On update action performed in process2")),
                new WaitForSeconds(5),
                new DoOnce(() => Console.WriteLine("Process2 ended"))
            }};

            ProcessChain process1 = new ProcessChain() { subProcesses = new SubProcess[]
            {
                new DoOnce(() => Console.WriteLine("Process1 started")),
                new WaitForSeconds(5),
                new DoOnce(() => process2.Start()),
                new DoOnce(() => Console.WriteLine("Process 1 ended"))
            }};

            process1.Start();
            process2.OnProcessComplete += new Action<ProcessChain>((ProcessChain process) => loop = false);

            while (loop)
            {
                OnUpdate?.Invoke();
            }
        }
    }
}