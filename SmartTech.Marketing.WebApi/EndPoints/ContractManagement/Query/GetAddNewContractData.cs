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
    public class GetAddNewContractDataEndPoint : EndpointBaseAsync
    .WithRequest<GetAddNewContractDataEndPointRequest>
    .WithActionResult<GetAddNewContractDataEndPointResponse>
    {
        private readonly ILogger<GetAddNewContractDataEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetAddNewContractDataEndPoint(ILogger<GetAddNewContractDataEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(GetAddNewContractDataEndPointRequest.Route)]
        [SwaggerOperation(Summary = "GetAddNewContractData", Description = "GetAddNewContractData ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query.GetAddNewContractData", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.ContractManagement" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAddNewContractDataEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<GetAddNewContractDataEndPointResponse>> HandleAsync([FromQuery]GetAddNewContractDataEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting GetAddNewContractData handling");
            var Appinput = _mapper.Map<GetAddNewContractDataHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<GetAddNewContractDataEndPointResponse>(result));
        }
    }
}
