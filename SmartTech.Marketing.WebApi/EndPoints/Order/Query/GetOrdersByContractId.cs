using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.Order.Query;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class GetOrdersByContractIdEndPoint : EndpointBaseAsync
    .WithRequest<GetOrdersByContractIdEndPointRequest>
    .WithActionResult<GetOrdersByContractIdEndPointResponse>
    {
        private readonly ILogger<GetOrdersByContractIdEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetOrdersByContractIdEndPoint(ILogger<GetOrdersByContractIdEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(GetOrdersByContractIdEndPointRequest.Route)]
        [SwaggerOperation(Summary = "GetOrdersByContractId", Description = "GetOrdersByContractId ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Order.Query.GetOrdersByContractId", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Order" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetOrdersByContractIdEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<GetOrdersByContractIdEndPointResponse>> HandleAsync([FromQuery]GetOrdersByContractIdEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting GetOrdersByContractId handling");
            var Appinput = _mapper.Map<GetOrdersByContractIdHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<GetOrdersByContractIdEndPointResponse>(result));
        }
    }
}
