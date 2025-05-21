using System;
using System.Linq.Expressions;
using ExpressionExtensions;

public static class ExpandSamples
{
    public static void Run()
    {
        Console.WriteLine("=== ExpandExtensions 範例 ===");

        // 單參數擴展為雙參數
        Expression<Func<int, bool>> expr1 = x => x > 10;
        var expanded1 = expr1.Expand<int, string>();
        Console.WriteLine(expanded1); // (x, x_1) => x > 10
        Console.WriteLine(expanded1.Compile()(15, "")); // True
        Console.WriteLine(expanded1.Compile()(5, ""));  // False

        // 雙參數擴展為三參數
        Expression<Func<int, int, bool>> expr2 = (x, y) => x > y;
        var expanded2 = expr2.Expand<int, int, int>();
        Console.WriteLine(expanded2); // (x, y, y_2) => x > y
        Console.WriteLine(expanded2.Compile()(11, 9, 0)); // True
        Console.WriteLine(expanded2.Compile()(9, 10, 0)); // False

        // 三參數擴展為四參數
        Expression<Func<int, int, int, bool>> expr3 = (x, y, z) => x + y > z;
        var expanded3 = expr3.Expand<int, int, int, int>();
        Console.WriteLine(expanded3); // (x, y, z, z_3) => x + y > z
        Console.WriteLine(expanded3.Compile()(1, 2, 2, 0)); // True
        Console.WriteLine(expanded3.Compile()(1, 2, 5, 0)); // False

        Console.WriteLine();
    }
}
