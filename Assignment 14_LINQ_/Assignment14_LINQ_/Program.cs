using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ASSLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            // ============================================
            // LINQ - Element Operators
            // ============================================

            Console.WriteLine("============ LINQ - Element Operators ============\n");

            // 1. Get first Product out of Stock
            Console.WriteLine("1. First Product out of Stock:");
            var firstOutOfStock = ListGenerators.ProductList
                .FirstOrDefault(p => p.UnitsInStock == 0);
            Console.WriteLine(firstOutOfStock);
            Console.WriteLine();

            // 2. Return the first product whose Price > 1000, unless there is no match
            Console.WriteLine("2. First product with Price > 1000:");
            var expensiveProduct = ListGenerators.ProductList
                .FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine(expensiveProduct ?? (object)"No product found");
            Console.WriteLine();

            // 3. Retrieve the second number greater than 5
            Console.WriteLine("3. Second number greater than 5:");
            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondGreaterThan5 = Arr
                .Where(n => n > 5)
                .Skip(1)
                .FirstOrDefault();
            Console.WriteLine(secondGreaterThan5);
            Console.WriteLine();

            // ============================================
            // LINQ - Aggregate Operators
            // ============================================

            Console.WriteLine("============ LINQ - Aggregate Operators ============\n");

            // 1. Count of odd numbers in the array
            Console.WriteLine("1. Count of odd numbers:");
            int[] Arr1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var oddCount = Arr1.Count(n => n % 2 != 0);
            Console.WriteLine($"Odd numbers count: {oddCount}");
            Console.WriteLine();

            // 2. List of customers and how many orders each has
            Console.WriteLine("2. Customers and their order count:");
            var customerOrders = ListGenerators.CustomerList
                .Select(c => new {
                    c.CustomerName,
                    OrderCount = c.Orders?.Length ?? 0
                });
            foreach (var c in customerOrders.Take(5))
                Console.WriteLine($"{c.CustomerName}: {c.OrderCount} orders");
            Console.WriteLine();

            // 3. List of categories and how many products each has
            Console.WriteLine("3. Categories and product count:");
            var categoryProducts = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Category = g.Key,
                    ProductCount = g.Count()
                });
            foreach (var cat in categoryProducts)
                Console.WriteLine($"{cat.Category}: {cat.ProductCount} products");
            Console.WriteLine();

            // 4. Total of numbers in array
            Console.WriteLine("4. Sum of array:");
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var sum = Arr2.Sum();
            Console.WriteLine($"Total: {sum}");
            Console.WriteLine();

            // 5. Total characters of all words in dictionary
            // Note: Create dictionary_english.txt or use File.ReadAllLines("dictionary_english.txt")
            Console.WriteLine("5. Total characters in dictionary:");
            string[] dictionary = File.Exists("dictionary_english.txt")
                ? File.ReadAllLines("dictionary_english.txt")
                : new[] { "apple", "banana", "cherry", "date", "elderberry" };
            var totalChars = dictionary.Sum(w => w.Length);
            Console.WriteLine($"Total characters: {totalChars}");
            Console.WriteLine();

            // 6. Length of shortest word
            Console.WriteLine("6. Shortest word length:");
            var shortestLength = dictionary.Min(w => w.Length);
            Console.WriteLine($"Shortest word length: {shortestLength}");
            Console.WriteLine();

            // 7. Length of longest word
            Console.WriteLine("7. Longest word length:");
            var longestLength = dictionary.Max(w => w.Length);
            Console.WriteLine($"Longest word length: {longestLength}");
            Console.WriteLine();

            // 8. Average length of words
            Console.WriteLine("8. Average word length:");
            var avgLength = dictionary.Average(w => w.Length);
            Console.WriteLine($"Average length: {avgLength:F2}");
            Console.WriteLine();

            // 9. Total units in stock for each product category
            Console.WriteLine("9. Total units in stock per category:");
            var stockPerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Category = g.Key,
                    TotalStock = g.Sum(p => p.UnitsInStock)
                });
            foreach (var cat in stockPerCategory)
                Console.WriteLine($"{cat.Category}: {cat.TotalStock} units");
            Console.WriteLine();

            // 10. Cheapest price in each category
            Console.WriteLine("10. Cheapest price per category:");
            var cheapestPerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Category = g.Key,
                    MinPrice = g.Min(p => p.UnitPrice)
                });
            foreach (var cat in cheapestPerCategory)
                Console.WriteLine($"{cat.Category}: ${cat.MinPrice}");
            Console.WriteLine();

            // 11. Products with cheapest price in each category (using Let)
            Console.WriteLine("11. Cheapest products per category:");
            var cheapestProducts = from p in ListGenerators.ProductList
                                   group p by p.Category into g
                                   let minPrice = g.Min(p => p.UnitPrice)
                                   select new
                                   {
                                       Category = g.Key,
                                       Products = g.Where(p => p.UnitPrice == minPrice)
                                   };
            foreach (var cat in cheapestProducts)
            {
                Console.WriteLine($"{cat.Category}:");
                foreach (var p in cat.Products)
                    Console.WriteLine($"  - {p.ProductName}: ${p.UnitPrice}");
            }
            Console.WriteLine();

            // 12. Most expensive price in each category
            Console.WriteLine("12. Most expensive price per category:");
            var maxPricePerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Category = g.Key,
                    MaxPrice = g.Max(p => p.UnitPrice)
                });
            foreach (var cat in maxPricePerCategory)
                Console.WriteLine($"{cat.Category}: ${cat.MaxPrice}");
            Console.WriteLine();

            // 13. Products with most expensive price in each category
            Console.WriteLine("13. Most expensive products per category:");
            var expensiveProducts = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    MaxPrice = g.Max(p => p.UnitPrice),
                    Products = g.Where(p => p.UnitPrice == g.Max(x => x.UnitPrice))
                });
            foreach (var cat in expensiveProducts)
            {
                Console.WriteLine($"{cat.Category}:");
                foreach (var p in cat.Products)
                    Console.WriteLine($"  - {p.ProductName}: ${p.UnitPrice}");
            }
            Console.WriteLine();

            // 14. Average price per category
            Console.WriteLine("14. Average price per category:");
            var avgPricePerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Category = g.Key,
                    AvgPrice = g.Average(p => p.UnitPrice)
                });
            foreach (var cat in avgPricePerCategory)
                Console.WriteLine($"{cat.Category}: ${cat.AvgPrice:F2}");
            Console.WriteLine();

            // ============================================
            // LINQ - Set Operators
            // ============================================

            Console.WriteLine("============ LINQ - Set Operators ============\n");

            // 1. Unique category names
            Console.WriteLine("1. Unique categories:");
            var uniqueCategories = ListGenerators.ProductList
                .Select(p => p.Category)
                .Distinct();
            foreach (var cat in uniqueCategories)
                Console.WriteLine(cat);
            Console.WriteLine();

            // 2. Unique first letters from products and customers
            Console.WriteLine("2. Unique first letters from products and customers:");
            var productFirstLetters = ListGenerators.ProductList
                .Select(p => p.ProductName[0]);
            var customerFirstLetters = ListGenerators.CustomerList
                .Select(c => c.CustomerName[0]);
            var allFirstLetters = productFirstLetters
                .Union(customerFirstLetters)
                .OrderBy(c => c);
            Console.WriteLine(string.Join(", ", allFirstLetters));
            Console.WriteLine();

            // 3. Common first letters
            Console.WriteLine("3. Common first letters:");
            var commonFirstLetters = productFirstLetters
                .Intersect(customerFirstLetters)
                .OrderBy(c => c);
            Console.WriteLine(string.Join(", ", commonFirstLetters));
            Console.WriteLine();

            // 4. First letters in products but not in customers
            Console.WriteLine("4. Product letters not in customer names:");
            var productOnlyLetters = productFirstLetters
                .Except(customerFirstLetters)
                .Distinct()
                .OrderBy(c => c);
            Console.WriteLine(string.Join(", ", productOnlyLetters));
            Console.WriteLine();

            // 5. Last three characters (with duplicates)
            Console.WriteLine("5. Last 3 characters from all names:");
            var productLast3 = ListGenerators.ProductList
                .Select(p => p.ProductName.Length >= 3 ?
                    p.ProductName.Substring(p.ProductName.Length - 3) : p.ProductName);
            var customerLast3 = ListGenerators.CustomerList
                .Select(c => c.CustomerName.Length >= 3 ?
                    c.CustomerName.Substring(c.CustomerName.Length - 3) : c.CustomerName);
            var allLast3 = productLast3.Concat(customerLast3);
            foreach (var last3 in allLast3.Take(10))
                Console.WriteLine(last3);
            Console.WriteLine();

            // ============================================
            // LINQ - Quantifiers
            // ============================================

            Console.WriteLine("============ LINQ - Quantifiers ============\n");

            // 1. Any word contains 'ei'
            Console.WriteLine("1. Any word contains 'ei':");
            string[] words = { "believe", "relief", "receipt", "field", "their" };
            var hasEI = words.Any(w => w.Contains("ei"));
            Console.WriteLine($"Contains 'ei': {hasEI}");
            Console.WriteLine();

            // 2. Categories with at least one product out of stock
            Console.WriteLine("2. Categories with out of stock products:");
            var categoriesWithOutOfStock = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0))
                .Select(g => new
                {
                    Category = g.Key,
                    Products = g.ToList()
                });
            foreach (var cat in categoriesWithOutOfStock)
            {
                Console.WriteLine($"{cat.Category}:");
                foreach (var p in cat.Products)
                    Console.WriteLine($"  - {p.ProductName} (Stock: {p.UnitsInStock})");
            }
            Console.WriteLine();

            // 3. Categories with all products in stock
            Console.WriteLine("3. Categories with all products in stock:");
            var categoriesAllInStock = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0))
                .Select(g => new
                {
                    Category = g.Key,
                    Products = g.ToList()
                });
            foreach (var cat in categoriesAllInStock)
            {
                Console.WriteLine($"{cat.Category}:");
                foreach (var p in cat.Products)
                    Console.WriteLine($"  - {p.ProductName} (Stock: {p.UnitsInStock})");
            }
            Console.WriteLine();

            // ============================================
            // LINQ - Grouping Operators
            // ============================================

            Console.WriteLine("============ LINQ - Grouping Operators ============\n");

            // 1. Group by remainder when divided by 5
            Console.WriteLine("1. Numbers grouped by remainder (mod 5):");
            List<int> numbers = new List<int>
                { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var groupedByRemainder = numbers
                .GroupBy(n => n % 5)
                .OrderBy(g => g.Key);
            foreach (var group in groupedByRemainder)
            {
                Console.WriteLine($"Numbers with remainder {group.Key} when divided by 5:");
                Console.WriteLine($"  {string.Join(", ", group)}");
            }
            Console.WriteLine();

            // 2. Words grouped by first letter
            Console.WriteLine("2. Words grouped by first letter:");
            string[] dictionaryWords = { "apple", "apricot", "banana", "blueberry",
                "cherry", "cranberry", "date", "dragonfruit" };
            var groupedByFirstLetter = dictionaryWords
                .GroupBy(w => w[0])
                .OrderBy(g => g.Key);
            foreach (var group in groupedByFirstLetter)
            {
                Console.WriteLine($"{group.Key}:");
                Console.WriteLine($"  {string.Join(", ", group)}");
            }
            Console.WriteLine();

            // 3. Group by same characters (custom comparer)
            Console.WriteLine("3. Words with same characters:");
            string[] wordArray = { "from", "salt", "earn", "last", "near", "form" };
            var groupedBySameChars = wordArray
                .GroupBy(w => string.Concat(w.OrderBy(c => c)))
                .Where(g => g.Count() > 1);
            foreach (var group in groupedBySameChars)
            {
                Console.WriteLine(string.Join(", ", group));
            }
            Console.WriteLine();

            Console.WriteLine("\n=== Assignment Complete ===");
            Console.ReadKey();
        }
    }
}