using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hyperativa.Api.Helper
{
    public class EncryptionConverter : ValueConverter<string, string>
    {
        public EncryptionConverter()
            : base(
                v => CryptoHelper.Encrypt(v),   
                v => CryptoHelper.Decrypt(v))   
        {
        }
    }
}
