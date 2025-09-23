# Functional Programming Demo in .NET
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/wesleydeland/FunctionalProgrammingDemo/dotnet.yml)
![GitHub last commit](https://img.shields.io/github/last-commit/wesleydeland/FunctionalProgrammingDemo)

This project demonstrates various functional programming patterns and features in C#/.NET. It serves as a practical example of how to implement functional programming concepts in a traditionally object-oriented language.

## Functional Programming Concepts Demonstrated

### 1. Pure Functions
Pure functions are demonstrated through several examples:
- `Add(x, y)`: A simple pure function that always returns the same output for the same inputs
- `Multiply(x, y)`: Another pure function example showing mathematical operations
- `Factorial(n)`: Recursive pure function demonstrating mathematical computation

Pure functions have no side effects and always produce the same output for the same input, making code more predictable and easier to test.

### 2. Immutability
Immutability is showcased through:
- Record types (`Person` record)
- Immutable data structures (`ImmutableArray`)
- With-expressions for creating modified copies (`person1 with { Age = 31 }`)
- LINQ operations that create new collections instead of modifying existing ones

### 3. Higher-Order Functions
The project demonstrates higher-order functions through:
- Function delegation using `Func<T>`
- Passing functions as parameters
- Returning functions from other functions

### 4. Function Composition
Function composition is demonstrated through:
- `DoubleIt` and `SquareIt` functions
- Composition example in `DoubleThenSquare`
- LINQ method chaining

### 5. Pattern Matching
Modern C# pattern matching features are demonstrated through several examples:

#### Type Pattern Matching
```csharp
public static double CalculateArea(Shape shape) => shape switch
{
    Circle c => Math.PI * c.Radius * c.Radius,
    Rectangle r => r.Width * r.Height,
    Triangle t => 0.5 * t.Base * t.Height,
    null => 0,
    _ => throw new ArgumentException("Unknown shape type", nameof(shape))
};
```

#### Property Pattern Matching
```csharp
public static string ClassifyPerson(Person person) => person switch
{
    { Age: < 13 } => "Child",
    { Age: < 20 } => "Teenager",
    { Age: < 65 } => "Adult",
    _ => "Senior"
};
```

#### Tuple Pattern Matching
```csharp
public static string CompareNumbers(int a, int b) => (a, b) switch
{
    (0, 0) => "Both zero",
    (var x, var y) when x > y => "First is larger",
    (var x, var y) when x < y => "Second is larger",
    _ => "Both are equal"
};
```

### 6. Declarative Programming
The project uses LINQ to demonstrate declarative programming:
- Using `Where` and `Select` for data transformation
- Working with sequences using LINQ methods
- Creating readable, declarative code

## Running the Demo

The program includes examples of each concept with clear console output showing the results. Each example is clearly labeled in the output, making it easy to understand what's being demonstrated.

## Project Structure

- `Program.cs`: Main program file containing all demonstrations
- `FunctionalExamples`: Static class containing pure function examples
- `PatternMatchingExamples`: Static class demonstrating various pattern matching techniques
- `ImmutableExamples`: Static class showing immutability patterns
- `PureFunctionExamples`: Static class with additional pure function demonstrations

## Output Examples

The program produces formatted output showing:
- Results of pure function calculations
- Immutability demonstrations
- Pattern matching results across different scenarios
- Function composition results
- LINQ and declarative programming examples

## Best Practices Demonstrated

1. **Immutability**: Using records and immutable collections
2. **Pure Functions**: Avoiding side effects and external state
3. **Expression-Bodied Members**: Using concise syntax for simple methods
4. **Pattern Matching**: Using modern C# pattern matching features
5. **LINQ**: Using declarative style for collection operations

## Requirements

- .NET 9.0 or later
- Visual Studio 2022 or later / Visual Studio Code with C# extensions
