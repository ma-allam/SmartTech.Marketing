using MediatR;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetContractByIdHandlerInput : BaseRequest, IRequest<GetContractByIdHandlerOutput>
    {
        public GetContractByIdHandlerInput() { }
        public GetContractByIdHandlerInput(Guid correlationId) : base(correlationId) { }
        [Required]
        public int ContractId { get; set; }
    }
}
