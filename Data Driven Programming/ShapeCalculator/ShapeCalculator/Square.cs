namespace ShapeCalculator;
/*
 * File: Square.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 6 July 2026
 *
 * Purpose:
 * Inherits from the Shape base class to provide the area and perimeter calculations for a square.
 */

/// <summary>
/// Represents a square whose four sides have the same length.
/// </summary>
public class Square : Shape
{
    /// <summary>
    /// Initialises a new square whose sides all have the length of 1.
    /// </summary>
    public Square() : this([1])
    {
    }

    /// <summary>
    /// Initialises a new square using one given length for all four sides.
    /// </summary>
    /// <param name="sideLengths">
    /// An array containing exactly one value - the length of every side.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when exactly one side length is not supplied.
    /// </exception>
    public Square(int[] sideLengths) : base("square", sideLengths)
    {
        // A square only needs one supplied value because all four sides are equal!
        if (sideLengths.Length != 1)
        {
            throw new ArgumentException("A square requires exactly one side length, as all four lengths are the same.");
        }
    }

    /// <inheritdoc/>
    public override int CalculatePerimeter()
    {
        return _sides[0] * 4; // A square has four equal sides, so multiply the single side length by 4
    }

    /// <inheritdoc/>
    public override int CalculateArea()
    {
        return _sides[0] * _sides[0]; // A square's area is the length of one side squared
    }

}
