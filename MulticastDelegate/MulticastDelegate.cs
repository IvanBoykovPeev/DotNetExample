using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulticastDelegate
{
    public delegate void StringDelegate(string aValue);

    public class TestMulticastDelegate
    {
        void PrintString(string aValue)
        {
            Console.WriteLine(aValue);
        }

        void PrintStringLength(string aValue)
        {
            Console.WriteLine("Lenght = {0}", aValue.Length);
        }

        static void PrintStringWithDate(string aValue)
        {
            Console.WriteLine("{0}: {1}", DateTime.Now, aValue);
        }

        static void PrintStringInvocatioList(Delegate aDelegate)
        {
            Console.WriteLine("(");
            Delegate[] list = aDelegate.GetInvocationList();
            foreach (Delegate d in list)
            {
                Console.WriteLine(" {0}", d.Method.Name);
            }
            Console.WriteLine(" )");
        }

        static void Main(string[] args)
        {
            TestMulticastDelegate tmd = new TestMulticastDelegate();
            StringDelegate printDelegate = new StringDelegate(tmd.PrintString);
            StringDelegate printLenghtDelegate = new StringDelegate(tmd.PrintString);
            StringDelegate printWithDateDelegate = new StringDelegate(PrintStringWithDate);
            PrintStringInvocatioList(printDelegate);
            //Prints: (PrintString)
            StringDelegate combinedDelegate = (StringDelegate)Delegate.Combine(printDelegate, printLenghtDelegate);
            PrintStringInvocatioList(combinedDelegate);
            //Prints: ( PrintString PrintStringLenghts )

            combinedDelegate = (StringDelegate)Delegate.Combine(combinedDelegate, printWithDateDelegate);
            PrintStringInvocatioList(combinedDelegate);
            //Prints: ( PrintString PrintStringLenght PrintStringWithDate )

            //Invoke the delegate
            combinedDelegate("test");
        }
    }
}
