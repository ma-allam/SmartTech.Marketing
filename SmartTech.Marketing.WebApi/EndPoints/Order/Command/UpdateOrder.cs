using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.Order.Command;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Command
{
    public class UpdateOrderEndPoint : EndpointBaseAsync
    .WithRequest<UpdateOrderEndPointRequest>
    .WithActionResult<UpdateOrderEndPointResponse>
    {
        private readonly ILogger<UpdateOrderEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UpdateOrderEndPoint(ILogger<UpdateOrderEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpPost(UpdateOrderEndPointRequest.Route)]
        [SwaggerOperation(Summary = "UpdateOrder", Description = "UpdateOrder ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Order.Command.UpdateOrder", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Order" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UpdateOrderEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<UpdateOrderEndPointResponse>> HandleAsync([FromBody]UpdateOrderEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting UpdateOrder handling");
            var Appinput = _mapper.Map<UpdateOrderHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<UpdateOrderEndPointResponse>(result));
        }
    }
}
