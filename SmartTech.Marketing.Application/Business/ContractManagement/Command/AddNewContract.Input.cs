using MediatR;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Command
{
    public class AddNewContractHandlerInput : BaseRequest, IRequest<AddNewContractHandlerOutput>
    {
        public AddNewContractHandlerInput() { }
        public AddNewContractHandlerInput(Guid correlationId) : base(correlationId) { }
        [Required]
        public string ContractNumber { get; set; } = null!;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public double TotalContractCost { get; set; }

        [Required]
        public int TotalCredit { get; set; }

        [Required]
        public int SelectedCurrencyId { get; set; }

        public string? Notes { get; set; }

        [Required]
        public int SelectedClientId { get; set; }

        [Required]
        public int SelectedPaymentTypeId { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}
