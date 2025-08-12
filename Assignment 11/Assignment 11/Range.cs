using System;

public class Range<T> where T : IComparable<T>
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
