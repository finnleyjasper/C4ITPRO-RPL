namespace ShapeCalculator;
/*
 * File: Circle.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 22 July 2026
 *
 * Purpose:
 * Inherits from the Shape base class to provide the area and perimeter calculations for a circle.
 */

/// <summary>
/// Represents a circle defined by its radius.
/// </summary>
public class Circle : Shape
{
    /// <summary>
    /// Initialises a new circle with a radius of 1.
    /// </summary>
    public Circle() : this([1])
    {
    }

    /// <summary>
    /// Initialises a new circle using its radius.
    /// </summary>
    /// <param name="sideLengths">
    /// An array containing exactly one value - the radius of the circle.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when exactly one radius is not supplied.
    /// </exception>
    public Circle(int[] sideLengths) : base("circle", sideLengths)
    {
        if (sideLengths.Length != 1)
        {
            throw new ArgumentException("A circle requires exactly one length: its radius.");
        }
    }

    /// <inheritdoc/>
    public override int CalculatePerimeter()
    {
        return (int)(2 * Math.PI * _sides[0]);
    }

    /// <inheritdoc/>
    public override int CalculateArea()
    {
        return (int)(Math.PI * _sides[0] * _sides[0]);
    }
}
