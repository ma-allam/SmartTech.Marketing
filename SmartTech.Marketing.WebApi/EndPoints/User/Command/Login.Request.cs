using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class LoginEndPointRequest : BaseRequest
    {
        public const string Route = "/api/user/v{version:apiVersion}/Login/";
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
