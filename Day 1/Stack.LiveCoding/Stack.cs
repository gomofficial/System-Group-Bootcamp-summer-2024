using System;


namespace 
{
    public class Stack
    {
        private const int _size = 1000;
        private int[] _stack;
        private int _top;

        public Stack()
        {
            _stack = new int[_size];
            _top = -1;
        }

        public bool Push(int value)
        {
            if (_top >= _size - 1)
            {
                Console.WriteLine("overflow");
                throw new InvalidOperationException("overflow");
            }
            else
            {
                _stack[++_top] = value;
                Console.WriteLine($"{value} push");
                return true;
            }
        }

        public int Pop()
        {
            if (_top < 0)
            {
                Console.WriteLine("underflow");
                throw new InvalidOperationException("underflow");

            }
            else
            {
                int Value = _stack[_top--];
                Console.WriteLine($"{value} pop");
                return Value;
            }
        }

        public int Peek()
        {
            if (_top < 0)
            {
                Console.WriteLine("empty");
                throw new InvalidOperationException("empty");

            }
            else
            {
                return _stack[_top];
            }
        }

        public bool IsEmpty()
        {
            return _top < 0;
        }
}