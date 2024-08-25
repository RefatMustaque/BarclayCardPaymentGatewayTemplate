using System.Security.Cryptography;
using System.Text;

namespace BarclayPG.Helpers
{
    public class SHAHelper
    {
        public static string ConvertToShaHash(string data)
        {
            SHA1 Hasher = SHA1.Create();
            byte[] NCodedText = Encoding.Default.GetBytes(data);
            byte[] HashedDataBytes = Hasher.ComputeHash(NCodedText);

            StringBuilder HashedDataStringBuilder = new StringBuilder();

            for (int i = 0; i < HashedDataBytes.Length; i++)
            {
                HashedDataStringBuilder.Append(HashedDataBytes[i].ToString("X2"));
            }

            return HashedDataStringBuilder.ToString();
        }
        public static string ConvertToSha256Hash(string rawData)
        {
            // Create a SHA256 instance
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
