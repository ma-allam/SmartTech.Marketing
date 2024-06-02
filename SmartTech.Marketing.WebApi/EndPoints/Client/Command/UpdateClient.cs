using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.Clients.Command;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Command
{
    public class UpdateClientEndPoint : EndpointBaseAsync
    .WithRequest<UpdateClientEndPointRequest>
    .WithActionResult<UpdateClientEndPointResponse>
    {
        private readonly ILogger<UpdateClientEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UpdateClientEndPoint(ILogger<UpdateClientEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpPost(UpdateClientEndPointRequest.Route)]
        [SwaggerOperation(Summary = "UpdateClient", Description = "UpdateClient ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Client.Command.UpdateClient", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Client.Command" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UpdateClientEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<UpdateClientEndPointResponse>> HandleAsync([FromBody]UpdateClientEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting UpdateClient handling");
            var Appinput = _mapper.Map<UpdateClientHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<UpdateClientEndPointResponse>(result));
        }
    }
}
