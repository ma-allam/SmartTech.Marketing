using System.Security.Cryptography;

namespace SmartTech.Marketing.Core.Crypto
{
    public interface IAesCryptoHelper
    {
        string Key
        {
            get;
            set;
        }
        string TokenKey
        {
            get;
            set;
        }
        CryptoStream EncryptStream(Stream responseStream);
        Stream DecryptStream(Stream cipherStream);
        string DecryptString(string cipherText);
        string EncryptString(string plainText);
        string DecryptToken(string cipherText);
        string EncryptToken(string plainText);
        string DecryptAES(string encryptedText);
    }
}
