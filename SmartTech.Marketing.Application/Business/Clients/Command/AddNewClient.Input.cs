using MediatR;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.Application.Business.Clients.Command
{
    public class AddNewClientHandlerInput : BaseRequest, IRequest<AddNewClientHandlerOutput>
    {
        public AddNewClientHandlerInput() { }
        public AddNewClientHandlerInput(Guid correlationId) : base(correlationId) { }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }

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
