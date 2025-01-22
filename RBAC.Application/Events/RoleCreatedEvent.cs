using MediatR;

namespace RBAC.Application.Events
{
    public record RoleCreatedEvent(Guid RoleId) : INotification;
}
