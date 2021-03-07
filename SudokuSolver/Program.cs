using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SudokuSolver.Extensions;
using SudokuSolver.Models;

namespace SudokuSolver
{
    class Program
    {
        const int fieldSize = 9;
        const int maxNumberAmount = 9;
        const int cubeSize = 3;
        static int[,] arr = new int[fieldSize, fieldSize];
        static Coordinate currentPosition;
        static Cube currentCube => new Cube(currentPosition, cubeSize);

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

                    var emptyNumberEntries = arr.Entries(0);
                    foreach (var coordinate in emptyNumberEntries)
                    {
                        currentPosition = coordinate;

                        if (IsAlreadyContains(currentCheckedNumber))
                        {
                            continue;
                        }

                        if (IsLastCellRemaining())
                        {
                            arr[currentPosition.X, currentPosition.Y] = currentCheckedNumber;
                            continue;
                        }
                        
                        if (IsLastCellInCubeRemaining(currentCheckedNumber))
                        {
                            arr[currentPosition.X, currentPosition.Y] = currentCheckedNumber;
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

        private static bool IsAlreadyContains(int currentNumber)
        {
            var entries = arr.Entries(currentNumber);

            //Already contained in horizontal line
            return entries.Any(e => e.Y == currentPosition.Y)
                   //Already contained in vertical line
                   || entries.Any(e => e.X == currentPosition.X)
                   //Already contained in current cube
                   || entries.Any(e => e.IsWithinCube(currentCube));
        }

        private static bool IsLastCellRemaining()
        {
            var entries = arr.Entries(0);

            //Last in horizontal line
            return entries.Count(e => e.Y == currentPosition.Y) == 1
                   //Last in vertical line
                   || entries.Count(e => e.X == currentPosition.X) == 1
                   //Last in current cube
                   || entries.Count(e => e.IsWithinCube(currentCube)) == 1;;
        }

        private static bool IsLastCellInCubeRemaining(int currentNumber)
        {
            var currentNumberEntries = arr.Entries(currentNumber);
            var defaultNumberEntries = arr.Entries(0);

            return false;
        }
    }
}
