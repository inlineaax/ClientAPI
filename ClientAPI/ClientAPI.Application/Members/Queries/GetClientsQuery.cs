using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using MediatR;

namespace ClientAPI.Application.Members.Queries
{
    public class GetClientsQuery : IRequest<IEnumerable<Client>>
    {
        public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<Client>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetClientsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<Client>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
            {
                var clients = await _unitOfWork.MongoClientRepository.GetClientsAsync();
                return clients;
            }
        }
    }
}
