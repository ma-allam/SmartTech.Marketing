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
    public class AddNewOrderEndPoint : EndpointBaseAsync
    .WithRequest<AddNewOrderEndPointRequest>
    .WithActionResult<AddNewOrderEndPointResponse>
    {
        private readonly ILogger<AddNewOrderEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AddNewOrderEndPoint(ILogger<AddNewOrderEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpPost(AddNewOrderEndPointRequest.Route)]
        [SwaggerOperation(Summary = "AddNewOrder", Description = "AddNewOrder ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Order.Command.AddNewOrder", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Order" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AddNewOrderEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<AddNewOrderEndPointResponse>> HandleAsync([FromBody] AddNewOrderEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting AddNewOrder handling");
            var Appinput = _mapper.Map<AddNewOrderHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<AddNewOrderEndPointResponse>(result));
        }
    }
}
