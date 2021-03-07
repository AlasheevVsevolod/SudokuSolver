using NUnit.Framework;
using SudokuSolver;
using SudokuSolver.Extensions;
using SudokuSolver.Models;

namespace Tests.IntExtensionsTests
{
    public class Tests
    {
        private const int cubeSize = 3;

        [Test]
        public void IsWithinCube_CurrentPosIsOutsideCurrentCube_ShouldReturnFalse()
        {
            var currentPos = new Coordinate(5,5);
            var currentCube = new Cube(new Coordinate(0,0), cubeSize);

            var isWithinCube = currentPos.IsWithinCube(currentCube);
            Assert.IsFalse(isWithinCube);
        }
        
        [Test]
        public void IsWithinCube_CurrentPosIsInsideCurrentCube_ShouldReturnTrue()
        {
            var currentPos = new Coordinate(2,1);
            var currentCube = new Cube(new Coordinate(0,0), cubeSize);

            var isWithinCube = currentPos.IsWithinCube(currentCube);
            Assert.IsTrue(isWithinCube);
        }
    }
}