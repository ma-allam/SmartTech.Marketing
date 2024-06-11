namespace SmartTech.Marketing.Core.Crypto
{
    public interface IRsaCryptoHelper
    {
        string Encrypt(string text);
        string Decrypt(string encrypted);
    }
}
