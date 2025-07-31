using System;

#region Q1 - WeekDays Enum
enum WeekDays { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

class Program
{
    static void Main()
    {
        Console.WriteLine("Week Days:");
        foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
        {
            Console.WriteLine(day);
        }
        #endregion

        #region Q2 - Person Struct Array
        Console.WriteLine("\nPeople List:");
        Person[] persons = new Person[3];
        persons[0] = new Person("Ali", 25);
        persons[1] = new Person("Sara", 30);
        persons[2] = new Person("Omar", 22);

        foreach (var person in persons)
        {
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
        }
        #endregion

        #region Q3 - Season Enum Input
        Console.WriteLine("\nEnter a season (Spring, Summer, Autumn, Winter):");
        string inputSeason = Console.ReadLine();
        Season season;
        if (Enum.TryParse(inputSeason, true, out season))
        {
            switch (season)
            {
                case Season.Spring:
                    Console.WriteLine("March to May"); break;
                case Season.Summer:
                    Console.WriteLine("June to August"); break;
                case Season.Autumn:
                    Console.WriteLine("September to November"); break;
                case Season.Winter:
                    Console.WriteLine("December to February"); break;
            }
        }
        else
        {
            Console.WriteLine("Invalid season name.");
        }
        #endregion

        #region Q4 - Permissions Enum with Flags
        Permissions myPerms = Permissions.Read | Permissions.Write;
        Console.WriteLine($"\nInitial Permissions: {myPerms}");

        myPerms |= Permissions.Execute;
        Console.WriteLine($"After Adding Execute: {myPerms}");

        myPerms &= ~Permissions.Write;
        Console.WriteLine($"After Removing Write: {myPerms}");

        Console.WriteLine("Has Delete? " + myPerms.HasFlag(Permissions.Delete));
        #endregion

        #region Q5 - Color Check
        Console.WriteLine("\nEnter a color (Red, Green, Blue):");
        string inputColor = Console.ReadLine();
        if (Enum.TryParse(inputColor, true, out Colors color))
        {
            Console.WriteLine($"{color} is a primary color.");
        }
        else
        {
            Console.WriteLine("Not a primary color.");
        }
        #endregion

        #region Q6 - Point Struct and Distance
        Console.WriteLine("\nEnter coordinates for Point 1 (X and Y):");
        double x1 = double.Parse(Console.ReadLine());
        double y1 = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter coordinates for Point 2 (X and Y):");
        double x2 = double.Parse(Console.ReadLine());
        double y2 = double.Parse(Console.ReadLine());

        Point p1 = new Point(x1, y1);
        Point p2 = new Point(x2, y2);
        Console.WriteLine("Distance = " + p1.DistanceTo(p2));
        #endregion

        #region Q7 - Oldest Person
        Console.WriteLine("\nEnter 3 people:");
        Person[] people = new Person[3];
        for (int i = 0; i < 3; i++)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());
            people[i] = new Person(name, age);
        }

        Person oldest = people[0];
        foreach (var p in people)
            if (p.Age > oldest.Age) oldest = p;

        Console.WriteLine($"Oldest: {oldest.Name}, Age: {oldest.Age}");
        #endregion
    }
}

struct Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

struct Point
{
    public double X, Y;
    public Point(double x, double y)
    {
        X = x; Y = y;
    }

    public double DistanceTo(Point other)
    {
        return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2));
    }
}

enum Season { Spring, Summer, Autumn, Winter }

[Flags]
enum Permissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Delete = 4,
    Execute = 8
}

enum Colors { Red, Green, Blue }
