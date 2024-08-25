using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.Clients.Query;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class SearchClientsEndPoint : EndpointBaseAsync
    .WithRequest<SearchClientsEndPointRequest>
    .WithActionResult<SearchClientsEndPointResponse>
    {
        private readonly ILogger<SearchClientsEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public SearchClientsEndPoint(ILogger<SearchClientsEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(SearchClientsEndPointRequest.Route)]
        [SwaggerOperation(Summary = "SearchClients", Description = "SearchClients ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Client.Query.SearchClients", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Client" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SearchClientsEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<SearchClientsEndPointResponse>> HandleAsync([FromQuery]SearchClientsEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting SearchClients handling");
            var Appinput = _mapper.Map<SearchClientsHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<SearchClientsEndPointResponse>(result));
        }
    }
}
