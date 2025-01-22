using MediatR;
using RBAC.Core.Interfaces;

namespace RBAC.Application.Commands.User
{
    public record DeleteUserCommand(Guid UserId) : IRequest<bool>;

    internal class DeleteUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.DeleteAsync(request.UserId);
        }
    }
}
