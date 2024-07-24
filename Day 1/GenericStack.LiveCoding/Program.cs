namespace Generic_Stack{
class Program
    {
        class Person:IPrintable{
            public string Name { get; set;}
            public Person(string name){
                this.Name = name;
            }
            public string Print(){
                return Name;
            }
        }
        public static void Main()
        {
            var Stack = new Stack<Person>(1000);
            Stack.Push(new Person("p1"));
            Stack.Push(new Person("p2"));
            Console.WriteLine($"Stack contents: {Stack}");
        }
    }
}