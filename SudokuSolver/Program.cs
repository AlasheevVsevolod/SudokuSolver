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
                    foreach (var currentCoordinate in emptyNumberEntries)
                    {
                        var currentCube = new Cube(currentCoordinate, cubeSize);

                        if (IsAlreadyContains(currentCoordinate, currentCube, currentCheckedNumber))
                        {
                            continue;
                        }

                        if (IsLastCellRemaining(currentCoordinate, currentCube))
                        {
                            SetNewValue(currentCoordinate, currentCheckedNumber);
                            continue;
                        }

                        if (IsLastCellInCubeRemaining(currentCoordinate, currentCube, currentCheckedNumber))
                        {
                            SetNewValue(currentCoordinate, currentCheckedNumber);
                            continue;
                        }

                        var qwe = false;
                        if (qwe)
                        {
                            SetNewValue(new Coordinate(0,0), arr[0,0]);
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

        private static bool IsAlreadyContains(Coordinate currentCoordinate, Cube currentCube, int currentNumber)
        {
            var entries = arr.Entries(currentNumber);

            //Already contained in horizontal line
            return entries.Any(e => e.Y == currentCoordinate.Y)
                   //Already contained in vertical line
                   || entries.Any(e => e.X == currentCoordinate.X)
                   //Already contained in current cube
                   || entries.Any(currentCube.ContainsCoordinate);
        }

        private static bool IsLastCellRemaining(Coordinate currentCoordinate, Cube currentCube)
        {
            var entries = arr.Entries(0);

            //Last in horizontal line
            return entries.Count(e => e.Y == currentCoordinate.Y) == 1
                   //Last in vertical line
                   || entries.Count(e => e.X == currentCoordinate.X) == 1
                   //Last in current cube
                   || entries.Count(currentCube.ContainsCoordinate) == 1;
        }

        private static bool IsLastCellInCubeRemaining(Coordinate currentCoordinate, Cube currentCube, int currentNumber)
        {
            var defaultNumberEntries = arr.Entries(0);
            var checkedNumberEntries = arr.Entries(currentNumber);

            //Last possible in cube horizontal line
            if (defaultNumberEntries.Count(e => e.Y == currentCoordinate.Y && e.X.IsInRange(currentCube.MinX, currentCube.MaxX)) == 1 
                && checkedNumberEntries.Count(e => e.Y != currentCoordinate.Y && e.Y.IsInRange(currentCube.MinY, currentCube.MaxY)) == cubeSize - 1) 
                return true;

            //Last possible in cube vertical line
            if (defaultNumberEntries.Count(e => e.X == currentCoordinate.X && e.Y.IsInRange(currentCube.MinY, currentCube.MaxY)) == 1 
                && checkedNumberEntries.Count(e => e.X != currentCoordinate.X && e.X.IsInRange(currentCube.MinX, currentCube.MaxX)) == cubeSize - 1) 
                return true;

            return false;
        }

        private static void SetNewValue(Coordinate currentCoordinate, int currentNumber)
        {
            arr[currentCoordinate.X, currentCoordinate.Y] = currentNumber;
            for (var i = 0; i < fieldSize; i++)
            {
                for (var j = 0; j < fieldSize; j++)
                {
                    if (j == currentCoordinate.X && i == currentCoordinate.Y)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.Write($"{arr[j, i]} ");
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine("- - - - - - - - -");
        }
    }
}
