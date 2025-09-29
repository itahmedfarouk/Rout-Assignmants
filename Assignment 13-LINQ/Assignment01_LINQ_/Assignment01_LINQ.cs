using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main()
    {
        // Sample data setup
        var products = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Laptop", Category = "Electronics", UnitPrice = 1200, UnitsInStock = 0 },
            new Product { ProductID = 2, ProductName = "Phone", Category = "Electronics", UnitPrice = 800, UnitsInStock = 50 },
            new Product { ProductID = 3, ProductName = "Chair", Category = "Furniture", UnitPrice = 600, UnitsInStock = 10 },
            new Product { ProductID = 4, ProductName = "Table", Category = "Furniture", UnitPrice = 900, UnitsInStock = 5 }
        };

        var arr = new int[] {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};

        // 1. Element Operators
        var outOfStockProduct = products.FirstOrDefault(p => p.UnitsInStock == 0);
        Console.WriteLine("First Product Out of Stock: " + outOfStockProduct?.ProductName);

        var expensiveProduct = products.FirstOrDefault(p => p.UnitPrice > 1000);
        Console.WriteLine("First Product Over $1000: " + expensiveProduct?.ProductName);

        var secondGreaterThanFive = arr.Where(x => x > 5).Skip(1).FirstOrDefault();
        Console.WriteLine("Second number greater than 5: " + secondGreaterThanFive);

        // 2. Aggregate Operators
        Console.WriteLine("Odd numbers count: " + arr.Count(x => x % 2 != 0));

        // 3. Aggregate Operators Continued ...
    }
}
