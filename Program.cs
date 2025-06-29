using System;

namespace Assignment2_CSharp_Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Write a program that allows the user to enter a number then print it.
            Console.WriteLine("Enter a number:");
            int userNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"You entered: {userNumber}");
            #endregion

            #region Write C# program that converts a string to an integer, but the string contains non-numeric characters. And mention what will happen
            string input = "123abc";
            try
            {
                int converted = Convert.ToInt32(input);
                Console.WriteLine(converted);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("FormatException occurs because the string contains non-numeric characters.");
            }
            #endregion

            #region Write C# program that Perform a simple arithmetic operation with floating-point numbers And mention what will happen
            float a = 5.5f;
            float b = 2.2f;
            float result = a + b;
            Console.WriteLine($"The result of adding {a} and {b} is {result}");
            // Floating-point arithmetic may result in small precision errors
            #endregion

            #region Write C# program that Extract a substring from a given string.
            string fullString = "Hello C# Developers";
            string subString = fullString.Substring(6, 2);
            Console.WriteLine($"Extracted substring: {subString}");
            #endregion

            #region Write C# program that Assigning one value type variable to another and modifying the value of one variable and mention what will happen
            int x = 10;
            int y = x;
            y = 20;
            Console.WriteLine($"x: {x}, y: {y}");
            // Value types are copied by value, so modifying y does not affect x.
            #endregion

            #region Write C# program that Assigning one reference type variable to another and modifying the object through one variable and mention what will happen
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = arr1;
            arr2[0] = 99;
            Console.WriteLine($"arr1[0]: {arr1[0]}, arr2[0]: {arr2[0]}");
            // Reference types are copied by reference, so modifying arr2 affects arr1.
            #endregion

            #region Write C# program that take two string variables and print them as one variable
            string str1 = "Hello ";
            string str2 = "World";
            string combined = str1 + str2;
            Console.WriteLine(combined);
            #endregion

            #region Which of the following statements is correct about the C#.NET code snippet given below?
            /*
            int d;
            d = Convert.ToInt32(!(30 < 20));
            */
            // The expression !(30 < 20) evaluates to true, which Convert.ToInt32 will convert to 1.
            Console.WriteLine("A value 1 will be assigned to d.");
            #endregion

            #region Which of the following is the correct output for the C# code given below?
            /*
            Console.WriteLine(13 / 2 + " " + 13 % 2);
            */
            // 13 / 2 = 6 (integer division), 13 % 2 = 1
            Console.WriteLine("6 1");
            #endregion

            #region What will be the output of the C# code given below?
            /*
            int num = 1, z = 5;

            if (!(num <= 0))
                Console.WriteLine(++num + z++ + " " + ++z);
            else
                Console.WriteLine(--num + z-- + " " + --z);
            */
            // !(1 <= 0) => true
            // ++num = 2, z++ = 5 (z becomes 6 after)
            // 2 + 5 = 7
            // ++z = 7
            Console.WriteLine("7 7");
            #endregion
        }
    }
}
