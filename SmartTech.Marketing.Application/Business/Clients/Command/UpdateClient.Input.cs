using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Command
{
    public class UpdateClientHandlerInput : BaseRequest, IRequest<UpdateClientHandlerOutput>
    {
        public UpdateClientHandlerInput() { }
        public UpdateClientHandlerInput(Guid correlationId) : base(correlationId) { }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// Use 0 to indicate no change
        /// </summary>
        public int SelectedClientType { get; set; } = 0;
        /// <summary>
        /// Use 0 to indicate no change
        /// </summary>
        public int SelectedCountry { get; set; } = 0; 
    }
}
