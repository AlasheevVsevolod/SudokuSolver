namespace SudokuSolver
{
    public static class IntExtensions
    {
        public static bool IsWithin(this int value, int min, int max)
        {
            return min <= value && value <= max;
        }

        public static bool IsWithinCube(this (int x, int y) currentPos, (int x, int y) currentCube, int cubeSize)
        {
            return currentPos.x.IsWithin(currentCube.x * cubeSize, currentCube.x * (cubeSize + 1) - 1) && 
                   currentPos.y.IsWithin(currentCube.y * cubeSize, currentCube.y * (cubeSize + 1) - 1);
        }
    }
}