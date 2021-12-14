# Process_Chains
A system for writing asynchronous code in C#

Here is some example code to demonstrate how it is used.

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
                new DoOnce(() => Console.WriteLine("The number is " + num)),
                new DoOnce(() => num++),
                new DoOnUpdate(-1, () => {  num--; OtherFunction(); }, new Func<bool>(() => { return num > 0; })),
                new WaitForSeconds(5),
                new DoOnce(() => CloseProgram?.Invoke())
            }));
        }

        private void OtherFunction()
        {
            Console.WriteLine("num has been reduced by 1, num is now " + num);
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
