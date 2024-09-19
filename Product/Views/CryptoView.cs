using System.Security.Cryptography;
using System.Text;

namespace Product.Views
{
    public class CryptoView
    {
        public static (byte[] hash, byte[] salt) GenerateHash(string passwordString)
        {
            using (var hmac = new HMACSHA512())
            {
                byte[] passwordSalt = hmac.Key;
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
                return (passwordHash, passwordSalt);
            }
        }

        public static bool CompareStringVsHash(string passwordString, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                byte[] newPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
                return newPassword.SequenceEqual(passwordHash);
            }
        }
    }
}
