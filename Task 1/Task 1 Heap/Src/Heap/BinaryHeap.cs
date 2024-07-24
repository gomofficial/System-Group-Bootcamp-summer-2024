using System.Text;
using Task_1.Interface;

namespace Task_1.Heap;

public class BinaryHeap<T> where T : IPrintable, IComparable<T>
{
    protected List<T> Heap = new();
    protected int Size;

    // adding a new element to heap
    public void Add(T element)
    {
        Heap.Add(element);
        Size++;
        HeapifyUp(Size - 1);
    }
    
    // Removes the element at the top of the heap
    // Returns object Type T
    public T Pop()
    {
        if (Size == 0) throw new InvalidOperationException("Heap is Empty");
        var item = Heap[0];
        Heap[0] = Heap[Size - 1];
        Size--;
        HeapifyDown(0);
        return item;
    }

    // See what is on the top of the heap without removing it
    // Returns Object Type T
    public T Peek()
    {
        if (Size == 0) throw new InvalidOperationException("Heap is Empty");
        return Heap[0];
    }
    
    // Removes a certain element in heap by swapping it with the last element and then removing it from
    // the end of the heap. then it starts to Heapify the swapped element up or down.
    public void Remove(T element)
    {
        var index = Heap.IndexOf(element);
        if (index == -1)
        {
            throw new ArgumentException($"{element} not in heap!");
        }

        Swap(index, Size - 1);
        Heap.RemoveAt(Size - 1);
        Size--;
        int parentIndex = (index - 1) / 2;
        if (Heap[index].CompareTo((Heap[parentIndex])) > 0)
        {
            HeapifyUp(index);
        }
        else
        {
            HeapifyDown(index);
        }
    }
    private void HeapifyUp(int index)
    {
        if (HasParent(index) && Heap[index].CompareTo(Parent(index)) > 0)
        {
            int parent = GetParentIndex(index);
            Swap(index, parent);
            HeapifyUp(parent);
        }
    }
    private void HeapifyDown(int index)
    {
        if (HasLeftChild(index))
        {
            int largestChild = GetLeftChildIndex(index);
            if (HasRightChild(index) && LeftChild(index).CompareTo(RightChild(index)) < 0)
            {
                largestChild = GetRightChildIndex(index);
            }

            if (Heap[index].CompareTo(Heap[largestChild]) < 0)
            {
                Swap(index, largestChild);
                HeapifyDown(largestChild);
            }
        }
    }
    
    //Swapping Between two of the indexes of heap
    private void Swap(int i, int j)
    {
        if ((i<0 || i>Size) || (j<0 || j>Size))
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        (Heap[i], Heap[j]) = (Heap[j], Heap[i]);
    }
    
    //returns the left child index
    // Returns int
    private int GetLeftChildIndex(int parentIndex)
    {
        if (parentIndex<0 || parentIndex>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        return 2 * parentIndex + 1;
    }
    
    //returns the right child index
    // Returns int
    private int GetRightChildIndex(int parentIndex)
    {
        if (parentIndex<0 || parentIndex>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        return 2 * parentIndex + 2;
    }
    
    //returns the parent index
    // Returns int
    private int GetParentIndex(int childIndex)
    {
        if (childIndex<0 || childIndex>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        
        return (childIndex - 1) / 2;
    }
    
    //returns the left child object
    // Returns object Type T
    private T LeftChild(int index)
    {
        if (index<0 || index>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        var leftChild = Heap[GetLeftChildIndex(index)];
        if (leftChild == null)
        {
            throw new NullReferenceException("parent object is null.");
        }
        return leftChild;
    }
    
    //returns the right child object
    // Returns object Type T
    private T RightChild(int index)
    {
        if (index<0 || index>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        var rightChild = Heap[GetRightChildIndex(index)];
        if (rightChild == null)
        {
            throw new NullReferenceException("parent object is null.");
        }
        return rightChild;
    }
    
    //returns the parent object
    // Returns object Type T
    private T Parent(int index)
    {
        if (index<0 || index>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        var parent = Heap[GetParentIndex(index)];
        if (parent == null)
        {
            throw new NullReferenceException("parent object is null.");
        }
        return parent;
    }
    
    //Checks the presence of left child
    // Returns bool
    private bool HasLeftChild(int index)
    {
        if (index<0 || index>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        return GetLeftChildIndex(index) < Size;
    }
    
    //Checks the presence of right child
    // Returns bool
    private bool HasRightChild(int index)
    {
        if (index<0 || index>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        return GetRightChildIndex(index) < Size;
    }
    
    //Checks the presence of parent
    // Returns bool
    private bool HasParent(int index)
    {
        if (index<0 || index>Size)
        {
            throw new IndexOutOfRangeException("Invalid index");
        }
        return GetParentIndex(index) >= 0;
    }
    
    // Override ToString creates a string which represents the heap
    // Returns string
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        int levels = (int)Math.Log2(Size) + 1;
        for (int level = 0; level < levels; level++)
        {
            int levelStartIndex = (1 << level) - 1;
            int levelEndIndex = Math.Min(Size, (1 << (level + 1)) - 1);
            result.Append(new string(' ', (levels - level - 1) * 2));
            for (int i = levelStartIndex; i < levelEndIndex; i++)
            {
                result.Append(Heap[i].Print());
                if (i < levelEndIndex - 1)
                    result.Append(", ");
            }
            result.AppendLine();
        }
        return result.ToString();
    }
}