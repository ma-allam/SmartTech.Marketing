using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Marketing.Application.Business.UploadDownloadAttach;
using SmartTech.Marketing.Core.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach
{
    public class UploadAttachmentEndPoint : EndpointBaseAsync
    .WithRequest<UploadAttachmentEndPointRequest>
    .WithActionResult<UploadAttachmentEndPointResponse>
    {
        private readonly ILogger<UploadAttachmentEndPoint> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UploadAttachmentEndPoint(ILogger<UploadAttachmentEndPoint> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;

        }
        //[Authorize]
        [ApiVersion("0.0")]
        [HttpPost(UploadAttachmentEndPointRequest.Route)]
        [SwaggerOperation(Summary = "UploadAttachment", Description = "UploadAttachment ", OperationId = "SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach.UploadAttachment", Tags = new[] { "SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UploadAttachmentEndPointResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ExceptionOutput))]
        public override async Task<ActionResult<UploadAttachmentEndPointResponse>> HandleAsync([FromForm]UploadAttachmentEndPointRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Information : Starting UploadAttachment handling");
            var Appinput = _mapper.Map<UploadAttachmentHandlerInput>(request);
            var result = await _mediator.Send(Appinput, cancellationToken);

            return Ok(_mapper.Map<UploadAttachmentEndPointResponse>(result));
        }
    }
}
