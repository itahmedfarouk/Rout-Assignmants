using System;

// =============================================
// Part 01 - Multiple Choice Questions (MCQs)
// =============================================

// 1. What is the primary purpose of an interface in C#?
//    Answer: b) To define a blueprint for a class
// 2. Which of the following is NOT a valid access modifier for interface members in C#?
//    Answer: a) private
// 3. Can an interface contain fields in C#?
//    Answer: b) No
// 4. In C#, can an interface inherit from another interface?
//    Answer: b) Yes, interfaces can inherit from multiple interfaces
// 5. Which keyword is used to implement an interface in a class in C#?
//    Answer: d) implements
// 6. Can an interface contain static methods in C#?
//    Answer: a) Yes
// 7. In C#, can an interface have explicit access modifiers for its members?
//    Answer: b) No, all members are implicitly public
// 8. What is the purpose of an explicit interface implementation in C#?
//    Answer: a) To hide the interface members from outside access
// 9. In C#, can an interface have a constructor?
//    Answer: b) No, interfaces cannot have constructors
// 10. How can a C# class implement multiple interfaces?
//    Answer: c) By separating interface names with commas

// =============================================
// Part 02 - Practical Interface Implementations
// =============================================

interface IShape
{
    double Area { get; }
    void DisplayShapeInfo();
}

interface ICircle : IShape { }
interface IRectangle : IShape { }

class Circle : ICircle
{
    public double Radius { get; set; }
    public double Area => Math.PI * Radius * Radius;

    public Circle(double radius) => Radius = radius;

    public void DisplayShapeInfo()
    {
        Console.WriteLine($"Circle: Radius = {Radius}, Area = {Area:F2}");
    }
}

class Rectangle : IRectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Area => Width * Height;

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public void DisplayShapeInfo()
    {
        Console.WriteLine($"Rectangle: Width = {Width}, Height = {Height}, Area = {Area}");
    }
}

interface IAuthenticationService
{
    bool AuthenticateUser(string username, string password);
    bool AuthorizeUser(string username, string role);
}

class BasicAuthenticationService : IAuthenticationService
{
    private string storedUsername = "admin";
    private string storedPassword = "1234";
    private string storedRole = "Admin";

    public bool AuthenticateUser(string username, string password)
    {
        return username == storedUsername && password == storedPassword;
    }

    public bool AuthorizeUser(string username, string role)
    {
        return username == storedUsername && role == storedRole;
    }
}

interface INotificationService
{
    void SendNotification(string recipient, string message);
}

class EmailNotificationService : INotificationService
{
    public void SendNotification(string recipient, string message)
    {
        Console.WriteLine($"Email sent to {recipient}: {message}");
    }
}

class SmsNotificationService : INotificationService
{
    public void SendNotification(string recipient, string message)
    {
        Console.WriteLine($"SMS sent to {recipient}: {message}");
    }
}

class PushNotificationService : INotificationService
{
    public void SendNotification(string recipient, string message)
    {
        Console.WriteLine($"Push notification sent to {recipient}: {message}");
    }
}

class Program
{
    static void Main()
    {
        // Part 2 - Q1: Shapes
        IShape circle = new Circle(5);
        IShape rectangle = new Rectangle(4, 6);
        circle.DisplayShapeInfo();
        rectangle.DisplayShapeInfo();

        // Part 2 - Q2: Authentication
        IAuthenticationService authService = new BasicAuthenticationService();
        Console.WriteLine(authService.AuthenticateUser("admin", "1234"));   // True
        Console.WriteLine(authService.AuthorizeUser("admin", "Admin"));     // True

        // Part 2 - Q3: Notification Services
        INotificationService email = new EmailNotificationService();
        INotificationService sms = new SmsNotificationService();
        INotificationService push = new PushNotificationService();

        email.SendNotification("ahmed@example.com", "Hello from email!");
        sms.SendNotification("01000000000", "Hello from SMS!");
        push.SendNotification("device123", "Hello from push!");
    }
}
