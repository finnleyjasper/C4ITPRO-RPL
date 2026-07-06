namespace ShapeCalculator;
/*
 * File: Rectangle.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 6 July 2026
 *
 * Purpose:
 * Inherits from the Shape base class to provide the area and perimeter calculations for a rectangle.
 */

/// <summary>
/// Represents a four-sided rectangle.
/// </summary>
public class Rectangle : Shape
{
    /// <summary>
    /// Initialises a new rectangle with a length of 1 and a width of 2.
    /// </summary>
    public Rectangle() : this([1, 2])
    {
    }

    /// <summary>
    /// Initialises a new rectangle using its length and width.
    /// </summary>
    /// <param name="sideLengths">
    /// Two values containing the rectangle's length and width.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when exactly two side lengths are not supplied, or both side lengths are the same.
    /// </exception>
    public Rectangle(int[] sideLengths) : base("rectangle", sideLengths)
    {
        // A rectangle must have one length and one width
        if (sideLengths.Length != 2)
        {
            throw new ArgumentException("A rectangle requires exactly two lengths: its length and width.");
        }

        // Equal dimensions would make this a square, rather than a rectangle
        if (sideLengths[0] == sideLengths[1])
        {
            throw new ArgumentException("A rectangle's length and width must be different. Use a Square for equal lengths!");
        }
    }

    /// <inheritdoc/>
    public override int CalculatePerimeter()
    {
        return (_sides[0] + _sides[1]) * 2; // A rectangle's perimeter is the sum of its length and width, multiplied by 2
    }

    /// <inheritdoc/>
    public override int CalculateArea()
    {
        return _sides[0] * _sides[1]; // A rectangle's area is its length multiplied by its width
    }

}
