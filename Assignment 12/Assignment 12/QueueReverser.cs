using System;
using System.Collections.Generic;

public class QueueReverser
{
    public static void ReverseQueue<T>(Queue<T> queue)
    {
        Stack<T> stack = new Stack<T>();

        // Step 1: Dequeue all elements into stack
        while (queue.Count > 0)
            stack.Push(queue.Dequeue());

        // Step 2: Push back to queue (now reversed)
        while (stack.Count > 0)
            queue.Enqueue(stack.Pop());
    }
}
