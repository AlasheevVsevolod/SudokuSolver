using SudokuSolver.Models;

namespace SudokuSolver.Extensions
{
    public static class CoordinateExtensions
    {
        public static bool IsWithinCube(this Coordinate currentPos, Cube currentCube)
        {
            return currentPos.X.IsWithin(currentCube.MinX, currentCube.MaxX)
                   && currentPos.Y.IsWithin(currentCube.MinY, currentCube.MaxY);
        }
    }
}