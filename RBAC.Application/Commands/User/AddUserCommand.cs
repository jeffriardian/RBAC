using MediatR;
using RBAC.Application.Events;
using RBAC.Core.DTO.User;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Commands.User
{
    public record AddUserCommand(CreateUserDto user) : IRequest<ResponseViewModel<UserDto>>;


    public class AddUserCommandHandler(IUserRepository userRepository, IPublisher mediator)
        : IRequestHandler<AddUserCommand, ResponseViewModel<UserDto>>
    {
        public async Task<ResponseViewModel<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.AddAsync(request.user);
            await mediator.Publish(new UserCreatedEvent(user.Data.Select(p => p.Id).FirstOrDefault()));
            return user;
        }
    }
}
