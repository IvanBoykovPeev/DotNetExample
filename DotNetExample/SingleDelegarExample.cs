using System;

namespace DotNetExample
{
    //Declaration of delegate
    public delegate void SimpleDelegat(string aParam);

    class TestDelegate
    {
        public static void TestFunction(string aParam)
        {
            Console.WriteLine("I was called by a delegate.");
            Console.WriteLine("I got a parameter {0}.", aParam);
        }

        static void Main()
        {
            //Insantietion of delegate
            SimpleDelegat simpleDelegate = new SimpleDelegat(TestFunction);
            //Invocation of the method, pointet by a delegate
            simpleDelegate("Any string");
        }
    }
}
