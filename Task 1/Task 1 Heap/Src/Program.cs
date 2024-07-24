using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_1.Interface;
using Task_1.Heap;

namespace Task_1
{
    public class Program
    {
        public class Temperature:IPrintable,IComparable<Temperature>
        {
            public double Fahrenheit;

            public Temperature(double fahrenheit)
            {
                this.Fahrenheit = fahrenheit;
            }
            public string Print()
            {
                return Fahrenheit.ToString();
            }

            public int CompareTo(Temperature? tmp) {
                if (tmp != null)
                    return this.Fahrenheit.CompareTo(tmp.Fahrenheit);
                else
                    throw new NullReferenceException("Null");
            }
        }
        public static void Main(string[] args)
        {
            BinaryHeap<Temperature> heap = new BinaryHeap<Temperature>();
            Temperature t1 = new(40);
            Temperature t2 = new(-5);
            Temperature t3 = new(2);
            Temperature t4 = new(0);
            Temperature t5 = new(100);
            Temperature t6 = new(100);
            Temperature t7 = new(100);

            heap.Add(t1);
            heap.Add(t2);
            heap.Add(t3);
            heap.Add(t4);
            heap.Add(t5);
            heap.Add(t6);
            heap.Add(t7);

            Console.WriteLine(heap);
        }
    }
}