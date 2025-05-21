using System;
using System.Linq.Expressions;
using ExpressionExtensions;

public static class BindSamples
{
    public static void Run()
    {
        Console.WriteLine("=== BindExtensions 範例 ===");

        // 雙參數綁定為單參數（將 b 綁定為 5）
        Expression<Func<int, int, bool>> expr1 = (a, b) => a + b > 10;
        var bound1 = expr1.Bind(5);
        Console.WriteLine(bound1); // a => (a + 5) > 10
        Console.WriteLine(bound1.Compile()(6)); // True
        Console.WriteLine(bound1.Compile()(3)); // False

        // 三參數綁定為雙參數（將 c 綁定為 "abc"）
        Expression<Func<int, int, string, bool>> expr2 = (a, b, c) => a.ToString() == c && b > 0;
        var bound2 = expr2.Bind("abc");
        Console.WriteLine(bound2); // (a, b) => (a.ToString() == "abc") && b > 0
        Console.WriteLine(bound2.Compile()(123, 1)); // True
        Console.WriteLine(bound2.Compile()(456, 1)); // False

        // 四參數綁定為三參數（將 d 綁定為 2.0）
        Expression<Func<int, string, DateTime, double, bool>> expr3 = (a, b, c, d) => d > 1.5 && b.Length == a;
        var bound3 = expr3.Bind(2.0);
        Console.WriteLine(bound3); // (a, b, c) => (2.0 > 1.5) && b.Length == a
        Console.WriteLine(bound3.Compile()(3, "abc", DateTime.Now)); // True
        Console.WriteLine(bound3.Compile()(2, "abc", DateTime.Now)); // False

        Console.WriteLine();
    }
}
