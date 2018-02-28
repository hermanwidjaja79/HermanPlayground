using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace HashCheckerLibrary
{
    public class HashCheckerLibrary
    {
        public static string GetHashCodeFromFile(string strFilename)
        {
            string strHashPassword = String.Empty;
            if (strFilename is null || !File.Exists(strFilename))
            {
                throw new IOException("GetHashCodeFromFile() - Invalid \"strFilename\" parameter");
            }

            using (var stream = File.OpenRead(strFilename))
            {
                return strHashPassword = GetHashCodeFromFile(stream);
            }
        }

        public static string GetHashCodeFromFile(Stream stream)
        {
            if (stream is null)
            {
                throw new IOException("GetHashCodeFromFile() - Invalid \"stream\" parameter");
            }

            MD5 md5Instance = MD5.Create();
            byte[] hash = md5Instance.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "");
        }

    }
}
