using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetContractByIdEndPointRequest : BaseRequest
    {
        public const string Route = "/api/ContractManagement/v{version:apiVersion}/GetContractById/";
        [Required]
        public int ContractId { get; set; }
    }
}
