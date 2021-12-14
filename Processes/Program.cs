using System;

namespace Processes
{
    public class Test : Processor
    {
        public event Action CloseProgram;

        int num = 5;

        public void SomeFunction()
        {
            StartProcess(ProcessChain.CreateProcess("name", new Process[]
            {
                new DoOnce(() => Console.WriteLine("The number of processes on Test object is " + processes.Count)),
                new DoOnce(() => Console.WriteLine("The number is " + num)),
                new DoOnce(() => num++),
                new DoOnce(() => Console.WriteLine("The number is " + num)),
                new DoOnce(() => num++),
                new DoOnce(() => Console.WriteLine("The number is " + num)),
                new DoOnce(() => num++),
                new WaitForSeconds(5),
                new DoOnce(() => CloseProgram?.Invoke())
            }));
        }
    }

    public class Program
    {
        public static event Action OnUpdate;

        static void Main()
        {
            bool loop = true;
            Test test = new Test();

            test.SomeFunction();
            test.CloseProgram += () => loop = false;

            while (loop)
            {
                OnUpdate?.Invoke();
            }
        }
    }
}