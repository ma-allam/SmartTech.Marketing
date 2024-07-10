using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Command
{
    public class UpdateContractEndPoint : EndpointBaseAsync
    .WithRequest<UpdateContractEndPointRequest>
    .WithActionResult<UpdateContractEndPointResponse>
    {
        private readonly ILogger<UpdateContractEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UpdateContractEndPoint(ILogger<UpdateContractEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpPost(UpdateContractEndPointRequest.Route)]
        [SwaggerOperation(Summary = "UpdateContract", Description = "UpdateContract ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Command.UpdateContract", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.ContractManagement" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UpdateContractEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<UpdateContractEndPointResponse>> HandleAsync([FromBody]UpdateContractEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting UpdateContract handling");
            var Appinput = _mapper.Map<UpdateContractHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<UpdateContractEndPointResponse>(result));
        }
    }
}
