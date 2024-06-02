using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class ContractDueDateInput
    {
        public DateOnly DueDate { get; set; }
        public string? Notes { get; set; }
    }

    public class ContractImageModeInput
    {
        public string Name { get; set; }
        public double CreditFactor { get; set; }
    }

    public class ContractImageResolutionInput
    {
        public int ResolutionInCm { get; set; }
        public double MinOrderAreaSize { get; set; }
        public double CreditFactor { get; set; }
        public int ContractImageTypeId { get; set; }
    }

    public class ContractOrderPriorityInput
    {
        public string Name { get; set; }
        public int MaxAllowedDays { get; set; }
        public double CreditFactor { get; set; }
    }

    public class ContractPaymentInformationInput
    {
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? BankAddress { get; set; }
        public string? Iban { get; set; }
        public string? ClientNameInBank { get; set; }
        public string? Notes { get; set; }
    }

    public class ContractPeriodInput
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public double AvailableCredit { get; set; }
        public double RemainingCredit { get; set; }
    }

    public class ContractServiceInput
    {
        public string ServiceName { get; set; }
        public double ServiceCost { get; set; }
        public string? Notes { get; set; }
    }

    public class AttachmentInput
    {
        public IFormFile File { get; set; }
        public string Tags { get; set; }
        public string? Notes { get; set; }
    }
}
