using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Command
{
    public class UpdateClientEndPointRequest : BaseRequest
    {
        public const string Route = "/api/client/v{version:apiVersion}/UpdateClient/";

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
