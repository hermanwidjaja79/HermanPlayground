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
    }
}
