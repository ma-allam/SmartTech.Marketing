using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Auth.User;
using SmartTech.Marketing.Core.Exceptions;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenHandlerInput, RefreshTokenHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<RefreshTokenHandler> _logger;
        private readonly IJwtHandler _jwtHandler;

        public RefreshTokenHandler(ILogger<RefreshTokenHandler> logger, IDataBaseService databaseService, IJwtHandler jwtHandler)
        {
            _logger = logger;
            _databaseService = databaseService;
            _jwtHandler = jwtHandler;
        }
        public async Task<RefreshTokenHandlerOutput> Handle(RefreshTokenHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RefreshToken business logic");
            RefreshTokenHandlerOutput output = new RefreshTokenHandlerOutput(request.CorrelationId());
            if (string.IsNullOrEmpty(request.Token))
            {
                throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Token is Empty");
            }
            var ValidatedToekn = _jwtHandler.ValidateJwtToken(request.Token);

            if (ValidatedToekn == null)
            {
                throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Unvalid token");
            }
            string data = ValidatedToekn.Claims.First(x => x.Type == JWTClaims.ActiveContext).Value ?? string.Empty;
            if (string.IsNullOrEmpty(data))
            {
                throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Error while Refeshing");
            }

            ActiveContext activeContext = JsonConvert.DeserializeObject<ActiveContext>(data, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            var token = _jwtHandler.CreateWithRefreshToken(activeContext);

            output.Context = token;
            return output;
        }
    }
}
