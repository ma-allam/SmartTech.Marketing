using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SmartTech.Marketing.Core.Exceptions
{
    public class WebApiException : Exception
    {
        public WebApiExceptionSource ExceptionSource { get; }

        public WebApiException() : this("Internal Server Error")
        {
            ExceptionSource = WebApiExceptionSource.GeneralException;
        }

        public WebApiException(string message, params object[] args) : this(WebApiExceptionSource.FromTranslation, message, args)
        {
            ExceptionSource = WebApiExceptionSource.FromTranslation;
        }
        public WebApiException(WebApiExceptionSource webApiExceptionSource, string message, params object[] args) : this(null, webApiExceptionSource, message, args)
        {
            ExceptionSource = webApiExceptionSource;
        }

        public WebApiException(Exception? innerException, WebApiExceptionSource webApiExceptionSource, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            ExceptionSource = webApiExceptionSource;

        }

        public override string ToString()
        {
            // ErrorMessageType=FromTranslation  it must be translated from Message Field by its code
            // ErrorMessageType=DynamicMessage  it is a dynamic message show as it is from Message Field
            // ErrorMessageType=GeneralException it is generalException Message must not show to the end user so show "Contact the administrator"
            // ErrorMessageType=FromDataBase it databse error must show as it is from Details and if Details is empty show the message


            var error = new ExceptionOutput
            {
                ErrorMessageType = ExceptionSource,
                Message = Message,
                Details = InnerException?.Message ?? "",
                ExtraDetails = InnerException?.InnerException?.Message ?? ""
            };
            return JsonConvert.SerializeObject(error);
        }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WebApiExceptionSource
    {
        FromTranslation,
        DynamicMessage,
        FromDataBase,
        GeneralException
    }
}
