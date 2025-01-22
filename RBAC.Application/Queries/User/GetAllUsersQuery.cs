using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Queries.User
{
    public record GetAllUsersQuery() : IRequest<ResponseViewModel<UserEntity>>;
    public class GetAllUsersQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetAllUsersQuery, ResponseViewModel<UserEntity>>
    {
        public async Task<ResponseViewModel<UserEntity>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetAllAsync();
        }
    }
}
