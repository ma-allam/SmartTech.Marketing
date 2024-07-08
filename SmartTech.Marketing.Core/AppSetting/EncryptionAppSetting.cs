namespace SmartTech.Marketing.Core.AppSetting;

public class EncryptionAppSetting
{
    public static string SectionName { get; set; } = "Encryption";
    public string Salt { get; set; }
    public string InitalizaionVector { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    public bool EnableEncryption { get; set; }
    public bool EnableJwtEncryption { get; set; }
    public bool EnableResponseBodyEncryption { get; set; }
    public bool EnableRequestBodyEncryption { get; set; }
    public bool EnableRequestQueryStringEncryption { get; set; }
}