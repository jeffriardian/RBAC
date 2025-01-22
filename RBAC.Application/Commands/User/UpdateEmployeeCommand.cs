using MediatR;
using RBAC.Core.DTO.User;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Commands.User
{
    public record UpdateUserCommand(Guid UserId, UpdateUserDto User) : IRequest<ResponseViewModel<UserDto>>;
    public class UpdateUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<UpdateUserCommand, ResponseViewModel<UserDto>>
    {
        public async Task<ResponseViewModel<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.UpdateAsync(request.UserId, request.User);
        }
    }
}
