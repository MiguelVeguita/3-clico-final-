using System;

public class DoubleCircularList<T>
{
    public class Node
    {
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public T Value { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }

    private Node head;
    private int length = 0;

    public void InsertNodeAtStart(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
            head.Next = head;
            head.Previous = head;
        }
        else
        {
            Node tail = head.Previous;
            newNode.Next = head;
            newNode.Previous = tail;
            head.Previous = newNode;
            tail.Next = newNode;
            head = newNode;
        }
        length++;
    }

    public void InsertNodeAtEnd(T value)
    {
        if (head == null)
        {
            InsertNodeAtStart(value);
        }
        else
        {
            Node newNode = new Node(value);
            Node tail = head.Previous;
            newNode.Next = head;
            newNode.Previous = tail;
            tail.Next = newNode;
            head.Previous = newNode;
        }
        length++;
    }

    public Node GetFirstNodeA()
    {
        return head;
    }

    public int GetLengthA()
    {
        return length;
    }
}
