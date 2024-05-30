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
    public class RegisterEndPoint : EndpointBaseAsync
    .WithRequest<RegisterEndPointRequest>
    .WithActionResult<RegisterEndPointResponse>
    {
        private readonly ILogger<RegisterEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public RegisterEndPoint(ILogger<RegisterEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        //[Authorize]
        [ApiVersion("0.0")]
        [HttpPost(RegisterEndPointRequest.Route)]
        [SwaggerOperation(Summary = "Register", Description = "Register ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.User.Register", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.User" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(RegisterEndPointResponse))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<RegisterEndPointResponse>> HandleAsync([FromBody] RegisterEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting Register handling");
            var Appinput = _mapper.Map<RegisterHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<RegisterEndPointResponse>(result));
        }
    }
}
