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
    public class GetAddClientDataEndPoint : EndpointBaseAsync
    .WithRequest<GetAddClientDataEndPointRequest>
    .WithActionResult<GetAddClientDataEndPointResponse>
    {
        private readonly ILogger<GetAddClientDataEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetAddClientDataEndPoint(ILogger<GetAddClientDataEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(GetAddClientDataEndPointRequest.Route)]
        [SwaggerOperation(Summary = "GetAddClientData", Description = "GetAddClientData ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.AddClient.Query.GetAddClientData", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.AddClient.Query" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAddClientDataEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<GetAddClientDataEndPointResponse>> HandleAsync([FromQuery] GetAddClientDataEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting GetAddClientData handling");
            var Appinput = _mapper.Map<GetAddClientDataHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<GetAddClientDataEndPointResponse>(result));
        }
    }
}
