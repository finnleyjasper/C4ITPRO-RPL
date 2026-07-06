namespace ShapeCalculator;
/*
 * File: ShapeCalculator.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 6 July 2026
 *
 * Purpose:
 * Allows the creation, storage and calculation of shapes.
 *
 * The class can create a shape from its name and side lengths, store it in a collection,
 * and calculate its area or perimeter.
 */

/// <summary>
/// Creates, stores, finds and performs calculations for shapes.
/// </summary>
public sealed class ShapeCalculator
{
    private List<Shape> _shapes = [];
    private Shape? _selectedShape;

    /// <summary>
    /// Creates a supported shape and adds it to the calculator's collection.
    /// </summary>
    /// <param name="shapeName">
    /// The shape type. Supported values are Rectangle, Triangle and Square.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the shape name is unsupported.
    /// </exception>
    /// <returns>The name of the shape that was chosen.</returns>
    public bool ShapeNameValid(string shapeName)
    {
        // Ensure values are not null or whitespace
        ArgumentException.ThrowIfNullOrWhiteSpace(shapeName);

        // A case-insensitive comparison cause Rectangle & rEcTaNgLe should both work
        shapeName = shapeName.Trim().ToLowerInvariant();

        if (shapeName != "rectangle" && shapeName != "triangle" && shapeName != "square")
        {
            throw new ArgumentException($"The shape name '{shapeName}' is not supported, please choose Rectangle, Square or Triangle.");
        }
        else
        {
            return true; // The shape name is valid and supported. Yippee!
        }
    }

    /// <summary>
    /// Adds a shape to the calculator's collection using its name and side lengths.
    /// </summary>
    /// <param name="shapeName">The name of the shape to add.</param>
    /// <param name="sideLengths">An array of the shape's side lengths.</param>
    public void AddShape(string shapeName, int[] sideLengths)
    {
        // Bulletproofing
        ArgumentException.ThrowIfNullOrWhiteSpace(shapeName);
        ArgumentNullException.ThrowIfNull(sideLengths);

        Shape shape;

        switch (shapeName.Trim().ToLowerInvariant()) // Clean the name again
        {
            case "rectangle":
                shape = new Rectangle(sideLengths);
                break;
            case "triangle":
                shape = new Triangle(sideLengths);
                break;
            case "square":
                shape = new Square(sideLengths);
                break;
            default:
                throw new ArgumentException($"The shape name '{shapeName}' is not supported, please choose Rectangle, Square or Triangle.");
        }

        _shapes.Add(shape);
        Console.WriteLine($"Successfully added a {shape.Name} with side length(s): {string.Join(", ", shape.Sides)}");
    }

    /// <summary>
    /// Lists all the shapes to the console, including their names and side lengths.
    /// </summary>
    public void ListShapes()
    {
        if (_shapes.Count == 0)
        {
            Console.WriteLine("No shapes have been added yet.");
            return;
        }

        Console.WriteLine("Shapes in the calculator:");
        int i = 1;
        foreach (var shape in _shapes)
        {
            Console.WriteLine($"{i}. {shape.Name} - Side length(s): {string.Join(", ", shape.Sides)}");
            i++;
        }
    }

    /// <summary>
    /// Selects a shape from the calculator's list by index +1.
    /// Designed to be used with the ListShapes() method, which lists shapes to the user starting from 1.
    /// </summary>
    /// <param name="shapeIndex">The index of the shape to select.</param>
    public void SelectShape(int shapeIndex)
    {
        if (shapeIndex < 1 || shapeIndex > _shapes.Count)
        {
            throw new ArgumentOutOfRangeException("Shape index is out of range.");
        }

        _selectedShape = _shapes[shapeIndex - 1];
    }


    /// <summary>
    /// Calculates the perimeter of the selected shape.
    /// </summary>
    /// <returns>The perimeter in whole units.</returns>
    public int CalculateShapePerimeter()
    {
        if (_selectedShape == null)
        {
            throw new InvalidOperationException("No shape has been selected. Please select a shape before calculating its perimeter.");
        }
        return _selectedShape.CalculatePerimeter();
    }

    /// <summary>
    /// Calculates the area of the selected shape.
    /// </summary>
    /// <returns>The area in whole square units.</returns>
    public int CalculateShapeArea()
    {
        if (_selectedShape == null)
        {
            throw new InvalidOperationException("No shape has been selected. Please select a shape before calculating its area.");
        }
        return _selectedShape.CalculateArea();
    }

    /// <summary>
    /// Gets the currently selected shape or null if one is not selected.
    /// </summary>
    public Shape? SelectedShape => _selectedShape;

    /// <summary>
    /// Gets the list of all shapes in the calculator.
    /// </summary>
    public List<Shape> Shapes => _shapes;
}
