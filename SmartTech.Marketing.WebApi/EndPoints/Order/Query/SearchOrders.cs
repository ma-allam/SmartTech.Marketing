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
    public class SearchOrdersEndPoint : EndpointBaseAsync
    .WithRequest<SearchOrdersEndPointRequest>
    .WithActionResult<SearchOrdersEndPointResponse>
    {
        private readonly ILogger<SearchOrdersEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public SearchOrdersEndPoint(ILogger<SearchOrdersEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(SearchOrdersEndPointRequest.Route)]
        [SwaggerOperation(Summary = "SearchOrders", Description = "SearchOrders ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Order.Query.SearchOrders", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Order" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SearchOrdersEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<SearchOrdersEndPointResponse>> HandleAsync([FromQuery]SearchOrdersEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting SearchOrders handling");
            var Appinput = _mapper.Map<SearchOrdersHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<SearchOrdersEndPointResponse>(result));
        }
    }
}
