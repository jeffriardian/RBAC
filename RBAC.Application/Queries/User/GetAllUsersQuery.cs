using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;

namespace RBAC.Application.Queries.User
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserEntity>>;
    public class GetAllUsersQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetAllUsersQuery, IEnumerable<UserEntity>>
    {
        public async Task<IEnumerable<UserEntity>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetAllAsync();
        }
    }
}
