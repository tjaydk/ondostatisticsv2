using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace OndoStatisticsRedis.Services.Security
{
    public class Hashing
    {

        public static string CalculateSHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                // Get the hashed string.  
                string hash = BitConverter.ToString(hashedBytes).Replace("-", "");
                return hash;
            }
        }
    }
}
