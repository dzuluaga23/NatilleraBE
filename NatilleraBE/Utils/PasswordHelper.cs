using System.Security.Cryptography;
using System.Text;

namespace NatilleraBE.Utils
{
    public class PasswordHelper
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            var saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            string saltedPassword = salt + password;
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                string hash = Convert.ToBase64String(hashBytes);
                return (hash, salt);
            }
        }
    }
}
