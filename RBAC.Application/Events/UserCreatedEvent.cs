using MediatR;

namespace RBAC.Application.Events
{
    public record UserCreatedEvent(Guid UserId) : INotification;
}
