using ShapeCalculator;

namespace ShapeCalculatorUnitTest;

/// <summary>
/// Contains unit tests for the public behaviour of the shape calculator.
/// </summary>
[TestClass]
public sealed class ShapeCalculatorTests
{
    /// <summary>
    /// Confirms that each concrete shape supports construction without arguments.
    /// </summary>
    [TestMethod]
    public void ParameterlessConstructors_CreateShapesWithExpectedNames()
    {
        // Arrange and act: create each shape using its reasonable defaults.
        Shape[] shapes = [new Square(), new Rectangle(), new Triangle()];

        // Assert: each object was initialised with its correct type name.
        CollectionAssert.AreEqual(
            new[] { "Square", "Rectangle", "Triangle" },
            shapes.Select(shape => shape.Name).ToArray());
    }

    /// <summary>
    /// Confirms that a supported shape can be added and found by name.
    /// </summary>
    [TestMethod]
    public void AddShape_SquareCanBeFoundByName()
    {
        // Arrange: create an empty calculator.
        var calculator = new ShapeCalculator.ShapeCalculator();

        // Act: add the square and select the first item in the list.
        calculator.AddShape("Square", [5]);
        calculator.SelectShape(1);

        // Assert: the selected object has the expected shape name.
        Assert.AreEqual("Square", calculator.SelectedShape?.Name);
    }

    /// <summary>
    /// Confirms that constructors reject an incorrect number of supplied lengths.
    /// </summary>
    [TestMethod]
    public void ParameterisedConstructors_InvalidLengthCountsThrowArgumentException()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new Square([2, 2]));
        Assert.ThrowsExactly<ArgumentException>(() => new Triangle([2, 2]));
        Assert.ThrowsExactly<ArgumentException>(() => new Rectangle([2, 2, 2]));
    }

    /// <summary>
    /// Confirms that equal rectangle dimensions are rejected as a square.
    /// </summary>
    [TestMethod]
    public void Rectangle_EqualLengthAndWidthThrowsArgumentException()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new Rectangle([2, 2]));
    }
}
