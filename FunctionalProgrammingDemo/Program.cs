using System.Collections.Frozen;
using System.Collections.Immutable;

namespace FunctionalProgrammingDemo;

public record Person(string Name, int Age);

// Shape hierarchy for pattern matching examples
public abstract record Shape;
public record Circle(double Radius) : Shape;
public record Rectangle(double Width, double Height) : Shape;
public record Triangle(double Base, double Height) : Shape;

public static class PatternMatchingExamples
{
    // Switch expression with pattern matching
    public static double CalculateArea(Shape shape) => shape switch
    {
        Circle c => Math.PI * c.Radius * c.Radius,
        Rectangle r => r.Width * r.Height,
        Triangle t => 0.5 * t.Base * t.Height,
        null => 0,
        _ => throw new ArgumentException("Unknown shape type", nameof(shape))
    };

    // Property pattern matching
    public static string ClassifyPerson(Person person) => person switch
    {
        { Age: < 13 } => "Child",
        { Age: < 20 } => "Teenager",
        { Age: < 65 } => "Adult",
        _ => "Senior"
    };

    // Tuple pattern matching
    public static string CompareNumbers(int a, int b) => (a, b) switch
    {
        (0, 0) => "Both zero",
        (var x, var y) when x > y => "First is larger",
        (var x, var y) when x < y => "Second is larger",
        _ => "Both are equal"
    };
}

public static class FunctionalExamples
{
    // Pure Function Example
    public static int Add(int x, int y) => x + y;

    // Function Composition Example
    public static int DoubleIt(int x) => x * 2;
    public static int SquareIt(int x) => x * x;
    public static int DoubleThenSquare(int x) => SquareIt(DoubleIt(x));
}

public class Program
{
    public static void Main()
    {
        // Higher-Order Function Example
        Func<int, int, int> operation = FunctionalExamples.Add;

        // Function Composition Example
        Func<int, int> doubleIt = FunctionalExamples.DoubleIt;
        Func<int, int> squareIt = FunctionalExamples.SquareIt;
        Func<int, int> doubleThenSquare = FunctionalExamples.DoubleThenSquare;

        // Declarative Style Example (LINQ)
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
        Console.WriteLine("Immutable Calculation: CalculateEquation(2, 3) = " + ImmutableExamples.CalculateSquare(2, 3));

        Console.WriteLine("Linq Immutability Example:");
        ImmutableExamples.LinqImmutability();

        // Pure Function Examples
        Console.WriteLine("Pure Function Example: Multiply(4, 5) = " + PureFunctionExamples.Multiply(4, 5));

        Console.WriteLine("Pure Function Example: Factorial(5) = " + PureFunctionExamples.Factorial(5));

        var iterations = Enumerable.Range(1, 10);

        iterations.Select(_ => PureFunctionExamples.CallExternalService())
            .ToList();

        // Pattern Matching Examples
        Console.WriteLine("\nPattern Matching Examples:");

        // Shape pattern matching
        Shape[] shapes =
        {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4)
        };

        shapes.Select(s =>
        {
            var area = PatternMatchingExamples.CalculateArea(s);
            Console.WriteLine($"Area of {s.GetType().Name}: {area:F2}");
            return area;
        }).ToList();

        // Person classification pattern matching
        var people = new[]
        {
            new Person("Tommy", 10),
            new Person("Jane", 16),
            new Person("Bob", 35),
            new Person("Martha", 70)
        };

        people.Select(person =>
        {
            var classification = PatternMatchingExamples.ClassifyPerson(person);
            Console.WriteLine($"{person.Name} is a {classification}");
            return classification;
        }).ToList();

        // Number comparison pattern matching
        var numberPairs = new[] { (0, 0), (5, 3), (2, 7), (4, 4) };
        numberPairs.Select(pair =>
        {
            var comparison = PatternMatchingExamples.CompareNumbers(pair.Item1, pair.Item2);
            Console.WriteLine($"Comparing {pair.Item1} and {pair.Item2}: {comparison}");
            return comparison;
        }).ToList();
    }
}

internal static class ImmutableExamples
{
    internal static int CalculateSquare(int a, int b)
    {
        // Immutable variables
        int sum = a + b;
        int product = a * b;
        int squared = product * product;
        return squared;
    }

    internal static IEnumerable<int> LinqImmutability()
    {
        ImmutableArray<int> numbers = [.. Enumerable.Range(1, 5)];
        var doubled = numbers.Select(n => n * 2).ToFrozenSet();
        Console.WriteLine("Original: " + string.Join(", ", numbers));
        Console.WriteLine("Doubled: " + string.Join(", ", doubled));

        return doubled;
    }
}

internal static class PureFunctionExamples
{
    internal static int Multiply(int x, int y) => x * y;
    internal static int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);

    /// <summary>
    /// Simulates calling an external service that may fail randomly.
    /// This function is not pure because it relies on randomness and has side effects (console output).
    /// It is included here to demonstrate handling failures in a functional style.
    /// </summary>
    /// <returns>Result of the operation</returns>
    internal static Result CallExternalService()
    {
        try
        {
            var rand = new Random();
            int randomNumber = rand.Next(1, 1001);

            //this is non-functional, but that is OKAY
            // C# is a hybrid language and not everything needs to be functional
            if (randomNumber % 2 != 0)
                throw new Exception("Random number is odd, operation failed");

            Console.WriteLine("Random number is even, operation succeeded");
            return Result.Successful("Operation completed successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: {0}", ex.Message);
            return Result.Failed("Operation failed");
        }
    }
}

internal record Result(bool Success, string Message)
{
    internal static Result Successful(string message) => new(true, message);
    internal static Result Failed(string message) => new(false, message);
}
