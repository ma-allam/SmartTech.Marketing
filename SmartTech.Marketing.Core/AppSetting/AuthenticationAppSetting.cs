using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.AppSetting
{
    public class AuthenticationAppSetting
    {
        public static string SectionName { get; set; } = "Authentication";
        //public Cognito Cognito { get; set; }
        public Jwt Jwt { get; set; }
        //public AzureAuthentication Azure { get; set; }
        public string AuthenticationAllowed { get; set; }
    }

    //public class Cognito
    //{
    //    public static string SectionName { get; set; } = "Cognito";
    //    public string AppClientId { get; set; }
    //    public string AppClientSecret { get; set; }
    //    public bool IncludeErrorDetails { get; set; }
    //    public string MetadataAddress { get; set; }
    //    public bool RequireHttpsMetadata { get; set; }
    //    public bool SaveToken { get; set; }
    //    public string UserPool { get; set; }
    //    public bool ValidateIssuer { get; set; }
    //    public string UserPoolID { get; set; }
    //    public bool Enable { get; set; }

    //}

    public class Jwt
    {
        public static string SectionName { get; set; } = "Jwt";
        public string secretKey { get; set; }
        public int expiryMinutes { get; set; }
        public int RefreshExpiryMinutes { get; set; }
        public string issuer { get; set; }

        public bool Enable { get; set; }
    }

    //public class AzureAuthentication
    //{
    //    public static string SectionName { get; set; } = "Azure";
    //    public string Instance { get; set; }
    //    public Guid ClientId { get; set; }
    //    public Guid TenantId { get; set; }
    //    public bool Enable { get; set; }
    //}
}
