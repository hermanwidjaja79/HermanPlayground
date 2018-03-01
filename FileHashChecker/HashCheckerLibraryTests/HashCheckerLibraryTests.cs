using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashCheckerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HashCheckerLibrary.Tests
{
    [TestClass()]
    public class HashCheckerLibraryTests
    {
        #region HelperFunctions
        // Helper Functions
        //
        protected Dictionary<string, string> PrepareTestMD5Library()
        {
            Dictionary<string, string> lstMD5TestLibrary = new Dictionary<string, string>();
            lstMD5TestLibrary.Add(
                "This is a temporary file. Created by FileHashCheckerLib Tests on " + DateTime.Now.ToShortDateString(),
                "F4FF2234716434C417E7AF718EAF8B0D"
                );
            return lstMD5TestLibrary;
        }
        #endregion

        [TestMethod()]
        public void GetHashCodeFromFileTest()
        {
            Dictionary<string, string> dctTestFiles = PrepareTestMD5Library();
            foreach (KeyValuePair<string, string> entry in dctTestFiles)
            {
                // Create temp file
                //
                string strTempFileName = System.IO.Path.GetTempFileName();
                File.AppendAllText(strTempFileName, entry.Key);

                Assert.AreEqual(entry.Value, HashCheckerLibrary.GetHashCodeFromFile(strTempFileName));

                // clear out temp file
                //
                File.Delete(strTempFileName);
            }
        }
    }
}