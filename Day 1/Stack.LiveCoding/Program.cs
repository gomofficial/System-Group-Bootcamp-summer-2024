using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 
{
    class Program
    {
        static void Main()
        {
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Console.WriteLine(stack.Pop() + "pushed");
            Console.WriteLine("top : " + stack.Peek());
            while (!stack.IsEmpty())
            {
                Console.Write(stack.Pop() + "poped");
            }
        }
    }
}