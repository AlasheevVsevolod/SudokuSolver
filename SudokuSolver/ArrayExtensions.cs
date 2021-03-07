﻿using System.Collections.Generic;

namespace SudokuSolver
{
    public static class ArrayExtensions
    {
        public static bool Contains<T>(this T[,] array, T value)
        {
            var xLength = array.GetLength(0);
            var yLength = array.GetLength(1);

            for (var i = 0; i < xLength; i++)
            {
                for (var j = 0; j < yLength; j++)
                {
                    if (array[i, j].Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static int Count<T>(this T[,] array, T value)
        {
            var xLength = array.GetLength(0);
            var yLength = array.GetLength(1);
            var counter = 0;

            for (var i = 0; i < xLength; i++)
            {
                for (var j = 0; j < yLength; j++)
                {
                    if (array[i, j].Equals(value))
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        public static List<(int, int)> Entries<T>(this T[,] array, T value)
        {
            var xLength = array.GetLength(0);
            var yLength = array.GetLength(1);
            var entries = new List<(int, int)>();

            for (var i = 0; i < xLength; i++)
            {
                for (var j = 0; j < yLength; j++)
                {
                    if (array[i, j].Equals(value))
                    {
                        entries.Add((i, j));
                    }
                }
            }

            return entries;
        }
    }
}
