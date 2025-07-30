using ApiGateway.Infrastructure.Contracts.UserContactProvided;
using Application.Infrastructure.Abstractions;
using ConversationAdapter.Contracts.Queue.Events;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Application.Consumers;

public class UserContactProvidedRequestConsumer(IUserQueueMessageSender queueMessageSender) 
    : MediatorRequestHandler<UserContactProvidedRequestDto, Result<UserContactProvidedResponseDto>>
{
    protected override async Task<Result<UserContactProvidedResponseDto>> Handle(
        UserContactProvidedRequestDto request,
        CancellationToken cancellationToken)
    {
        var userContactProvided = new UserContactProvided(
            request.PhoneNumber,
            request.PhoneNumber);

        await queueMessageSender.SendAsync(userContactProvided, cancellationToken);

        return Result.Success(new UserContactProvidedResponseDto());
    }
}