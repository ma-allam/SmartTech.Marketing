using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetAllContractsHandlerOutput : BaseResponse
    {
        public GetAllContractsHandlerOutput() { }
        public GetAllContractsHandlerOutput(Guid correlationId) : base(correlationId) { }
        public List<ContractDto> Contracts { get; set; }
        public string Message { get; set; }
    }
    public class ContractDto
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public double TotalContractCost { get; set; }
        public double TotalCredit { get; set; }
        public int CurrencyId { get; set; }
        public string? Notes { get; set; }
        public int ClientId { get; set; }
        public int ContractPaymentTypeId { get; set; }
        public bool Enabled { get; set; }
        public double AcceptableCloudPerc { get; set; }
        public double MinSquareArea { get; set; }
        public List<ContractDueDateDto> ContractDueDates { get; set; }
        public List<ContractImageModeDto> ContractImageModes { get; set; }
        public List<ContractImageResolutionDto> ContractImageResolutions { get; set; }
        public List<ContractOrderPriorityDto> ContractOrderPriorities { get; set; }
        public List<ContractPaymentInformationDto> ContractPaymentInformation { get; set; }
        public List<ContractPeriodDto> ContractPeriods { get; set; }
        public List<ContractServiceDto> ContractServices { get; set; }
        public List<ContractAttachmentDto> ContractAttachments { get; set; }
    }

    public class ContractDueDateDto
    {
        public int Id { get; set; }
        public DateOnly DueDate { get; set; }
        public string? Notes { get; set; }
    }

    public class ContractImageModeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CreditFactor { get; set; }
    }

    public class ContractImageResolutionDto
    {
        public int Id { get; set; }
        public int ResolutionInCm { get; set; }
        public double MinOrderAreaSize { get; set; }
        public double CreditFactor { get; set; }
        public int ContractImageTypeId { get; set; }
    }

    public class ContractOrderPriorityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxAllowedDays { get; set; }
        public double CreditFactor { get; set; }
    }

    public class ContractPaymentInformationDto
    {
        public int Id { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? BankAddress { get; set; }
        public string? Iban { get; set; }
        public string? ClientNameInBank { get; set; }
        public string? Notes { get; set; }
    }

    public class ContractPeriodDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public double AvailableCredit { get; set; }
        public double RemainingCredit { get; set; }
    }

    public class ContractServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public double ServiceCost { get; set; }
        public string? Notes { get; set; }
    }

    public class ContractAttachmentDto
    {
        public int Id { get; set; }
        public string Tags { get; set; }
        public string Name { get; set; }
        public Guid AttachmentId { get; set; }
        public string FileExtension { get; set; }
        public string? Notes { get; set; }
        public string FileUrl { get; set; }
        public DateOnly UploadDate { get; set; }
    }
}
