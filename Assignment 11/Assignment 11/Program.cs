using System;

class Program
{
    static void Main()
    {
        // Bubble Sort
        int[] arr = { 5, 1, 4, 2, 8 };
        Console.WriteLine("Before sort:");
        BubbleSortOptimized.Print(arr);
        BubbleSortOptimized.Sort(arr);
        Console.WriteLine("After sort:");
        BubbleSortOptimized.Print(arr);

        // Range<T>
        var intRange = new Range<int>(10, 20);
        Console.WriteLine("\\n15 in range? " + intRange.IsInRange(15));
        Console.WriteLine("Length: " + intRange.Length());

        var doubleRange = new Range<double>(1.5, 3.7);
        Console.WriteLine("\\n2.5 in range? " + doubleRange.IsInRange(2.5));
        Console.WriteLine("Length: " + doubleRange.Length());
    }
}
