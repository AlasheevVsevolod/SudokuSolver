using SudokuSolver.Extensions;
using SudokuSolver.Models;

namespace SudokuSolver
{
    public class Cube
    {
        public Cube(Coordinate currentPosition, int cubeSize)
        {
            MinX = currentPosition.X / cubeSize;
            MaxX = MinX + cubeSize - 1;
            MinY = currentPosition.Y / cubeSize;
            MaxY = MinY + cubeSize - 1;
        }

        private int MinX { get; }
        private int MaxX { get; }
        private int MinY { get; }
        private int MaxY { get; }

        public bool ContainsCoordinate(Coordinate coordinate)
        {
            return coordinate.X.IsWithin(MinX, MaxX)
                   && coordinate.Y.IsWithin(MinY, MaxY);
        }
    }
}