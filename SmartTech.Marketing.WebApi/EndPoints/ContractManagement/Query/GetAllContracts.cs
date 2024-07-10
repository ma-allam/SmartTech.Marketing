using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.ContractManagement.Query;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetAllContractsEndPoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<GetAllContractsEndPointResponse>
    {
        private readonly ILogger<GetAllContractsEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetAllContractsEndPoint(ILogger<GetAllContractsEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(GetAllContractsEndPointRequest.Route)]
        [SwaggerOperation(Summary = "GetAllContracts", Description = "GetAllContracts ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query.GetAllContracts", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.ContractManagement" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAllContractsEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<GetAllContractsEndPointResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting GetAllContracts handling");
            var result = await _mediator.Send(new GetAllContractsHandlerInput(), cancellationToken);
            return Ok(_mapper.Map<GetAllContractsEndPointResponse>(result));
        }
    }
}
