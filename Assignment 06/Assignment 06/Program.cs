using System;

class Program
{
    // Question 5
    static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
            if (number % i == 0) return false;
        return true;
    }

    // Question 6
    static void MinMaxArray(int[] arr, out int min, out int max)
    {
        min = max = arr[0];
        foreach (int num in arr)
        {
            if (num < min) min = num;
            if (num > max) max = num;
        }
    }

    // Question 7
    static long Factorial(int num)
    {
        long result = 1;
        for (int i = 2; i <= num; i++)
            result *= i;
        return result;
    }

    // Question 8
    static string ChangeChar(string input, int position, char newChar)
    {
        if (position < 0 || position >= input.Length) return input;
        char[] chars = input.ToCharArray();
        chars[position] = newChar;
        return new string(chars);
    }

    static void Main()
    {
        #region Q1
        Console.WriteLine("Q1: Value Type by Value vs Reference:");
        void ChangeByValue(int x) { x = 100; }
        void ChangeByRef(ref int x) { x = 100; }

        int a = 5;
        ChangeByValue(a);
        Console.WriteLine("After ChangeByValue: " + a); // Output: 5

        ChangeByRef(ref a);
        Console.WriteLine("After ChangeByRef: " + a); // Output: 100
        #endregion

        #region Q2
        Console.WriteLine("\nQ2: Reference Type by Value vs Reference:");
        void RefByVal(int[] arr) { arr = new int[] { 9, 9 }; }
        void RefByRef(ref int[] arr) { arr = new int[] { 8, 8 }; }

        int[] myArray = { 1, 2 };
        RefByVal(myArray);
        Console.WriteLine("After RefByVal: " + string.Join(",", myArray)); // 1,2

        RefByRef(ref myArray);
        Console.WriteLine("After RefByRef: " + string.Join(",", myArray)); // 8,8
        #endregion

        #region Q3
        Console.WriteLine("\nQ3: Sum and Subtract Function:");
        int Sum(int x, int y) => x + y;
        int Sub(int x, int y) => x - y;

        Console.Write("Enter 1st number: ");
        int n1 = int.Parse(Console.ReadLine());
        Console.Write("Enter 2nd number: ");
        int n2 = int.Parse(Console.ReadLine());
        Console.WriteLine("Sum = " + Sum(n1, n2));
        Console.WriteLine("Subtract = " + Sub(n1, n2));
        #endregion

        #region Q4
        Console.WriteLine("\nQ4: Sum of digits function:");
        int SumDigits(int number)
        {
            int sum = 0;
            while (number != 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }

        Console.Write("Enter a number: ");
        int inputNum = int.Parse(Console.ReadLine());
        Console.WriteLine("Sum of digits = " + SumDigits(inputNum));
        #endregion

        #region Q5
        Console.WriteLine("\nQ5: Prime Check Function:");
        Console.Write("Enter a number: ");
        int primeCheck = int.Parse(Console.ReadLine());
        Console.WriteLine(IsPrime(primeCheck) ? "Prime" : "Not Prime");
        #endregion

        #region Q6
        Console.WriteLine("\nQ6: MinMaxArray Function:");
        int[] sampleArr = { 3, 7, 1, 9, 2 };
        MinMaxArray(sampleArr, out int min, out int max);
        Console.WriteLine("Min = " + min + ", Max = " + max);
        #endregion

        #region Q7
        Console.WriteLine("\nQ7: Factorial Function:");
        Console.Write("Enter a number: ");
        int factInput = int.Parse(Console.ReadLine());
        Console.WriteLine("Factorial = " + Factorial(factInput));
        #endregion

        #region Q8
        Console.WriteLine("\nQ8: ChangeChar Function:");
        Console.Write("Enter string: ");
        string original = Console.ReadLine();
        Console.Write("Enter position to change: ");
        int pos = int.Parse(Console.ReadLine());
        Console.Write("Enter new character: ");
        char newChar = char.Parse(Console.ReadLine());
        Console.WriteLine("Result: " + ChangeChar(original, pos, newChar));
        #endregion
    }
}
