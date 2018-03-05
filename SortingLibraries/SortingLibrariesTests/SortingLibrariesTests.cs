using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingLibraries.Tests
{
    [TestClass()]
    public class SortingLibrariesTests
    {
        #region HelperFunctions
        protected List<int> CreateList(params int[] list)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < list.Length; i++)
            {
                result.Add(list[i]);
            }
            return result;
        }
        protected Dictionary<List<int>, List<int>> CreateTestCases()
        {
            Dictionary<List<int>, List<int>> dctTestCases = new Dictionary<List<int>, List<int>>();
            // standard list - no duplicate (odd count)
            //
            dctTestCases.Add(
                CreateList(6, 9, 3),
                CreateList(3, 6, 9));
            // standard list - no duplicate (even count)
            //
            dctTestCases.Add(
                CreateList(6, 9, 3, 8, 1, 7),
                CreateList(1, 3, 6, 7, 8, 9));
            // standard list - no duplicate
            //
            dctTestCases.Add(
                CreateList(6, 9, 3, 12, 101, 78, 8, 1, 7),
                CreateList(1, 3, 6, 7, 8, 9, 12, 78, 101));
            // standard list - duplicate
            //
            dctTestCases.Add(
                CreateList(6, 3, 9, 3, 8, 1, 7, 3),
                CreateList(1, 3, 3, 3, 6, 7, 8, 9));
            // one entry list
            //
            dctTestCases.Add(
                CreateList(5),
                CreateList(5)
                );
            // two entry list
            //
            dctTestCases.Add(
                CreateList(7, 5),
                CreateList(5, 7)
                );
            // one entry list - duplicate
            //
            dctTestCases.Add(
                CreateList(5, 5, 5),
                CreateList(5, 5, 5)
                );
            // empty list
            //
            dctTestCases.Add(
                CreateList(),
                CreateList()
                );

            return dctTestCases;
        }
        public string ListToString(List<int> listSource)
        {
            StringBuilder sb = new StringBuilder();
            foreach(int entry in listSource)
            {
                sb.Append(entry + " ");
            }
            return sb.ToString();
        }
        #endregion

        [TestMethod()]
        public void QuickSortTest()
        {
            Dictionary<List<int>, List<int>> dctTestCases = CreateTestCases();

            foreach (KeyValuePair<List<int>, List<int>> entry in dctTestCases)
            {
                List<int> result = SortingLibraries.QuickSort(entry.Key);
                Assert.AreEqual(
                    ListToString(entry.Value),
                    ListToString(result)
                    );
            }
        }

        [TestMethod()]
        public void BubbleSortTest()
        {
            Dictionary<List<int>, List<int>> dctTestCases = CreateTestCases();

            foreach (KeyValuePair<List<int>, List<int>> entry in dctTestCases)
            {
                List<int> result = SortingLibraries.BubbleSort(entry.Key);
                Assert.AreEqual(
                    ListToString(entry.Value),
                    ListToString(result)
                    );
            }
        }

        [TestMethod()]
        public void RadixSortTest()
        {
            Dictionary<List<int>, List<int>> dctTestCases = CreateTestCases();

            foreach (KeyValuePair<List<int>, List<int>> entry in dctTestCases)
            {
                List<int> result = SortingLibraries.RadixSort(entry.Key);
                Assert.AreEqual(
                    ListToString(entry.Value),
                    ListToString(result)
                    );
            }
        }

        [TestMethod()]
        public void MergeSortTest()
        {
            Dictionary<List<int>, List<int>> dctTestCases = CreateTestCases();

            foreach (KeyValuePair<List<int>, List<int>> entry in dctTestCases)
            {
                List<int> result = SortingLibraries.MergeSort(entry.Key);
                Assert.AreEqual(
                    ListToString(entry.Value),
                    ListToString(result)
                    );
            }
        }
    }
}