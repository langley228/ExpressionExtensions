using System;
using System.Linq.Expressions;
using ExpressionExtensions;

public static class MergeSamples
{
    public static void Run()
    {
        Console.WriteLine("=== MergeExtensions 範例 ===");

        // 雙參數合併為單參數（將 y 合併到 x，所有 y 變成 x）
        Expression<Func<int, int, bool>> expr1 = (x, y) => x == y;
        var merged1 = expr1.Merge();
        Console.WriteLine(merged1); // x => x == x
        Console.WriteLine(merged1.Compile()(50)); // True
        Console.WriteLine(merged1.Compile()(10)); // True

        // 三參數合併為兩參數（將 c 合併到 b，所有 c 變成 b）
        Expression<Func<int, string, string, bool>> expr2 = (a, b, c) => b == c && a.ToString() == b;
        var merged2 = expr2.Merge<int, string>();
        Console.WriteLine(merged2); // (a, b) => b == b && a.ToString() == b
        Console.WriteLine(merged2.Compile()(5, "5")); // True
        Console.WriteLine(merged2.Compile()(5, "x")); // False

        // 四參數合併為三參數（將 d 合併到 c，所有 d 變成 c）
        Expression<Func<int, string, DateTime, DateTime, bool>> expr3 = (a, b, c, d) => c == d && b.Length == c.Day;
        var merged3 = expr3.Merge<int, string, DateTime>();
        Console.WriteLine(merged3); // (a, b, c) => c == c && b.Length == c.Day
        Console.WriteLine(merged3.Compile()(1, "abc", new DateTime(2024, 6, 21))); // True
        Console.WriteLine(merged3.Compile()(1, "a", new DateTime(2024, 6, 1))); // True

        Console.WriteLine();
    }
}
