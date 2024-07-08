using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;
using SmartTech.Marketing.Core.AppSetting;

namespace SmartTech.Marketing.Core.Crypto
{
    public class RsaCryptoHelper : IRsaCryptoHelper
    {

        private readonly RSACryptoServiceProvider _serverPrivateKey;
        private readonly RSACryptoServiceProvider _serverPublicKey;

        public RsaCryptoHelper(EncryptionAppSetting _encryptionOption)
        {
            _serverPrivateKey = GetPrivateKeyFromString(_encryptionOption.PrivateKey);
            _serverPublicKey = GetPublicKeyFromString(_encryptionOption.PublicKey);
        }
        private RSACryptoServiceProvider GetPublicKeyFromString(string keyValue)
        {
            using TextReader publicKeyTextReader = new StringReader(keyValue);
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParam);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);
            return csp;
        }
        private RSACryptoServiceProvider GetPrivateKeyFromString(string keyValue)
        {
            using TextReader privateKeyTextReader = new StringReader(keyValue);
            AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);
            return csp;
        }


        public string Encrypt(string text)
        {
            var encryptedBytes = _serverPublicKey.Encrypt(Encoding.UTF8.GetBytes(text), false);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encrypted)
        {
            var decryptedBytes = _serverPrivateKey.Decrypt(Convert.FromBase64String(encrypted), false);
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }
    }
}
