using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listacircular<T>

    {
    public class Node
    {
        public T Data;
        public Node Next;
        public Node Prev;

        public Node(T data)
        {
            Data = data;
        }
    }

    public Node head;
    public int Count { get; private set; }

    public void InsertNodeAtStart(T data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
            head.Next = head;
            head.Prev = head;
        }
        else
        {
            newNode.Next = head;
            newNode.Prev = head.Prev;
            head.Prev.Next = newNode;
            head.Prev = newNode;
            head = newNode;
        }
        Count++;
    }

    public Node GetNodeAtPosition(int index)
    {
        Node current = head;
        for (int i = 0; i < index; i++)
        {
            if (current == null)
            {
                return null; // Fuera de límites
            }
            current = current.Next;
        }
        return current;
    }
}


    

