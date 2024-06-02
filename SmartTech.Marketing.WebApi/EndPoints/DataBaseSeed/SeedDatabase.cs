using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.DataBaseSeed;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.DataBaseSeed
{
    public class SeedDatabaseEndPoint : EndpointBaseAsync
    .WithRequest<SeedDatabaseEndPointRequest>
    .WithActionResult<SeedDatabaseEndPointResponse>
    {
        private readonly ILogger<SeedDatabaseEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public SeedDatabaseEndPoint(ILogger<SeedDatabaseEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpPost(SeedDatabaseEndPointRequest.Route)]
        [SwaggerOperation(Summary = "SeedDatabase", Description = "SeedDatabase ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.DataBaseSeed.SeedDatabase", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.DataBaseSeed" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SeedDatabaseEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<SeedDatabaseEndPointResponse>> HandleAsync([FromBody]SeedDatabaseEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting SeedDatabase handling");
            var Appinput = _mapper.Map<SeedDatabaseHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<SeedDatabaseEndPointResponse>(result));
        }
    }
}
