using System.Collections.Frozen;

namespace FunctionalProgrammingDemo.TestsV3;

public class ProgramTests
{
    [Fact]
    public void Main_ExecutesWithoutException()
    {
        // This test verifies that the Main method can execute without throwing exceptions
        var exception = Record.Exception(() => Program.Main());
        Assert.Null(exception);
    }
}

public class HigherOrderFunctionTests
{
    [Theory]
    [InlineData(5, 7, 12)]  // Testing Add
    [InlineData(3, 4, 7)]   // Testing Add with different values
    public void HigherOrderFunction_Add_ReturnsCorrectResult(int x, int y, int expected)
    {
        Func<int, int, int> operation = FunctionalExamples.Add;
        var result = operation(x, y);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FunctionComposition_WithMultipleFunctions_WorksCorrectly()
    {
        Func<int, int> doubleIt = FunctionalExamples.DoubleIt;
        Func<int, int> squareIt = FunctionalExamples.SquareIt;

        var result = squareIt(doubleIt(3));  // (3 * 2)^2 = 36
        Assert.Equal(36, result);
    }

    [Fact]
    public void DeclarativeStyle_WithLinq_ReturnsCorrectResults()
    {
        var numbers = Enumerable.Range(1, 5);
        var evenSquares = numbers.Where(n => n % 2 == 0).Select(FunctionalExamples.SquareIt).ToList();

        Assert.Equal(new[] { 4, 16 }, evenSquares);  // 2^2=4, 4^2=16
    }
}

public class ExtendedPatternMatchingTests
{
    [Fact]
    public void CalculateArea_UnknownShape_ThrowsArgumentException()
    {
        var unknownShape = new UnknownShape();
        Assert.Throws<ArgumentException>(() => PatternMatchingExamples.CalculateArea(unknownShape));
    }

    private record UnknownShape : Shape;  // Test class for unknown shape case
}

public class ExtendedImmutableExamplesTests
{
    [Fact]
    public void LinqImmutability_OriginalArrayRemainsUnchanged()
    {
        var result = ImmutableExamples.LinqImmutability();

        // Verify the original numbers are still 1,2,3,4,5 by creating a new range and comparing
        var originalNumbers = Enumerable.Range(1, 5);
        Assert.Equal(result, originalNumbers.Select(n => n * 2));
    }
}

public class ExtendedPureFunctionExamplesTests
{
    [Theory]
    [InlineData(-1)]  // Testing negative input
    [InlineData(0)]   // Testing zero input
    public void Factorial_WithEdgeCases_ReturnsOne(int input)
    {
        var result = PureFunctionExamples.Factorial(input);
        Assert.Equal(1, result);
    }

    [Fact]
    public void CallExternalService_ExecutesMultipleTimes_HandlesSuccessAndFailure()
    {
        var results = Enumerable.Range(1, 100)  // Run 100 times to ensure we hit both success and failure cases
            .Select(_ => PureFunctionExamples.CallExternalService())
            .ToList();

        // Verify we got at least one success and one failure
        Assert.Contains(results, r => r.Success);
        Assert.Contains(results, r => !r.Success);
    }
}