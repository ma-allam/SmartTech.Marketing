using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Command
{
    public class AddNewContractEndPointRequest : BaseRequest
    {
        public const string Route = "/api/ContractManagement/v{version:apiVersion}/AddNewContract/";
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

        public List<AttachmentData> Attachments { get; set; }
    }
}
