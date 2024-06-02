using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.User.Command;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class AssignRoleEndPoint : EndpointBaseAsync
    .WithRequest<AssignRoleEndPointRequest>
    .WithActionResult<AssignRoleEndPointResponse>
    {
        private readonly ILogger<AssignRoleEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AssignRoleEndPoint(ILogger<AssignRoleEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpPost(AssignRoleEndPointRequest.Route)]
        [SwaggerOperation(Summary = "AssignRole", Description = "AssignRole ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.User.Command.AssignRole", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.User.Command" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AssignRoleEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<AssignRoleEndPointResponse>> HandleAsync([FromBody]AssignRoleEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting AssignRole handling");
            var Appinput = _mapper.Map<AssignRoleHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<AssignRoleEndPointResponse>(result));
        }
    }
}
