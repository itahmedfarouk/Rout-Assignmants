using System;

#region Q1 - Optimized Bubble Sort
class BubbleSortOptimized
{
    public static void Sort(int[] array)
    {
        int n = array.Length;
        bool swapped;
        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
    }

    public static void Print(int[] array)
    {
        Console.WriteLine(string.Join(", ", array));
    }
}
#endregion

#region Q2 - Generic Range<T> class
class Range<T> where T : IComparable<T>
{
    public T Min { get; }
    public T Max { get; }

    public Range(T min, T max)
    {
        if (min.CompareTo(max) > 0)
            throw new ArgumentException("Min must be <= Max");
        Min = min;
        Max = max;
    }

    public bool IsInRange(T value)
    {
        return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
    }

    public dynamic Length()
    {
        try
        {
            return (dynamic)Max - (dynamic)Min;
        }
        catch
        {
            throw new InvalidOperationException("Cannot calculate range length for this type");
        }
    }
}
#endregion

#region Program - Main Method
class Program
{
    static void Main()
    {
        // Test Q1
        int[] arr = { 5, 1, 4, 2, 8 };
        Console.WriteLine("Before sort:");
        BubbleSortOptimized.Print(arr);
        BubbleSortOptimized.Sort(arr);
        Console.WriteLine("After optimized bubble sort:");
        BubbleSortOptimized.Print(arr);

        // Test Q2
        var intRange = new Range<int>(10, 20);
        Console.WriteLine("\n15 in range? " + intRange.IsInRange(15));  // true
        Console.WriteLine("Length: " + intRange.Length());              // 10

        var doubleRange = new Range<double>(1.5, 3.7);
        Console.WriteLine("\n2.5 in range? " + doubleRange.IsInRange(2.5));  // true
        Console.WriteLine("Length: " + doubleRange.Length());               // 2.2
    }
}
#endregion
