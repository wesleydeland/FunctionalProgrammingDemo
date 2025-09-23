namespace FunctionalProgrammingDemo.TestsV3;

public class PatternMatchingTests
{
    [Fact]
    public void CalculateArea_Circle_ReturnsCorrectArea()
    {
        // Arrange
        var circle = new Circle(5);

        // Act
        var area = PatternMatchingExamples.CalculateArea(circle);

        // Assert
        Assert.Equal(Math.PI * 25, area);
    }

    [Fact]
    public void CalculateArea_Rectangle_ReturnsCorrectArea()
    {
        var rectangle = new Rectangle(4, 6);
        var area = PatternMatchingExamples.CalculateArea(rectangle);
        Assert.Equal(24, area);
    }

    [Fact]
    public void CalculateArea_Triangle_ReturnsCorrectArea()
    {
        var triangle = new Triangle(3, 4);
        var area = PatternMatchingExamples.CalculateArea(triangle);
        Assert.Equal(6, area);
    }

    [Fact]
    public void CalculateArea_NullShape_ReturnsZero()
    {
        var area = PatternMatchingExamples.CalculateArea(null);
        Assert.Equal(0, area);
    }

    [Theory]
    [InlineData("Tommy", 10, "Child")]
    [InlineData("Jane", 16, "Teenager")]
    [InlineData("Bob", 35, "Adult")]
    [InlineData("Martha", 70, "Senior")]
    public void ClassifyPerson_ReturnsCorrectClassification(string name, int age, string expected)
    {
        var person = new Person(name, age);
        var classification = PatternMatchingExamples.ClassifyPerson(person);
        Assert.Equal(expected, classification);
    }

    [Theory]
    [InlineData(0, 0, "Both zero")]
    [InlineData(5, 3, "First is larger")]
    [InlineData(2, 7, "Second is larger")]
    [InlineData(4, 4, "Both are equal")]
    public void CompareNumbers_ReturnsCorrectComparison(int a, int b, string expected)
    {
        var result = PatternMatchingExamples.CompareNumbers(a, b);
        Assert.Equal(expected, result);
    }
}

public class FunctionalExamplesTests
{
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-1, 1, 0)]
    [InlineData(0, 0, 0)]
    public void Add_ReturnsSumOfNumbers(int x, int y, int expected)
    {
        var result = FunctionalExamples.Add(x, y);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, 4)]
    [InlineData(0, 0)]
    [InlineData(-3, -6)]
    public void DoubleIt_ReturnsDoubleTheNumber(int input, int expected)
    {
        var result = FunctionalExamples.DoubleIt(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, 4)]
    [InlineData(3, 9)]
    [InlineData(-2, 4)]
    public void SquareIt_ReturnsSquareOfNumber(int input, int expected)
    {
        var result = FunctionalExamples.SquareIt(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, 16)]  // (2 * 2)^2 = 16
    [InlineData(3, 36)]  // (3 * 2)^2 = 36
    [InlineData(-2, 16)] // (-2 * 2)^2 = 16
    public void DoubleThenSquare_ReturnsCorrectResult(int input, int expected)
    {
        var result = FunctionalExamples.DoubleThenSquare(input);
        Assert.Equal(expected, result);
    }
}

public class ImmutableExamplesTests
{
    [Theory]
    [InlineData(2, 3, 121)]  // (2+3 + 2*3)^2 = (5 + 6)^2 = 11^2 = 121
    [InlineData(1, 1, 9)]   // (1+1 + 1*1)^2 = (2 + 1)^2 = 3^2 = 9
    public void CalculateNonsense_ReturnsCorrectResult(int a, int b, int expected)
    {
        var result = ImmutableExamples.CalculateNonsense(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void LinqImmutability_ReturnsDoubledValues()
    {
        var result = ImmutableExamples.LinqImmutability();
        Assert.Equal(new[] { 2, 4, 6, 8, 10 }, result);
    }
}

public class PureFunctionExamplesTests
{
    [Theory]
    [InlineData(4, 5, 20)]
    [InlineData(0, 5, 0)]
    [InlineData(-2, 3, -6)]
    public void Multiply_ReturnsCorrectProduct(int x, int y, int expected)
    {
        var result = PureFunctionExamples.Multiply(x, y);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(3, 6)]
    [InlineData(5, 120)]
    public void Factorial_ReturnsCorrectResult(int n, int expected)
    {
        var result = PureFunctionExamples.Factorial(n);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CallExternalService_ReturnsResult()
    {
        var result = PureFunctionExamples.CallExternalService();
        Assert.NotNull(result);
        Assert.IsType<Result>(result);
    }
}

public class ResultTests
{
    [Fact]
    public void Successful_CreatesSuccessfulResult()
    {
        var message = "Operation succeeded";
        var result = Result.Successful(message);

        Assert.True(result.Success);
        Assert.Equal(message, result.Message);
    }

    [Fact]
    public void Failed_CreatesFailedResult()
    {
        var message = "Operation failed";
        var result = Result.Failed(message);

        Assert.False(result.Success);
        Assert.Equal(message, result.Message);
    }
}
