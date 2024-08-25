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
    public class GetContractByIdEndPoint : EndpointBaseAsync
    .WithRequest<GetContractByIdEndPointRequest>
    .WithActionResult<GetContractByIdEndPointResponse>
    {
        private readonly ILogger<GetContractByIdEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetContractByIdEndPoint(ILogger<GetContractByIdEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(GetContractByIdEndPointRequest.Route)]
        [SwaggerOperation(Summary = "GetContractById", Description = "GetContractById ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query.GetContractById", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.ContractManagement" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetContractByIdEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<GetContractByIdEndPointResponse>> HandleAsync([FromQuery]GetContractByIdEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting GetContractById handling");
            var Appinput = _mapper.Map<GetContractByIdHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<GetContractByIdEndPointResponse>(result));
        }
    }
}
