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
        : DocumentsApiEndpointGroup<Ulid, GetDocumentResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get document";

        public override int ApiVersion => 1;

        public override IApiEndpointHandler<Ulid, GetDocumentResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<Ulid> requestHandler)
        {
            return builder.MapGet(
                "{id}",
                ([FromRoute] Ulid id, CancellationToken cancellationToken) =>
                    requestHandler(id, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : ApiEndpointHandlerBase<Ulid, GetDocumentRequestDto, GetDocumentResponse, GetDocumentResponseDto>(logger, mediator)
        {
            public override GetDocumentRequestDto MapRequest(Ulid documentId)
            {
                return new GetDocumentRequestDto(documentId);
            }

            public override GetDocumentResponse MapResponse(GetDocumentResponseDto responseDto)
            {
                return new GetDocumentResponse(responseDto.DocumentUrl);
            }
        }
    }
}