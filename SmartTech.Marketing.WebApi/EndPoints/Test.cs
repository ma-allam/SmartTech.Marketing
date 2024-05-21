using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints
{
    public class TestEndPoint : EndpointBaseAsync
    .WithRequest<TestEndPointRequest>
    .WithActionResult<TestEndPointResponse>
    {
        private readonly ILogger<TestEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public TestEndPoint(ILogger<TestEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        [Authorize]
        [ApiVersion("0.0")]
        [HttpGet(TestEndPointRequest.Route)]
        [SwaggerOperation(Summary = "Test", Description = "Test ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.Test", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TestEndPointResponse))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<TestEndPointResponse>> HandleAsync([FromQuery]TestEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting Test handling");
            var Appinput = _mapper.Map<TestHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<TestEndPointResponse>(result));
        }
    }
}
