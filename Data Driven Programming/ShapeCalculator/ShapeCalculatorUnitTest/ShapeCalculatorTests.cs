using ShapeCalculator;
/*
 * File: ShapeCalculatorTests.cs
 * Project: ShapeCalculatorTests
 * Author: Finnley Newnham
 * Date: 7 July 2026
 *
 * Purpose:
 * Apply unit tests to the ShapeCalculator Shapes
 *
 * Run with dotnet test ShapeCalculator/ShapeCalculatorUnitTest/ShapeCalculatorUnitTest.csproj
 */
namespace ShapeCalculatorUnitTest;

    [TestClass]
    public sealed class ShapeCalculatorTests
    {
        // Testing the framework  --------------------------------
        [TestMethod]
        public void TestMethod1()
        {
            int expectedOutput = 60;
            int actualOutput = 60;

            Assert.AreEqual( expectedOutput, actualOutput);
        }

        // Perimeter unit functional tests -----------------------
        [TestMethod]
        public void TestSquarePerimeter()
        {
            int expectedOutput = 20;
            Square s = new Square([5]);
            int actualOutput = s.CalculatePerimeter();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestRectanglePerimeter()
        {
            int expectedOutput = 16;
            Rectangle r = new Rectangle([5, 3]);
            int actualOutput = r.CalculatePerimeter();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestTrianglePerimeter()
        {
            int expectedOutput = 30;
            Triangle t = new Triangle([10, 10, 10]);
            int actualOutput = t.CalculatePerimeter();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        // Area unit functional tests -----------------------
        [TestMethod]
        public void TestSquareArea()
        {
            int expectedOutput = 25;
            Square s = new Square([5]);
            int actualOutput = s.CalculateArea();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestRectangleArea()
        {
            int expectedOutput = 15;
            Rectangle r = new Rectangle([5, 3]);
            int actualOutput = r.CalculateArea();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestTriangleArea()
        {
            int expectedOutput = 43;
            Triangle t = new Triangle([10, 10, 10]);
            int actualOutput = t.CalculateArea();
            Assert.AreEqual(expectedOutput, actualOutput);
        }


        // Equivalance partition tests -----------------------

        [TestMethod]
        public void TestSquareInputsEqPartition()
        {
            try
            {
                _ = new Square([5, 5]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a square is given more than one side length.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestSquareNegLengthEqPartition()
        {
            try
            {
                _ = new Square([-5]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a square is given a negative side length.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestRectangleInputEqPartition()
        {
            try
            {
                _ = new Rectangle([5, 5, 5]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a rectangle is given more than two side lengths.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestRectangleNegLengthEqPartition()
        {
            try
            {
                _ = new Rectangle([-5, 3]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a rectangle is given a negative side length.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestTriangleInputEqPartition()
        {
            try
            {
                _ = new Triangle([10, 10, 10, 10]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a triangle is given more than three side lengths.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestTriangleNegLengthEqPartition()
        {
            try
            {
                _ = new Triangle([-5, 5, 5]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a triangle is given a negative side length.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestTriangleValuesEqPartition() // extra one for triangles as they must be mathmatically possible
        {
            try
            {
                _ = new Triangle([1, 2, 3]); // <-- impossible triangle
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a triangle is given invalid side lengths.");
            }
            catch (ArgumentException)
            {
            }
        }

        // Boundary tests -------------------------------------
        // 0 Used as a boundary test for all shapes
        public void TestSquareBoundary()
        {
            try
            {
                _ = new Square([0]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a square is given a 0 length.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestRectangleBoundary()
        {
            try
            {
                _ = new Rectangle([0, 0]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a rectangle is given a 0 length.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestTriangleSidesBoundary()
        {
            try
            {
                _ = new Triangle([0, 0, 0]);
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a triangle is given a 0 length.");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestTriangleBoundary()
        {
            try
            {
                _ = new Triangle([1, 2, 3]); // <-- impossible triangle
                // Assert.Fail() only runs if the exception is NOT thrown, otherwise control move to catch block
                Assert.Fail("Expected an ArgumentException when a triangle is given invalid side lengths.");
            }
            catch (ArgumentException)
            {
            }
        }

        // Stress tests -------------------------------------
        [TestMethod]
        public void TestSquareStressTest()
        {
            int expectedOutput = 100000000;
            Square s = new Square([10000]);
            int actualOutput = s.CalculateArea();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestRectangleStressTest()
        {
            int expectedOutput = 200000000;
            Rectangle r = new Rectangle([20000, 10000]);
            int actualOutput = r.CalculateArea();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestTriangleStressTest()
        {
            int expectedOutput = 96824583;
            Triangle t = new Triangle([20000, 20000, 10000]);
            int actualOutput = t.CalculateArea();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

    }
