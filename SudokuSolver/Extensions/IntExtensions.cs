namespace SudokuSolver.Extensions
{
    public static class IntExtensions
    {
        public static bool IsInRange(this int value, int min, int max)
        {
            return min <= value && value <= max;
        }
    }
}