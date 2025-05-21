using System;
using System.Linq.Expressions;
using ExpressionExtensions;

public static class NotSamples
{
    public static void Run()
    {
        Console.WriteLine("=== NotExtensions 範例 ===");

        // 單參數 NOT
        Expression<Func<int, bool>> expr1 = x => x > 0;
        var not1 = expr1.Not();
        Console.WriteLine(not1); // x => Not(x > 0)
        Console.WriteLine(not1.Compile()(1)); // False
        Console.WriteLine(not1.Compile()(-1)); // True

        // 雙參數 NOT
        Expression<Func<int, string, bool>> expr2 = (x, y) => y.Length == x;
        var not2 = expr2.Not();
        Console.WriteLine(not2); // (x, y) => Not(y.Length == x)
        Console.WriteLine(not2.Compile()(3, "abc")); // True
        Console.WriteLine(not2.Compile()(2, "abc")); // False

        // 三參數 NOT
        Expression<Func<int, string, DateTime, bool>> expr3 = (x, y, z) => y.Length == z.Day;
        var not3 = expr3.Not();
        Console.WriteLine(not3); // (x, y, z) => Not(y.Length == z.Day)
        Console.WriteLine(not3.Compile()(1, "abc", new DateTime(2024, 6, 21))); // True
        Console.WriteLine(not3.Compile()(1, "aa", new DateTime(2024, 6, 2))); // False

        // 四參數 NOT
        Expression<Func<int, string, DateTime, double, bool>> expr4 = (a, b, c, d) => d > 1.5;
        var not4 = expr4.Not();
        Console.WriteLine(not4); // (a, b, c, d) => Not(d > 1.5)
        Console.WriteLine(not4.Compile()(1, "abc", DateTime.Now, 2.0)); // False
        Console.WriteLine(not4.Compile()(1, "abc", DateTime.Now, 1.0)); // True

        Console.WriteLine();
    }
}
