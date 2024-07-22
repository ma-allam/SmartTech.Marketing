using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.Clients.Query;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetClientByIdEndPoint : EndpointBaseAsync
    .WithRequest<GetClientByIdEndPointRequest>
    .WithActionResult<GetClientByIdEndPointResponse>
    {
        private readonly ILogger<GetClientByIdEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetClientByIdEndPoint(ILogger<GetClientByIdEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        //[NoCache]
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(GetClientByIdEndPointRequest.Route)]
        [SwaggerOperation(Summary = "GetClientById", Description = "GetClientById ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Client.Query.GetClientById", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.Client" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetClientByIdEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<GetClientByIdEndPointResponse>> HandleAsync([FromQuery]GetClientByIdEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting GetClientById handling");
            var Appinput = _mapper.Map<GetClientByIdHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<GetClientByIdEndPointResponse>(result));
        }
    }
}
