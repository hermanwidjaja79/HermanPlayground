using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingLibraries
{
    public class SortingLibraries
    {
        #region Utilities
        protected static string ListToString(List<int> listSource)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int entry in listSource)
            {
                sb.Append(entry + " ");
            }
            return sb.ToString();
        }
        protected static int GetMaxValue(List<int> listSource)
        {
            if (listSource == null || listSource.Count() == 0)
            {
                return -1;
            }

            int max = listSource[0];
            for (int i=0; i<listSource.Count(); i++)
            {
                if (max < listSource[i])
                {
                    max = listSource[i];
                }
            }
            return max;
        }
        protected static List<int> CopyList(List<int> master)
        {
            List<int> result = new List<int>();
            foreach(int entry in master)
            {
                result.Add(entry);
            }
            return result;
        }
        #endregion

        #region Sorting Algorithms
        public static List<int> BubbleSort (List<int> input)
        {
            if (input is null || input.Count() <= 1)
            {
                return input;
            }

            int lenToMove = input.Count();
            bool swap = false;
            do
            {
                // reset the flag
                //
                swap = false;
                for (int index = 0; index < lenToMove - 1; index++)
                {
                    if (input[index] > input[index + 1])
                    {
                        int iToSwap = input[index];
                        input[index] = input[index + 1];
                        input[index + 1] = iToSwap;
                        swap = true;
                    }
                }

                // the last entry will always be the maximum
                //
                lenToMove--;
            }
            while (swap);

            return input;
        }

        public static List<int> RadixSort (List<int> input)
        {
            if (input is null || input.Count() <= 1)
            {
                return input;
            }

            const int BASE = 10;
            int maxValue = input.Max();

            for (int factor = 1; factor <= maxValue; factor *= BASE)
            {
                // create buckets
                //
                List<List<int>> buckets = new List<List<int>>();
                for (int i=0; i < BASE; i++)
                {
                    buckets.Add(new List<int>());
                }

                // push to buckets
                //
                for (int i=0; i < input.Count(); i++)
                {
                    buckets[(input[i] / factor) % BASE].Add(input[i]);
                }

                // unload buckets
                //
                int destCounter = 0;
                for (int i = 0; i < BASE; i++)
                {
                    // unload each buckets
                    //
                    for (int counter = 0; counter < buckets[i].Count(); counter++)
                    {
                        input[destCounter] = buckets[i][counter];
                        destCounter++;
                    }
                }
            }

            return input;
        }

        public static List<int> MergeSort(List<int> input)
        {
            if (!(input == null || input.Count() <= 1))
            {
                MergeSort(ref input, 0, input.Count() - 1);
            }
            return input;
        }
        protected static void MergeSort(ref List<int> input, int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex < 0 || endIndex - startIndex <= 0)
            {
                return;
            }
            int midIndex = (startIndex + endIndex) / 2;
            if (endIndex - startIndex > 1)
            {
                MergeSort(ref input, startIndex, midIndex);
                MergeSort(ref input, midIndex, endIndex);
            }
            input = CombineLists(input, startIndex, midIndex, endIndex);
        }
        protected static List<int> CombineLists(List<int> input, int startIndex, int midIndex, int endIndex)
        {
            // basic input parameter check
            //
            if (input == null || input.Count() == 0 || startIndex < 0 || midIndex < 0 || endIndex < 0)
            {
                return input;
            }

            // set two running indexes and a copy result
            int i1 = startIndex;
            int i2 = (midIndex == startIndex) ? endIndex : midIndex;
            List<int> output = CopyList(input);

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (i2 > endIndex || (i1 < midIndex && input[i1] < input [i2]))
                {
                    output[i] = input[i1];
                    i1++;
                }
                else
                {
                    output[i] = input[i2];
                    i2++;
                }
            }

            return output;
        }
        #endregion
    }
}
