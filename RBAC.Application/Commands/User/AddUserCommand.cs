using MediatR;
using RBAC.Application.Events;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;

namespace RBAC.Application.Commands.User
{
    public record AddUserCommand(UserEntity user) : IRequest<UserEntity>;


    public class AddUserCommandHandler(IUserRepository userRepository, IPublisher mediator)
        : IRequestHandler<AddUserCommand, UserEntity>
    {
        public async Task<UserEntity> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.AddAsync(request.user);
            await mediator.Publish(new UserCreatedEvent(user.Id));
            return user;
        }
    }
}
