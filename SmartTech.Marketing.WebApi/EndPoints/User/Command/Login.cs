using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.User.Command;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class LoginEndPoint : EndpointBaseAsync
    .WithRequest<LoginEndPointRequest>
    .WithActionResult<LoginEndPointResponse>
    {
        private readonly ILogger<LoginEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public LoginEndPoint(ILogger<LoginEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        //[Authorize]
        [ApiVersion("0.0")]
        [HttpPost(LoginEndPointRequest.Route)]
        [SwaggerOperation(Summary = "Login", Description = "Login ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.User.Login", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.User" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LoginEndPointResponse))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<LoginEndPointResponse>> HandleAsync([FromBody] LoginEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting Login handling");
            var Appinput = _mapper.Map<LoginHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<LoginEndPointResponse>(result));
        }
    }
}
