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
            // standard list - no duplicate
            //
            dctTestCases.Add(
                CreateList(6, 9, 3, 8, 1, 7),
                CreateList(1, 3, 6, 7, 8, 9));
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
        public void BubbleSortTest()
        {
            Dictionary<List<int>, List<int>> dctTestCases = CreateTestCases();

            string filepath = "C:\\Users\\herman\\Desktop\\doh\\herman.log";
            System.IO.File.Delete(filepath);
            foreach (KeyValuePair<List<int>, List<int>> entry in dctTestCases)
            {
                List<int> result = SortingLibraries.BubbleSort(entry.Key);
                Assert.AreEqual(
                    ListToString(result), 
                    ListToString(entry.Value)
                    );
            }
        }
    }
}