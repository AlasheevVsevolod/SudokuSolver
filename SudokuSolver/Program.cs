using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SudokuSolver
{
    class Program
    {
        const int fieldSize = 9;
        const int maxNumberAmount = 9;
        const int cubeSize = 3;
        static int[,] arr = new int[fieldSize, fieldSize];
        static (int x, int y) currentPosition;
        static (int x, int y) currentCube => (currentPosition.x / 3, currentPosition.y / 3);

        private static void Main()
        {
            var isValidInput = false;
            while (!isValidInput)
            {
                try
                {
                    for (var i = 0; i < fieldSize; i++)
                    {
                        for (var j = 0; j < fieldSize; j++)
                        {
                            arr[j, i] = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                        }
                    }

                    isValidInput = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nError parsing string, please try again. {e}");
                }
            }

            Console.WriteLine();

            var loopsCount = 0;
            var totalTimer = Stopwatch.StartNew();

            var missedNumbers = new List<int>();
            for (var i = 1; i <= fieldSize; i++)
            {
                missedNumbers.Add(i);
            }

            while (arr.Contains(0))
            {
                var localTimer = Stopwatch.StartNew();

                for (var i = 0; i < missedNumbers.Count; i++)
                {
                    var currentCheckedNumber = missedNumbers[i];
                    if (arr.Count(currentCheckedNumber) == maxNumberAmount)
                    {
                        missedNumbers.Remove(currentCheckedNumber);
                        break;
                    }

                    Console.WriteLine($"Checking {currentCheckedNumber}...");

                    var numberEntries = arr.Entries(0);
                    foreach (var (x, y) in numberEntries)
                    {
                        currentPosition = (x, y);

                        if (IsAlreadyContains(currentCheckedNumber))
                        {
                            continue;
                        }

                        if (IsLastCellRemaining())
                        {
                            arr[currentPosition.x, currentPosition.y] = currentCheckedNumber;
                            continue;
                        }
                    }
                }

                Console.WriteLine($"Loop completed in {localTimer.Elapsed}");
                Console.WriteLine($"Loops amount: {++loopsCount}");
                Console.WriteLine($"Total time passed: {totalTimer.Elapsed}");
                localTimer.Restart();
            }

            Console.WriteLine($"Solved sudoku in {totalTimer.Elapsed}");
            Console.WriteLine("I hope, the video was interesting");
            Console.WriteLine("Thanks for watching. Laters, everybody");
            Console.ReadKey();
        }

        #region CheckIfAlreadyContains
        private static bool IsAlreadyContains(int currentNumber)
        {
            var entries = arr.Entries(currentNumber);
            return IsAlreadyContainsForHorizontal(entries)
                   || IsAlreadyContainsForVertical(entries)
                   || IsAlreadyContainsForCube(entries);
        }

        private static bool IsAlreadyContainsForHorizontal(IEnumerable<(int x, int y)> entries)
        {
            return entries.Any(e => e.y == currentPosition.y);
        }

        private static bool IsAlreadyContainsForVertical(IEnumerable<(int x, int y)> entries)
        {
            return entries.Any(e => e.x == currentPosition.x);
        }

        private static bool IsAlreadyContainsForCube(IEnumerable<(int x, int y)> entries)
        {
            return entries.Any(e => e.IsWithinCube(currentCube, cubeSize));
        }
        #endregion

        #region CheckIfLastCellRemaining
        private static bool IsLastCellRemaining()
        {
            var entries = arr.Entries(0);
            return IsHorizontallyLastCellRemaining(entries)
                   || IsVerticallyLastCellRemaining(entries)
                   || IsLastCellRemainingInCube(entries);
        }

        private static bool IsHorizontallyLastCellRemaining(IEnumerable<(int x, int y)> entries)
        {
            return entries.Count(e => e.y == currentPosition.y) == 1;
        }

        private static bool IsVerticallyLastCellRemaining(IEnumerable<(int x, int y)> entries)
        {
            return entries.Count(e => e.x == currentPosition.x) == 1;
        }

        private static bool IsLastCellRemainingInCube(IEnumerable<(int x, int y)> entries)
        {
            return entries.Count(e => e.IsWithinCube(currentCube, cubeSize)) == 1;
        }
        #endregion
    }
}
