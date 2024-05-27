using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using MediatR;

namespace ClientAPI.Application.Members.Queries
{
    public class GetClientByIdQuery : IRequest<Client>
    {
        public int Id { get; set; }

        public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetClientByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.MongoClientRepository.GetClientByIdAsync(request.Id);
            }
        }
    }
}
