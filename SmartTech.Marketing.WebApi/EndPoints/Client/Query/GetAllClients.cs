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
    public class GetAllClientsEndPoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<GetAllClientsEndPointResponse>
    {
        private readonly ILogger<GetAllClientsEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetAllClientsEndPoint(ILogger<GetAllClientsEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(GetAllClientsEndPointRequest.Route)]
        [SwaggerOperation(Summary = "GetAllClients", Description = "GetAllClients ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Client.Query.GetAllClients", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Client" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAllClientsEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<GetAllClientsEndPointResponse>> HandleAsync( CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting GetAllClients handling");
            var result = await _mediator.Send(new GetAllClientsHandlerInput(), cancellationToken);
            return Ok(_mapper.Map<GetAllClientsEndPointResponse>(result));
        }
    }
}
