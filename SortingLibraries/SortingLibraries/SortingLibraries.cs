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
        #endregion

        public static List<int> BubbleSort (List<int> input)
        {
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
            const int BASE = 10;
            int maxValue = GetMaxValue(input);

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
    }
}
