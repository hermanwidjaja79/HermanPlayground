using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileHashChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileHashChecker.Tests
{
    [TestClass()]
    public class FileHashCheckerLibTests
    {
        #region HelperFunctions
        // Helper Functions
        //
        protected Dictionary<string, string> PrepareTestMD5Library()
        {
            Dictionary<string, string> lstMD5TestLibrary = new Dictionary<string, string>();
            lstMD5TestLibrary.Add(
                "This is a temporary file. Created by FileHashCheckerLib Tests on " + DateTime.Now.ToShortDateString(),
                "8139C7BA321ED5293F03A02D760D7A4A"
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

                Assert.AreEqual(entry.Value, FileHashCheckerLib.GetHashCodeFromFile(strTempFileName));

                // clear out temp file
                //
                File.Delete(strTempFileName);
            }
        }
    }
}