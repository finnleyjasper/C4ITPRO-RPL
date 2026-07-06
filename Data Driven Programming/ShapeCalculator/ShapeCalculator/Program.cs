namespace ShapeCalculator;
/*
 * File: Program.cs
 * Project: ShapeCalculator
 * Author: Finnley Newnham
 * Date: 6 July 2026
 *
 * Purpose:
 * Allows the use of a shape calculator through a command-line interface.
 *
 * Use dotnet run --project ShapeCalculator/ShapeCalculator/ShapeCalculator.csproj to run.
 */

/// <summary>
/// Provides the command-line entry point for the application and user interface with flavour text.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Starts the Shape Calculator application.
    /// </summary>
    private static void Main()
    {
        ShapeCalculator shapeCalculator = new ShapeCalculator();
        Console.WriteLine("Welcome to the Shape Calculator!");

        string selection = "";
        while (selection != "`")
        {
            if (shapeCalculator.SelectedShape != null)
            {
                Console.WriteLine($"\n ==> Selected shape: {shapeCalculator.SelectedShape.Name} - {string.Join(", ", shapeCalculator.SelectedShape.Sides)}");
            }
            Console.WriteLine("\n >>> Please select an option:");
            Console.WriteLine("1. Add a shape");
            Console.WriteLine("2. List shapes");
            Console.WriteLine("3. Select a shape");
            if (shapeCalculator.SelectedShape != null)
            {
                Console.WriteLine("4. Calculate perimeter");
                Console.WriteLine("5. Calculate area");
            }
            Console.WriteLine("\n --- Or, enter '`' at any time to quit the application.\n\n");

            selection = Console.ReadLine()?.Trim().ToLowerInvariant() ?? ""; // Clean user input and handle nulls

            switch (selection)
            {
                case "1":
                    Console.WriteLine("Please enter the name of the shape to add.\n > Supported shapes are: square, triangle, and rectangle");
                    string newShapeName = Console.ReadLine() ?? "";

                    try
                    {
                        if (shapeCalculator.ShapeNameValid(newShapeName))
                        {
                            Console.WriteLine($"Great! Lets add a {newShapeName} to the calculator. Please enter the side lengths as whole numbers, separated by commas.");
                            Console.WriteLine($"\n! As a reminder, a square requires one side length, a rectangle requires a width and height, and a triangle requires three side lengths.");
                            Console.WriteLine($"Please separate the side lengths with commas, and do not include any letters or symbols.");
                            string? sideLengthsInput = Console.ReadLine()?.Trim();

                            if (!string.IsNullOrWhiteSpace(sideLengthsInput))
                            {
                                // Attempt to split the input based on commas, trim whitespace, and convert to integers
                                int[] sideLengths = sideLengthsInput.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
                                shapeCalculator.AddShape(newShapeName, sideLengths);
                                Console.WriteLine($"The {newShapeName} was added successfully.");
                            }
                            else
                            {
                                Console.WriteLine("No side lengths were entered. Returning to the main menu.");
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input for side lengths. Please enter whole numbers separated by commas.");
                    }
                    catch (ArgumentException exception)
                    {
                        // Shape validation errors are displayed before control returns to the menu.
                        Console.WriteLine($"The shape could not be added: {exception.Message}");
                    }
                    break;
                case "2":
                    shapeCalculator.ListShapes();
                    break;
                case "3":
                    if (shapeCalculator.Shapes.Count == 0)
                        {
                            Console.WriteLine("No shapes have been added yet. Please add a shape first.");
                            break;
                        }
                    shapeCalculator.ListShapes();
                    Console.WriteLine("Please enter the number of the shape to select.");

                    string? shapeSelectionInput = Console.ReadLine();

                    // TryParse checks that the user's input can be converted to an int
                    if (int.TryParse(shapeSelectionInput, out int selectionIndex))
                    {
                        try
                        {
                            shapeCalculator.SelectShape(selectionIndex);
                            Console.WriteLine($"You selected {shapeCalculator.SelectedShape?.Name}.");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("That shape number does not exist. Please choose a number from the list.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a whole number.");
                    }
                    break;
                case "4":
                    if (shapeCalculator.SelectedShape != null)
                    {
                        Console.WriteLine($"The perimeter of the selected shape is: {shapeCalculator.CalculateShapePerimeter()}");
                    }
                    break;
                case "5":
                    if (shapeCalculator.SelectedShape != null)
                    {
                        Console.WriteLine($"The area of the selected shape is: {shapeCalculator.CalculateShapeArea()}");
                    }
                    break;
                case "`":
                    Console.WriteLine("Thank you for using the Shape Calculator!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
