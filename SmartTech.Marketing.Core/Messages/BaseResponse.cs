namespace SmartTech.Marketing.Core.Messages
{
    public abstract class BaseResponse : BaseMessage
    {
        public BaseResponse(Guid correlationId) : base()
        {
            _correlationId = correlationId;
        }

        public BaseResponse()
        {
        }
    }
}
