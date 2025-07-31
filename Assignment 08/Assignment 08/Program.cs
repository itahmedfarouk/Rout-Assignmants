using System;

#region Project 1 – Point3D with Constructor Chaining, ToString, ==, Sorting, ICloneable

class Point3D : IComparable, ICloneable
{
    public int X, Y, Z;

    public Point3D() : this(0, 0, 0) { }
    public Point3D(int x) : this(x, 0, 0) { }
    public Point3D(int x, int y) : this(x, y, 0) { }
    public Point3D(int x, int y, int z)
    {
        X = x; Y = y; Z = z;
    }

    public override string ToString()
    {
        return $"Point Coordinates: ({X}, {Y}, {Z})";
    }

    public static bool operator ==(Point3D a, Point3D b)
    {
        return a?.X == b?.X && a?.Y == b?.Y && a?.Z == b?.Z;
    }

    public static bool operator !=(Point3D a, Point3D b) => !(a == b);

    public override bool Equals(object obj)
    {
        return obj is Point3D p && this == p;
    }

    public override int GetHashCode()
    {
        return (X, Y, Z).GetHashCode();
    }

    public int CompareTo(object obj)
    {
        var p = obj as Point3D;
        int result = X.CompareTo(p.X);
        return result != 0 ? result : Y.CompareTo(p.Y);
    }

    public object Clone()
    {
        return new Point3D(X, Y, Z);
    }
}

#endregion

#region Project 2 – Static Maths Class

static class Maths
{
    public static int Add(int a, int b) => a + b;
    public static int Subtract(int a, int b) => a - b;
    public static int Multiply(int a, int b) => a * b;
    public static double Divide(int a, int b) => b != 0 ? (double)a / b : double.NaN;
}

#endregion

#region Project 3 – Duration with Constructors, Overrides, Operator Overloading

class Duration
{
    public int Hours { get; private set; }
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    public Duration(int h, int m, int s)
    {
        Hours = h; Minutes = m; Seconds = s;
        Normalize();
    }

    public Duration(int totalSeconds)
    {
        Hours = totalSeconds / 3600;
        Minutes = (totalSeconds % 3600) / 60;
        Seconds = totalSeconds % 60;
    }

    public override string ToString()
    {
        string result = "";
        if (Hours > 0) result += $"Hours: {Hours}, ";
        if (Hours > 0 || Minutes > 0) result += $"Minutes: {Minutes}, ";
        result += $"Seconds: {Seconds}";
        return result;
    }

    public override bool Equals(object obj)
    {
        return obj is Duration d && this.TotalSeconds() == d.TotalSeconds();
    }

    public override int GetHashCode() => TotalSeconds().GetHashCode();

    public int TotalSeconds() => Hours * 3600 + Minutes * 60 + Seconds;

    private void Normalize()
    {
        int total = TotalSeconds();
        Hours = total / 3600;
        Minutes = (total % 3600) / 60;
        Seconds = total % 60;
    }

    public static Duration operator +(Duration a, Duration b) => new Duration(a.TotalSeconds() + b.TotalSeconds());
    public static Duration operator +(Duration a, int s) => new Duration(a.TotalSeconds() + s);
    public static Duration operator +(int s, Duration a) => new Duration(a.TotalSeconds() + s);
    public static Duration operator -(Duration a, Duration b) => new Duration(a.TotalSeconds() - b.TotalSeconds());
    public static Duration operator ++(Duration d) => new Duration(d.TotalSeconds() + 60);
    public static Duration operator --(Duration d) => new Duration(d.TotalSeconds() - 60);
    public static bool operator >(Duration a, Duration b) => a.TotalSeconds() > b.TotalSeconds();
    public static bool operator <(Duration a, Duration b) => a.TotalSeconds() < b.TotalSeconds();
    public static bool operator >=(Duration a, Duration b) => a.TotalSeconds() >= b.TotalSeconds();
    public static bool operator <=(Duration a, Duration b) => a.TotalSeconds() <= b.TotalSeconds();
    public static explicit operator DateTime(Duration d) => new DateTime(1, 1, 1, d.Hours, d.Minutes, d.Seconds);
}

#endregion

#region Main Method Example Usage

class Program
{
    static void Main()
    {
        // Project 1
        var P = new Point3D(10, 10, 10);
        Console.WriteLine(P.ToString());

        Console.WriteLine("\nEnter coordinates for P1:");
        int.TryParse(Console.ReadLine(), out int x1);
        int.TryParse(Console.ReadLine(), out int y1);
        int.TryParse(Console.ReadLine(), out int z1);
        var P1 = new Point3D(x1, y1, z1);

        Console.WriteLine("Enter coordinates for P2:");
        int x2 = Convert.ToInt32(Console.ReadLine());
        int y2 = Convert.ToInt32(Console.ReadLine());
        int z2 = Convert.ToInt32(Console.ReadLine());
        var P2 = new Point3D(x2, y2, z2);

        Console.WriteLine($"P1 == P2? {P1 == P2}");

        Point3D[] arr = {
            new Point3D(3, 2, 1),
            new Point3D(1, 5, 2),
            new Point3D(2, 1, 3)
        };
        Array.Sort(arr);
        Console.WriteLine("Sorted Points:");
        foreach (var pt in arr)
            Console.WriteLine(pt);

        var cloned = (Point3D)P1.Clone();
        Console.WriteLine("Cloned P1: " + cloned);

        // Project 2
        Console.WriteLine("\nMaths:");
        Console.WriteLine("Add: " + Maths.Add(5, 3));
        Console.WriteLine("Sub: " + Maths.Subtract(5, 3));
        Console.WriteLine("Mul: " + Maths.Multiply(5, 3));
        Console.WriteLine("Div: " + Maths.Divide(5, 3));

        // Project 3
        Console.WriteLine("\nDurations:");
        Duration D1 = new Duration(1, 10, 15);
        Console.WriteLine(D1);
        Duration D2 = new Duration(7800);
        Console.WriteLine(D2);
        Duration D3 = new Duration(666);
        Console.WriteLine(D3);

        D3 = D1 + D2;
        Console.WriteLine("D3 = D1 + D2 → " + D3);
        D3 = D1 + 7800;
        Console.WriteLine("D3 = D1 + 7800 → " + D3);
        D3 = 666 + D3;
        Console.WriteLine("D3 = 666 + D3 → " + D3);

        D3 = ++D1;
        Console.WriteLine("++D1 → " + D3);

        D3 = --D2;
        Console.WriteLine("--D2 → " + D3);

        D1 = D1 - D2;
        Console.WriteLine("D1 - D2 → " + D1);

        Console.WriteLine("D1 > D2? " + (D1 > D2));
        Console.WriteLine("D1 <= D2? " + (D1 <= D2));

        DateTime dt = (DateTime)D1;
        Console.WriteLine("DateTime cast: " + dt.ToLongTimeString());
    }
}

#endregion
