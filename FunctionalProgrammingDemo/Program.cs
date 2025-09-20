// Functional Programming Demo in .NET
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

public record Person(string Name, int Age);

public static class FunctionalExamples
{
    // 1. Pure Function Example
    public static int Add(int x, int y) => x + y;

    // 4. Function Composition Example
    public static int DoubleIt(int x) => x * 2;
    public static int SquareIt(int x) => x * x;
    public static int DoubleThenSquare(int x) => SquareIt(DoubleIt(x));
}

public class Program
{
    public static void Main()
    {
        // 3. Higher-Order Function Example
        Func<int, int, int> operation = FunctionalExamples.Add;

        // 4. Function Composition Example
        Func<int, int> doubleIt = FunctionalExamples.DoubleIt;
        Func<int, int> squareIt = FunctionalExamples.SquareIt;
        Func<int, int> doubleThenSquare = FunctionalExamples.DoubleThenSquare;

        // 5. Declarative Style Example (LINQ)
        IEnumerable<int> numbers = Enumerable.Range(1, 5);
        var evenSquares = numbers.Where(n => n % 2 == 0).Select(squareIt);

        var doubled = doubleIt.Invoke(4);
        Console.WriteLine("DoubleIt(4) = " + doubled);

        Console.WriteLine("Pure Function: Add(2, 3) = " + FunctionalExamples.Add(2, 3));

        var person1 = new Person("Alice", 30);
        // Immutability: creating a new record with modified value
        var person2 = person1 with { Age = 31 };
        Console.WriteLine($"Immutability: {person1.Name} age {person1.Age} -> {person2.Name} age {person2.Age}");

        Console.WriteLine("Higher-Order Function: operation(5, 7) = " + operation(5, 7));

        Console.WriteLine("Function Composition: doubleThenSquare(3) = " + doubleThenSquare(3));

        Console.WriteLine("Declarative Style (LINQ): even squares = [" + string.Join(", ", evenSquares) + "]");

        // Immutable Examples
        Console.WriteLine("Immutable Calculation: CalculateEquation(2, 3) = " + ImmutableExamples.CalculateEquation(2, 3));

        Console.WriteLine("Linq Immutability Example:");
        ImmutableExamples.LinqImmutability();

        // Pure Function Examples
        Console.WriteLine("Pure Function Example: Multiply(4, 5) = " + PureFunctionExamples.Multiply(4, 5));

        Console.WriteLine("Pure Function Example: Factorial(5) = " + PureFunctionExamples.Factorial(5));

        var iterations = Enumerable.Range(1, 100);
        foreach (var item in iterations)
        {
            var result = PureFunctionExamples.CallExternalService();
            Console.WriteLine($"CallExternalService Result: Success = {result.Success}, Message = '{result.Message}'");

        }
    }
}

internal static class ImmutableExamples
{
    internal static int CalculateEquation(int a, int b)
    {
        // Immutable variables
        int sum = a + b;
        int product = a * b;
        int squared = product * product;
        return squared;
    }

    internal static void LinqImmutability()
    {
        ImmutableArray<int> numbers = [1, 2, 3, 4, 5];
        var doubled = numbers.Select(n => n * 2).ToList(); // Creates a new list
        Console.WriteLine("Original: " + string.Join(", ", numbers));
        Console.WriteLine("Doubled: " + string.Join(", ", doubled));
    }
}

internal static class PureFunctionExamples
{
    internal static int Multiply(int x, int y) => x * y;
    internal static int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);

    internal static Result CallExternalService()
    {
        var result = new Result();
        try
        {
            var rand = new Random();
            int randomNumber = rand.Next(1, 1001);
            if (randomNumber % 2 != 0)
                throw new Exception("Random number is odd, operation failed");

            result.SetSuccess("Operation completed successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: {0}", ex.Message);
            result.SetSuccess("Operation failed", false);
        }

        return result;
    }
}

internal class Result
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;

    public void SetSuccess(string message, bool isSuccess = true)
    {
        Success = isSuccess;
        Message = message;
    }
}
