using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;

namespace RBAC.Application.Queries.User
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<UserEntity>;

    public class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        public async Task<UserEntity> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetByIdAsync(request.UserId);
        }
    }
}
