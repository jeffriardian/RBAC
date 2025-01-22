using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Queries.User
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<ResponseViewModel<UserEntity>>;

    public class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, ResponseViewModel<UserEntity>>
    {
        public async Task<ResponseViewModel<UserEntity>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetByIdAsync(request.UserId);
        }
    }
}
