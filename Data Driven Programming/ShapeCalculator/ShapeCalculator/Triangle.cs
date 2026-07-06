namespace ShapeCalculator;
/*
 * File: Triangle.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 6 July 2026
 *
 * Purpose:
 * Inherits from the Shape base class to provide the area and perimeter calculations for a triangle.
 */

/// <summary>
/// Represents a three-sided triangle.
/// </summary>
public class Triangle : Shape
{
    /// <summary>
    /// Initialises a new triangle with three equal sides of 1.
    /// </summary>
    public Triangle() : this([1, 1, 1])
    {
    }

    /// <summary>
    /// Initialises a new triangle using three side lengths.
    /// </summary>
    /// <param name="sideLengths">The three side lengths of the triangle.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when exactly three side lengths are not supplied.
    /// </exception>
    public Triangle(int[] sideLengths) : base("triangle", sideLengths)
    {
        // A triangle must have exactly three side lengths
        if (sideLengths.Length != 3)
        {
            throw new ArgumentException("A triangle requires exactly three side lengths.");
        }
        if (sideLengths[0] + sideLengths[1] <= sideLengths[2] ||
            sideLengths[0] + sideLengths[2] <= sideLengths[1] ||
            sideLengths[1] + sideLengths[2] <= sideLengths[0])
        {
            throw new ArgumentException("The supplied side lengths do not form a valid triangle.");
        }
    }

    /// <inheritdoc/>
    public override int CalculatePerimeter()
    {
        return _sides.Sum(); // A triangle's perimeter is the sum of its three side lengths
    }

    /// <inheritdoc/>
    public override int CalculateArea()
    {
        return (int)Math.Sqrt(
            CalculatePerimeter() / 2.0 *
            (CalculatePerimeter() / 2.0 - _sides[0]) *
            (CalculatePerimeter() / 2.0 - _sides[1]) *
            (CalculatePerimeter() / 2.0 - _sides[2])); // Heron's formula for triangle area given all three side lengths
    }

}
