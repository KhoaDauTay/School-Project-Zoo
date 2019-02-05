using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Animals;

namespace Zoos
{
    /// <summary>
    /// Contains methods for sorting a list using various algorithms.
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// Uses a bubble sort to sort animals by name.
        /// </summary>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">What function to use to sort the list.</param>
        /// <returns>The sorted result list.</returns>
        public static SortResult BubbleSort(this IList list, Func<object, object, int> comparer)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int swapCounter = 0;
            int compareCounter = 0;

            // use a for loop to loop backward through the list
            // e.g. initialize the loop variable to the length of the list - 1 and decrement the variable instead of increment
            for (int i = list.Count - 1; i > 0; i--)
            {
                // loop forward as long as the loop variable is less than the outer loop variable
                for (int j = 0; j < i; j++)
                {
                    // if the name of the current animal is "greater" than the name of the next animal, swap the two animals
                    compareCounter++;

                    if (comparer(list[j], list[j + 1]) > 0)
                    {
                        // swap the current animal with the next animal
                        list.Swap(j, j + 1);

                        // increment the swap counter
                        swapCounter++;
                    }
                }
            }

            sw.Stop();

            return new SortResult { SwapCount = swapCounter, CompareCount = compareCounter, ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds };
        }

        /// <summary>
        /// Uses a selection sort to sort animals by name.
        /// </summary>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">What function to use to sort the list.</param>
        /// <returns>The sorted result list.</returns>
        public static SortResult SelectionSort(this IList list, Func<object, object, int> comparer)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int swapCounter = 0;
            int compareCounter = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                object minObject = list[i];

                for (int j = i + 1; j < list.Count; j++)
                {
                    compareCounter++;

                    if (comparer(list[j], minObject) < 0)
                    {
                        minObject = list[j];
                    }
                }

                if (comparer(list[i], minObject) != 0)
                {
                    list.Swap(i, list.IndexOf(minObject));
                    swapCounter++;
                }
            }

            sw.Stop();

            return new SortResult { SwapCount = swapCounter, CompareCount = compareCounter, ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds };
        }

        /// <summary>
        /// Uses an insertion sort to sort list by name.
        /// </summary>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">What function to use to sort the list.</param>
        /// <returns>The sorted result list.</returns>
        public static SortResult InsertionSort(this IList list, Func<object, object, int> comparer)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int swapCounter = 0;
            int compareCounter = 0;

            for (int i = 1; i < list.Count; i++)
            {
                compareCounter++;

                for (int j = i; j > 0 && comparer(list[j], list[j - 1]) < 0; j--)
                {
                    list.Swap(j, j - 1);
                    swapCounter++;
                }
            }

            sw.Stop();

            return new SortResult { SwapCount = swapCounter, CompareCount = compareCounter, ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds };
        }

        /// <summary>
        /// Uses a quick sort to sort list by name.
        /// </summary>
        /// <param name="list">The list of list to sort.</param>
        /// <param name="leftIndex">The leftmost part of the list to sort.</param>
        /// <param name="rightIndex">The rightmost part of the list to sort.</param>
        /// <param name="sortResult">The object holding the results of the sort algorithm.</param>
        /// <param name="comparer">The function used to sort the list.</param>
        public static void QuickSort(this IList list, int leftIndex, int rightIndex, SortResult sortResult, Func<object, object, int> comparer)
        {
            // initialize variables to the passed-in indexes
            int leftPointer = leftIndex;
            int rightPointer = rightIndex;

            object pivotObject = list[(leftIndex + rightIndex) / 2];

            while (true)
            {
                while (comparer(list[leftPointer], pivotObject) < 0)
                {
                    leftPointer++;
                    sortResult.CompareCount++;
                }

                while (comparer(pivotObject, list[rightPointer]) < 0)
                {
                    rightPointer--;
                    sortResult.CompareCount++;
                }

                if (leftPointer <= rightPointer)
                {
                    list.Swap(leftPointer, rightPointer);
                    sortResult.SwapCount++;
                    leftPointer++;
                    rightPointer--;
                }

                if (leftPointer > rightPointer)
                {
                    break;
                }
            }

            if (leftIndex < rightPointer)
            {
                list.QuickSort(leftIndex, rightPointer, sortResult, comparer);
            }

            if (leftPointer < rightIndex)
            {
                list.QuickSort(leftPointer, rightIndex, sortResult, comparer);
            }
        }

        /// <summary>
        /// Uses a shell sort to sort list by name.
        /// </summary>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">What function to use to sort the list.</param>
        /// <returns>The sorted result list.</returns>
        public static SortResult ShellSort(this IList list, Func<object, object, int> comparer)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int swapCounter = 0;
            int compareCounter = 0;
            int[] intervals = { 1, 2, 4, 8 };

            for (int i = intervals.Length - 1; i >= 0; i--)
            {
                int interval = intervals[i];

                for (int j = 0; j < interval; j++)
                {
                    for (int k = j + interval; k < list.Count; k += interval)
                    {
                        compareCounter++;

                        for (int m = k; m >= interval && comparer(list[m], list[m - interval]) < 0; m -= interval)
                        {
                            list.Swap(m, m - interval);
                            swapCounter++;
                        }
                    }
                }
            }

            sw.Stop();

            return new SortResult { SwapCount = swapCounter, CompareCount = compareCounter, ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds };
        }

        /// <summary>
        /// Swaps two items in a given list.
        /// </summary>
        /// <param name="list">The list of data.</param>
        /// <param name="index1">The index of the first item to swap.</param>
        /// <param name="index2">The index of the second item to swap.</param>
        private static void Swap(this IList list, int index1, int index2)
        {
            object tempObject = null;

            tempObject = list[index1];
            list[index1] = list[index2];
            list[index2] = tempObject;
        }
    }
}