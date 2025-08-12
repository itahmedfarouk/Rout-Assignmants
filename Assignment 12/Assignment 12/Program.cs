class Program
{
    static void Main()
    {
        //  Q1 - Reverse Queue
        Queue<int> numbers = new Queue<int>(new[] { 1, 2, 3, 4, 5 });
        Console.WriteLine("Original Queue: " + string.Join(", ", numbers));
        QueueReverser.ReverseQueue(numbers);
        Console.WriteLine("Reversed Queue: " + string.Join(", ", numbers));

        //  Q2 - Check Balanced Parentheses
        string input = "[()]{()}";
        Console.WriteLine($"\nInput: {input}");
        Console.WriteLine(ParenthesesChecker.IsBalanced(input) ? "Balanced" : "Not Balanced");

        input = "[({)]}";
        Console.WriteLine($"\nInput: {input}");
        Console.WriteLine(ParenthesesChecker.IsBalanced(input) ? "Balanced" : "Not Balanced");
    }
}
