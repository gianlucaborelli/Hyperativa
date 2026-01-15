using System.Security.Cryptography;
using System.Text;

namespace Hyperativa.Api.Helper
{
    public static class HashHelper
    {
        public static string Hash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Valor inválido para hash.");

            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(value);
            var hashBytes = sha.ComputeHash(bytes);

            return Convert.ToHexString(hashBytes); 
        }
    }
}
