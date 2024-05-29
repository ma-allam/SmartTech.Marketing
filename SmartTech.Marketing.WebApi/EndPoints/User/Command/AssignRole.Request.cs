using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class AssignRoleEndPointRequest : BaseRequest
    {
        public const string Route = "/api/user/v{version:apiVersion}/AssignRole/";

        [Required]
        public string Username { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
