using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Command
{
    public class AddNewClientEndPointRequest : BaseRequest
    {
        public const string Route = "/api/client/v{version:apiVersion}/AddNewClient/";
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public int SelectedCountry { get; set; }
        [Required]
        public int SelectedClientType { get; set; }
    }
}
