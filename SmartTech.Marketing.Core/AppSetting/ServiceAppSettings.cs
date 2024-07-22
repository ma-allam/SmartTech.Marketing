namespace SmartTech.Marketing.Core.AppSetting;

public class ServiceAppSettings
{
    public static string SectionName { get; set; } = "ServiceSettings";
    public bool EnableSwagger { get; set; }
    public string BaseServicePath { get; set; }
}