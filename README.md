# ExpressionExtensions

## 介紹

ExpressionExtensions 提供多種 Lambda Expression 的組合、參數操作與擴充功能，讓你能更靈活地進行動態查詢條件組合、參數綁定、參數擴展、參數合併與條件反向等操作。  
這些功能特別適合用於 LINQ、EF Core 等需要動態產生查詢條件的情境，讓程式碼更簡潔、可讀性更高，也更容易維護。

---

## 功能範例

### AndAlso 合併（AND 條件組合）

將多個條件以 AND（&&）邏輯合併為單一 Lambda 表達式。

```csharp
Expression<Func<int, bool>> expr1 = x => x > 0;
Expression<Func<int, bool>> expr2 = x => x < 100;
var combined = expr1.AndAlso(expr2);
// x => ((x > 0) && (x < 100))
Console.WriteLine(combined.Compile()(50)); // True
Console.WriteLine(combined.Compile()(150)); // False
```

#### 多參數範例

```csharp
Expression<Func<int, string, bool>> expr3 = (x, y) => x > 0;
Expression<Func<int, string, bool>> expr4 = (x, y) => y.Length > 2;
var combined2 = expr3.AndAlso(expr4);
// (x, y) => ((x > 0) && (y.Length > 2))
```

### OrElse 合併（OR 條件組合）

將多個條件以 OR（||）邏輯合併為單一 Lambda 表達式。

```csharp
Expression<Func<int, bool>> expr1 = x => x < 0;
Expression<Func<int, bool>> expr2 = x => x > 100;
var combined = expr1.OrElse(expr2);
// x => ((x < 0) || (x > 100))
Console.WriteLine(combined.Compile()(-5)); // True
Console.WriteLine(combined.Compile()(50)); // False
```

#### 多參數範例

```csharp
Expression<Func<int, string, bool>> expr3 = (x, y) => x < 0;
Expression<Func<int, string, bool>> expr4 = (x, y) => y == "ok";
var combined2 = expr3.OrElse(expr4);
// (x, y) => ((x < 0) || (y == "ok"))
```

### Not 反向（條件取反）

產生原 Lambda 條件的邏輯相反（NOT）表達式。

```csharp
Expression<Func<int, bool>> expr = x => x > 0;
var notExpr = expr.Not();
// x => Not(x > 0)
Console.WriteLine(notExpr.Compile()(1)); // False
Console.WriteLine(notExpr.Compile()(-1)); // True
```

#### 多參數範例

```csharp
Expression<Func<int, string, bool>> expr2 = (x, y) => y.Length == x;
var not2 = expr2.Not();
// (x, y) => Not(y.Length == x)
```

### Bind 參數綁定（Partial Application）

將 Lambda 的最後一個參數綁定為指定值，產生參數數量更少的新 Lambda（部分應用）。

```csharp
Expression<Func<int, int, bool>> expr = (a, b) => a + b > 10;
var bound = expr.Bind(5);
// a => (a + 5) > 10
Console.WriteLine(bound.Compile()(6)); // True
Console.WriteLine(bound.Compile()(3)); // False
```

#### 多參數範例

```csharp
Expression<Func<int, int, string, bool>> expr2 = (a, b, c) => a.ToString() == c && b > 0;
var bound2 = expr2.Bind("abc");
// (a, b) => (a.ToString() == "abc") && b > 0
```

### Expand 參數擴展

將 Lambda 參數數量擴展（增加一個參數），但主體不會用到新參數，常用於參數對齊。

```csharp
Expression<Func<int, bool>> expr = x => x > 10;
var expanded = expr.Expand<int, string>();
// (x, x_1) => x > 10
Console.WriteLine(expanded.Compile()(15, "")); // True
Console.WriteLine(expanded.Compile()(5, ""));  // False
```

#### 多參數範例

```csharp
Expression<Func<int, int, bool>> expr2 = (x, y) => x > y;
var expanded2 = expr2.Expand<int, int, int>();
// (x, y, y_2) => x > y
```

### Merge 參數合併

將 Lambda 的最後兩個參數合併為同一個（例如 y 變成 x），產生參數數量更少的新 Lambda。

```csharp
Expression<Func<int, int, bool>> expr = (x, y) => x == y;
var merged = expr.Merge();
// x => x == x
Console.WriteLine(merged.Compile()(50)); // True
Console.WriteLine(merged.Compile()(10)); // True
```

#### 多參數範例

```csharp
Expression<Func<int, string, string, bool>> expr2 = (a, b, c) => b == c && a.ToString() == b;
var merged2 = expr2.Merge<int, string>();
// (a, b) => b == b && a.ToString() == b
```

---

## EF Core 範例

結合 ExpressionExtensions 與 Entity Framework Core，實現動態查詢條件組合：

```csharp
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ExpressionExtensions;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

public class SampleDbContext : DbContext
{
    public DbSet<Person> People => Set<Person>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase("TestDb");
}

class Program
{
    static void Main()
    {
        using var db = new SampleDbContext();
        db.People.AddRange(
            new Person { Name = "Alice", Age = 20 },
            new Person { Name = "Bob", Age = 30 },
            new Person { Name = "Carol", Age = 40 }
        );
        db.SaveChanges();

        // 建立條件運算式
        Expression<Func<Person, bool>> ageGt20 = p => p.Age > 20;
        Expression<Func<Person, bool>> nameIsBob = p => p.Name == "Bob";
        var combined = ageGt20.AndAlso(nameIsBob);

        // 查詢
        var result = db.People.Where(combined).ToList();
        foreach (var person in result)
        {
            Console.WriteLine($"{person.Name} ({person.Age})");
        }
        // 輸出：Bob (30)
    }
}
```