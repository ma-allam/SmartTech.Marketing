using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Command
{
    public class UpdateContractEndPointRequest : BaseRequest
    {
        public const string Route = "/api/ContractManagement/v{version:apiVersion}/UpdateContract/";
        [Required]
        public int ContractId { get; set; }

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

        public double AcceptableCloudPerc { get; set; }
        public double MinSquareArea { get; set; }
        public List<ContractDueDateInput> ContractDueDates { get; set; }
        public List<ContractImageModeInput> ContractImageModes { get; set; }
        public List<ContractImageResolutionInput> ContractImageResolutions { get; set; }
        public List<ContractOrderPriorityInput> ContractOrderPriorities { get; set; }
        public List<ContractPaymentInformationInput> ContractPaymentInformation { get; set; }
        public List<ContractPeriodInput> ContractPeriods { get; set; }
        public List<ContractServiceInput> ContractServices { get; set; }
        public List<AttachmentInput> Attachments { get; set; }
    }
}
