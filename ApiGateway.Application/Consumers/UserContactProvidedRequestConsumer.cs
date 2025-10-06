using ApiGateway.Infrastructure.Contracts.UserContactProvided;
using Application.Infrastructure.Abstractions;
using ConversationAdapter.Contracts.Queue.Events;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Application.Consumers;

public class UserContactProvidedRequestConsumer(IUserEventSender userEventSender) 
    : MediatorRequestHandler<UserContactProvidedRequestDto, Result<UserContactProvidedResponseDto>>
{
    protected override async Task<Result<UserContactProvidedResponseDto>> Handle(
        UserContactProvidedRequestDto request,
        CancellationToken cancellationToken)
    {
        var userContactProvided = new UserContactProvided(
            request.UserTelegramId,
            request.PhoneNumber);

        await userEventSender.SendAsync(userContactProvided, cancellationToken);

        return Result.Success(new UserContactProvidedResponseDto());
    }
}