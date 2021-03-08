using SudokuSolver.Extensions;
using SudokuSolver.Models;

namespace SudokuSolver
{
    public class Cube
    {
        public Cube(Coordinate currentPosition, int cubeSize)
        {
            MinX = currentPosition.X / cubeSize * cubeSize;
            MaxX = MinX + cubeSize - 1;
            MinY = currentPosition.Y / cubeSize * cubeSize;
            MaxY = MinY + cubeSize - 1;
        }

        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        public bool ContainsCoordinate(Coordinate coordinate)
        {
            return coordinate.X.IsInRange(MinX, MaxX)
                   && coordinate.Y.IsInRange(MinY, MaxY);
        }
    }
}