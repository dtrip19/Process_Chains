using System;

namespace Processes
{
    public class Test : Processor
    {
        int num = 5;
        string name = "name";

        public void SomeFunction()
        {
            StartProcess(ProcessChain.CreateProcess("name", new Process[]
            {
                new DoOnce(() => Console.WriteLine("The number of processes on Test object is " + processes.Count)),
                new DoOnce(() => Console.WriteLine("The number is " + num)),
                new DoOnUpdate(5, () => Console.WriteLine("The name is " + name))
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

            while (loop)
            {
                OnUpdate?.Invoke();
            }
        }
    }
}
