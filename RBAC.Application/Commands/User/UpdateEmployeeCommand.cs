using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;

namespace RBAC.Application.Commands.User
{
    public record UpdateUserCommand(UserEntity User)
        : IRequest<UserEntity>;
    public class UpdateUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<UpdateUserCommand, UserEntity>
    {
        public async Task<UserEntity> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.UpdateAsync(request.User);
        }
    }
}
