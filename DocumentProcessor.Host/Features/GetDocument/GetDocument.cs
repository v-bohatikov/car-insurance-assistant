using DocumentProcessor.Contracts.Api;
using DocumentProcessor.Infrastructure.Contracts.GetDocument;
using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace DocumentProcessor.Host.Features.GetDocument;

public class GetDocument
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : DocumentsEndpointGroup<GetDocumentRequest, GetDocumentResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get document";

        public override int ApiVersion => 1;

        public override IEndpointHandler<GetDocumentRequest, GetDocumentResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<GetDocumentRequest> requestHandler)
        {
            return builder.MapGet(
                "get",
                ([FromBody] GetDocumentRequest request, CancellationToken cancellationToken) => requestHandler(request, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : EndpointHandlerBase<GetDocumentRequest, GetDocumentRequestDto, GetDocumentResponse, GetDocumentResponseDto>(logger, mediator)
        {
            public override GetDocumentRequestDto MapRequest(GetDocumentRequest request)
            {
                return new GetDocumentRequestDto(
                    request.DocumentType,
                    request.FileId);
            }

            public override GetDocumentResponse MapResponse(GetDocumentResponseDto responseDto)
            {
                return new GetDocumentResponse(responseDto.DocumentUrl);
            }
        }
    }
}