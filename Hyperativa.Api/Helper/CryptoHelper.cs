using Hyperativa.Api.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Hyperativa.Api.Helper
{
    public static class CryptoHelper
    {
        private static byte[]? _key;
        private static byte[]? _iv;

        public static void Configure(IConfiguration configuration)
        {
            _key = Convert.FromBase64String(
                configuration["CryptoSettings:Key"]!);

            _iv = Convert.FromBase64String(
                configuration["CryptoSettings:Iv"]!);
        }

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using var aes = Aes.Create();
            aes.Key = _key!;
            aes.IV = _iv!;

            var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            var cipherBytes = encryptor.TransformFinalBlock(
                plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(cipherBytes);
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            using var aes = Aes.Create();
            aes.Key = _key!;
            aes.IV = _iv!;

            var decryptor = aes.CreateDecryptor();
            var cipherBytes = Convert.FromBase64String(cipherText);

            var plainBytes = decryptor.TransformFinalBlock(
                cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
