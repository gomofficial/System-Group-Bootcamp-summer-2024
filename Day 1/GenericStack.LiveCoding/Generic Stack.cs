using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;


namespace Generic_Stack
{

    public class Stack<T> where T : IPrintable
    {
        private T[] _stack;
        private int _top;

        public Stack(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
            _stack = new T[capacity];
            _top = 0;
        }

        public int Size => _top;

        public void Push(T item)
        {
            if (_top >= _stack.Length)
                throw new InvalidOperationException("overflow.");

            _stack[_top] = item;
            _top++;
        }

        public T Pop()
        {
            if (_top == 0)
                throw new InvalidOperationException("underflow.");

            _top--;
            return _stack[_top];
        }

        public T Peek()
        {
            if (_top == 0)
                throw new InvalidOperationException("empty.");

            return _stack[_top - 1];
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = _top - 1; i >= 0; i--)
            {
                sb.Append(_stack[i].Print());
                if (i > 0)
                    sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}