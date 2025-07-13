namespace Assignment_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1
            // Solution for Question 1
            Console.Write("Enter a number: ");
            int number = int.Parse(Console.ReadLine());
            if (number % 3 == 0 && number % 4 == 0)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
            #endregion

            #region Q2
            // Solution for Question 2
            Console.Write("Enter an integer: ");
            int num = int.Parse(Console.ReadLine());
            if (num < 0)
                Console.WriteLine("negative");
            else
                Console.WriteLine("positive");
            #endregion

            #region Q3
            // Solution for Question 3
            Console.Write("Enter first number: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Enter second number: ");
            int b = int.Parse(Console.ReadLine());
            Console.Write("Enter third number: ");
            int c = int.Parse(Console.ReadLine());
            int max = Math.Max(a, Math.Max(b, c));
            int min = Math.Min(a, Math.Min(b, c));
            Console.WriteLine("Max element = " + max);
            Console.WriteLine("Min element = " + min);
            #endregion

            #region Q4
            // Solution for Question 4
            Console.Write("Enter a number: ");
            int n = int.Parse(Console.ReadLine());
            if (n % 2 == 0)
                Console.WriteLine("Even");
            else
                Console.WriteLine("Odd");
            #endregion

            #region Q5
            // Solution for Question 5
            Console.Write("Enter a character: ");
            char ch = char.Parse(Console.ReadLine().ToLower());
            if ("aeiou".Contains(ch))
                Console.WriteLine("Vowel");
            else
                Console.WriteLine("Consonant");
            #endregion

        }
    }
}
