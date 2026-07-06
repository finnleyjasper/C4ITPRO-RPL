namespace ShapeCalculator;
/*
 * File: Shape.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 6 July 2026
 *
 * Purpose:
 * Defines the abstract Shape base class used by all concrete shape types.
 * The class stores common shape information and defines the area and
 * perimeter methods that each derived shape class must implement.
 */

/// <summary>
/// Defines the information and behaviour shared by every shape.
/// </summary>
/// <remarks>
/// This is an abstract class because a generic shape does not have enough
/// information to calculate an area or perimeter. Derived classes provide
/// those calculations for their specific shape, along with the shape name.
/// </remarks>
public abstract class Shape
{
    protected int[] _sides;
    private string _name;

    /// <summary>
    /// Initialises a new instance of the <see cref="Shape"/> class.
    /// </summary>
    /// <param name="name">The name used to identify the type of shape.</param>
    /// <param name="sides">The lengths of all sides belonging to the shape.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is blank or no side lengths are supplied.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when a side length is zero or negative.
    /// </exception>

    protected Shape(string name, int[] sides)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name); // Shapes must have a name!
        _name = name;

        if (sides is null || sides.Length == 0)
        {
            throw new ArgumentException("At least one side length must be supplied.");
        }

        if (sides.Any(side => side <= 0)) // Lambda expression checks for any side length that is zero or negative
        {
            throw new ArgumentOutOfRangeException(nameof(sides), "Side lengths must be greater than zero.");
        }

        // Copy the array so Shape's _sides is not a reference
        _sides = (int[])sides.Clone();
    }

    /// <summary>
    /// Calculates the total distance around the outside of the shape.
    /// </summary>
    /// <returns>The perimeter in whole units.</returns>
    public abstract int CalculatePerimeter();

    /// <summary>
    /// Calculates the area inside the shape.
    /// </summary>
    /// <returns>The area in whole square units.</returns>
    public abstract int CalculateArea();

    /// <summary>
    /// Gets the name of the shape.
    /// </summary>
    public string Name => _name;

    /// <summary>
    /// Gets the sides of the shape.
    /// </summary>
    public int[] Sides => _sides;
}
