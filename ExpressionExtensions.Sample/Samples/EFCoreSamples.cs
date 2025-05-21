using System;
using System.Linq;
using System.Linq.Expressions;
using ExpressionExtensions;

public static class EFCoreSamples
{
    public static void Run()
    {
        Console.WriteLine("=== EF Core + ExpressionExtensions 範例 ===");

        using var db = new SampleDbContext();
        db.Database.EnsureCreated();
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

        Console.WriteLine();
    }
}
