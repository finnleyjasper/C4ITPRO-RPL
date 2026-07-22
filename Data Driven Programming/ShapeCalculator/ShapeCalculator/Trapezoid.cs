namespace ShapeCalculator;
/*
 * File: Trapezoid.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 22 July 2026
 *
 * Purpose:
 * Inherits from the Shape base class to provide the area and perimeter calculations for a trapezoid.
 */

/// <summary>
/// Represents a trapezoid defined by two parallel bases, two other sides and its height.
/// </summary>
public class Trapezoid : Shape
{
    /// <summary>
    /// Initialises a new valid trapezoid with bases of 10 and 4, sides of 5, and a height of 4.
    /// </summary>
    public Trapezoid() : this([10, 4, 5, 5, 4])
    {
    }

    /// <summary>
    /// Initialises a trapezoid using two parallel bases, two other sides and its height.
    /// </summary>
    /// <param name="measurements">
    /// Exactly five values in this order: base 1, base 2, side 1, side 2 and height.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when five measurements are not supplied or they cannot form a valid trapezoid.
    /// </exception>
    public Trapezoid(int[] measurements) : base("trapezoid", measurements)
    {
        if (measurements.Length != 5)
        {
            throw new ArgumentException("A trapezoid requires exactly five measurements: two parallel bases, two sides and a height.");
        }

        int baseDifference = Math.Abs(measurements[0] - measurements[1]);
        int side1 = measurements[2];
        int side2 = measurements[3];
        int height = measurements[4];

        if (height > side1 || height > side2)
        {
            throw new ArgumentException("A trapezoid's height cannot be greater than either of its non-parallel sides.");
        }

        double side1Offset = Math.Sqrt((double)side1 * side1 - (double)height * height);
        double side2Offset = Math.Sqrt((double)side2 * side2 - (double)height * height);
        const double tolerance = 0.000000001;

        bool offsetsPointInSameDirection = Math.Abs(baseDifference - Math.Abs(side1Offset - side2Offset)) <= tolerance;
        bool offsetsPointInOppositeDirections = Math.Abs(baseDifference - (side1Offset + side2Offset)) <= tolerance;

        if (!offsetsPointInSameDirection && !offsetsPointInOppositeDirections)
        {
            throw new ArgumentException("The supplied bases, sides and height do not form a valid trapezoid.");
        }
    }

    /// <inheritdoc/>
    public override int CalculatePerimeter()
    {
        return _sides[0] + _sides[1] + _sides[2] + _sides[3];
    }

    /// <inheritdoc/>
    public override int CalculateArea()
    {
        return (int)((_sides[0] + _sides[1]) * _sides[4] / 2.0);
    }
}
