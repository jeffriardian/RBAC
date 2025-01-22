using MediatR;

namespace RBAC.Application.Events
{
    public record PermissionCreatedEvent(Guid PermissionId) : INotification;
}
