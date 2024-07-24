using Task_1.Heap;
using Test.TestDataTypes;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Add_Pop()
    {
        //Arrange
        BinaryHeap<Temperature> heap = new BinaryHeap<Temperature>();
        Temperature t1 = new(40);
        Temperature t2 = new(-5);
        Temperature t3 = new(2);
        Temperature t4 = new(0);
        Temperature t5 = new(100);
        //Act
        heap.Add(t1);
        heap.Add(t2);
        heap.Add(t3);
        heap.Add(t4);
        heap.Add(t5);
        //Assert
        Assert.IsTrue(heap.Pop()==t5);
        Assert.IsTrue(heap.Pop()==t1);
        Assert.IsTrue(heap.Pop()==t3);
        Assert.IsTrue(heap.Pop()==t4);
        Assert.IsTrue(heap.Pop()==t2);
    }

    [Test]
    public void Test_Peek()
    {
        //Arrange
        BinaryHeap<Temperature> heap = new BinaryHeap<Temperature>();
        Temperature t1 = new(40);
        Temperature t2 = new(-5);
        //Act
        heap.Add(t1);
        heap.Add(t2);
        //Assert
        Assert.IsTrue(heap.Peek() == t1);
    }

    [Test]
    public void Test_Remove()
    {
        //Arrange
        BinaryHeap<Temperature> heap = new BinaryHeap<Temperature>();
        Temperature t1 = new(40);
        Temperature t2 = new(-5);
        //Act
        heap.Add(t1);
        heap.Add(t2);
        //Assert
        heap.Remove(t1);
        Assert.IsTrue(heap.Pop()==t2);
    }

    [Test]
    public void Test_Remove_Exception()
    {
        BinaryHeap<Temperature> heap = new BinaryHeap<Temperature>();
        Temperature t1 = new(40);

        Assert.Throws<ArgumentException>(() => heap.Remove(t1));
    }
    
    [Test]
    public void Test_Peek_Exception()
    {
        BinaryHeap<Temperature> heap = new BinaryHeap<Temperature>();
        Temperature t1 = new(40);

        Assert.Throws<InvalidOperationException>(() => heap.Peek());
    }
    [Test]
    public void Test_Pop_Exception()
    {
        BinaryHeap<Temperature> heap = new BinaryHeap<Temperature>();
        Temperature t1 = new(40);

        Assert.Throws<InvalidOperationException>(() => heap.Pop());
    }
}