using System;
class Program
{
    static void Main()
    {
        #region Q6
        // Solution for Question 6
        Console.Write("Enter a number: ");
        int n = int.Parse(Console.ReadLine());
        for (int i = 1; i <= n; i++)
            Console.Write(i + (i < n ? ", " : "\n"));
        #endregion

        #region Q7
        // Solution for Question 7
        Console.Write("Enter a number: ");
        int num = int.Parse(Console.ReadLine());
        for (int i = 1; i <= 12; i++)
            Console.Write((num * i) + " ");
        Console.WriteLine();
        #endregion

        #region Q8
        // Solution for Question 8
        Console.Write("Enter a number: ");
        int max = int.Parse(Console.ReadLine());
        for (int i = 2; i <= max; i += 2)
            Console.Write(i + " ");
        Console.WriteLine();
        #endregion

        #region Q9
        // Solution for Question 9
        Console.Write("Enter base number: ");
        int b = int.Parse(Console.ReadLine());
        Console.Write("Enter exponent: ");
        int exp = int.Parse(Console.ReadLine());
        int result = 1;
        for (int i = 0; i < exp; i++)
            result *= b;
        Console.WriteLine("Result: " + result);
        #endregion

        #region Q10
        // Solution for Question 10
        int total = 0;
        Console.WriteLine("Enter marks of 5 subjects:");
        for (int i = 0; i < 5; i++)
            total += int.Parse(Console.ReadLine());
        double average = total / 5.0;
        double percentage = average;
        Console.WriteLine("Total marks = " + total);
        Console.WriteLine("Average Marks = " + average);
        Console.WriteLine("Percentage = " + percentage);
        #endregion

        #region Q11
        // Solution for Question 11
        Console.Write("Enter month number (1-12): ");
        int month = int.Parse(Console.ReadLine());
        int days = month switch
        {
            2 => 28,
            4 or 6 or 9 or 11 => 30,
            _ => 31
        };
        Console.WriteLine("Days in Month: " + days);
        #endregion

        #region Q12
        // Solution for Question 12
        Console.Write("Enter first number: ");
        double a = double.Parse(Console.ReadLine());
        Console.Write("Enter operator (+, -, *, /): ");
        char op = char.Parse(Console.ReadLine());
        Console.Write("Enter second number: ");
        double b = double.Parse(Console.ReadLine());
        double res = op switch
        {
            '+' => a + b,
            '-' => a - b,
            '*' => a * b,
            '/' => b != 0 ? a / b : double.NaN,
            _ => double.NaN
        };
        Console.WriteLine("Result = " + res);
        #endregion

        #region Q13
        // Solution for Question 13
        Console.Write("Enter a string: ");
        string str = Console.ReadLine();
        char[] arr = str.ToCharArray();
        Array.Reverse(arr);
        Console.WriteLine(new string(arr));
        #endregion

        #region Q14
        // Solution for Question 14
        Console.Write("Enter an integer: ");
        int num = int.Parse(Console.ReadLine());
        int rev = 0;
        while (num != 0)
        {
            rev = rev * 10 + num % 10;
            num /= 10;
        }
        Console.WriteLine("Reversed: " + rev);
        #endregion

        #region Q15
        // Solution for Question 15
        Console.Write("Enter start of range: ");
        int start = int.Parse(Console.ReadLine());
        Console.Write("Enter end of range: ");
        int end = int.Parse(Console.ReadLine());
        Console.WriteLine("Prime numbers between " + start + " and " + end + ":");
        for (int i = start; i <= end; i++)
        {
            if (i < 2) continue;
            bool isPrime = true;
            for (int j = 2; j <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
                Console.Write(i + " ");
        }
        Console.WriteLine();
        #endregion

        #region Q17
        // Solution for Question 17
        Console.WriteLine("Enter three points (x1 y1 x2 y2 x3 y3):");
        int x1 = int.Parse(Console.ReadLine());
        int y1 = int.Parse(Console.ReadLine());
        int x2 = int.Parse(Console.ReadLine());
        int y2 = int.Parse(Console.ReadLine());
        int x3 = int.Parse(Console.ReadLine());
        int y3 = int.Parse(Console.ReadLine());
        if ((y2 - y1) * (x3 - x2) == (y3 - y2) * (x2 - x1))
            Console.WriteLine("Points lie on the same straight line.");
        else
            Console.WriteLine("Points do not lie on the same straight line.");
        #endregion

        #region Q18
        // Solution for Question 18
        Console.Write("Enter time taken to complete the task (in hours): ");
        double hours = double.Parse(Console.ReadLine());
        if (hours >= 2 && hours < 3)
            Console.WriteLine("Highly efficient");
        else if (hours >= 3 && hours < 4)
            Console.WriteLine("Improve your speed");
        else if (hours >= 4 && hours <= 5)
            Console.WriteLine("Training required to improve speed");
        else if (hours > 5)
            Console.WriteLine("You are to leave the company");
        #endregion

    }
}
