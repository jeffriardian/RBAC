using MediatR;
using RBAC.Application.Events;
using RBAC.Core.DTO.User;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Commands.User
{
    public record LoginUserCommand(LoginUserDto user) : IRequest<ResponseViewModel<UserLoginDto>>;


    public class LoginUserCommandHandler(IUserRepository userRepository, IPublisher mediator)
        : IRequestHandler<LoginUserCommand, ResponseViewModel<UserLoginDto>>
    {
        public async Task<ResponseViewModel<UserLoginDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.LoginAsync(request.user);
            await mediator.Publish(new UserCreatedEvent(user.Data.Select(p => p.Id).FirstOrDefault()));
            return user;
        }
    }
}
