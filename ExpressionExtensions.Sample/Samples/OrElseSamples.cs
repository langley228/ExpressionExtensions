using System;
using System.Linq.Expressions;
using ExpressionExtensions;

public static class OrElseSamples
{
    public static void Run()
    {
        Console.WriteLine("=== OrElseExtensions 範例 ===");

        // 單參數 OrElse
        Expression<Func<int, bool>> expr1 = x => x < 0;
        Expression<Func<int, bool>> expr2 = x => x > 100;
        var combined1 = expr1.OrElse(expr2);
        Console.WriteLine(combined1); // x => ((x < 0) || (x > 100))
        Console.WriteLine(combined1.Compile()(-5)); // True
        Console.WriteLine(combined1.Compile()(50)); // False

        // 雙參數 OrElse
        Expression<Func<int, string, bool>> expr3 = (x, y) => x < 0;
        Expression<Func<int, string, bool>> expr4 = (x, y) => y == "ok";
        var combined2 = expr3.OrElse(expr4);
        Console.WriteLine(combined2); // (x, y) => ((x < 0) || (y == "ok"))
        Console.WriteLine(combined2.Compile()(-1, "no")); // True
        Console.WriteLine(combined2.Compile()(1, "ok")); // True
        Console.WriteLine(combined2.Compile()(1, "no")); // False

        // 三參數 OrElse
        Expression<Func<int, string, DateTime, bool>> expr5 = (a, b, c) => a > 0;
        Expression<Func<int, string, DateTime, bool>> expr6 = (a, b, c) => c.Year > 2000;
        var combined3 = expr5.OrElse(expr6);
        Console.WriteLine(combined3); // (a, b, c) => ((a > 0) || (c.Year > 2000))
        Console.WriteLine(combined3.Compile()(1, "abc", new DateTime(1999, 1, 1))); // True
        Console.WriteLine(combined3.Compile()(0, "abc", new DateTime(2024, 1, 1))); // True
        Console.WriteLine(combined3.Compile()(0, "abc", new DateTime(1999, 1, 1))); // False

        // 四參數 OrElse
        Expression<Func<int, string, DateTime, double, bool>> expr7 = (a, b, c, d) => a > 0;
        Expression<Func<int, string, DateTime, double, bool>> expr8 = (a, b, c, d) => d > 1.5;
        var combined4 = expr7.OrElse(expr8);
        Console.WriteLine(combined4); // (a, b, c, d) => ((a > 0) || (d > 1.5))
        Console.WriteLine(combined4.Compile()(1, "abc", DateTime.Now, 1.0)); // True
        Console.WriteLine(combined4.Compile()(0, "abc", DateTime.Now, 2.0)); // True
        Console.WriteLine(combined4.Compile()(0, "abc", DateTime.Now, 1.0)); // False

        Console.WriteLine();
    }
}
