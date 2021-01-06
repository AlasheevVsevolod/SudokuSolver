using System;
using System.Diagnostics;

namespace SudokuSolver
{
    class Program
    {
        const int fieldSize = 9;
        const int cubeSize = 3;
        static int[,] arr = new int[fieldSize, fieldSize];
        static Tuple<int, int> currentPosition;

        static Tuple<int, int> currentCube => new Tuple<int, int>(currentPosition.Item1 / 3, currentPosition.Item2 / 3);

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

                        //Console.WriteLine();
                    }

                    isValidInput = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nError parsing string, please try again");
                }
            }

            Console.WriteLine();

            var loopsCount = 0;
            var totalTimer = Stopwatch.StartNew();
            while (arr.Contains(0))
            {
                var localTimer = Stopwatch.StartNew();

                for (var i = 1; i <= 9; i++)
                {
                    Console.WriteLine($"Checking {i}...");

                    for (var y = 0; y < fieldSize; y++)
                    {
                        for (var x = 0; x < fieldSize; x++)
                        {
                            currentPosition = new Tuple<int, int>(x, y);
                            if (arr[currentPosition.Item1, currentPosition.Item2] != 0)
                            {
                                continue;
                            }

                            if (CheckIfAlreadyContains(a => a == i))
                            {
                                continue;
                            }

                            if (CheckIfLastCellRemaining())
                            {
                                arr[currentPosition.Item1, currentPosition.Item2] = i;
                            }


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
        private static bool CheckIfAlreadyContains(Func<int, bool> func)
        {
            return CheckIfAlreadyContainsForHorizontal(func) 
                   || CheckIfAlreadyContainsForVertical(func)
                   || CheckIfAlreadyContainsForCube(func);
        }

        private static bool CheckIfAlreadyContainsForHorizontal(Func<int, bool> func)
        {
            for (var i = 0; i < fieldSize; i++)
            {
                if (func.Invoke(arr[i, currentPosition.Item2]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckIfAlreadyContainsForVertical(Func<int, bool> func)
        {
            for (var i = 0; i < fieldSize; i++)
            {
                if (func.Invoke(arr[currentPosition.Item1, i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckIfAlreadyContainsForCube(Func<int, bool> func)
        {
            for (var i = 0; i < cubeSize; i++)
            {
                for (var j = 0; j < cubeSize; j++)
                {
                    if (func.Invoke(arr[currentCube.Item1 + i, currentCube.Item2 + j]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region CheckIfLastCellRemaining
        private static bool CheckIfLastCellRemaining()
        {
            return CheckIfHorizontallyLastCellRemaining()
                   || CheckIfVerticallyLastCellRemaining()
                   || CheckIfLastCellRemainingInCube();
        }

        private static bool CheckIfHorizontallyLastCellRemaining()
        {
            var emptyCount = 0;
            for (var x = 0; x < fieldSize; x++)
            {
                if (arr[x, currentPosition.Item2] == 0)
                {
                    emptyCount++;
                }
            }

            return emptyCount == 1;
        }

        private static bool CheckIfVerticallyLastCellRemaining()
        {
            var emptyCount = 0;
            for (var y = 0; y < fieldSize; y++)
            {
                if (arr[currentPosition.Item1, y] == 0)
                {
                    emptyCount++;
                }
            }

            return emptyCount == 1;
        }

        private static bool CheckIfLastCellRemainingInCube()
        {
            var emptyCount = 0;

            for (var i = 0; i < cubeSize; i++)
            {
                for (var j = 0; j < cubeSize; j++)
                {
                    if (arr[currentCube.Item1 + i, currentCube.Item2 + j] == 0)
                    {
                        emptyCount++;
                    }
                }
            }

            return emptyCount == 1;
        }
        #endregion
    }
}
