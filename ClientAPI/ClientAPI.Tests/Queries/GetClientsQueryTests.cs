using ClientAPI.Application.Members.Queries;
using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ClientAPI.Tests.Queries
{
    public class GetClientsQueryTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMongoClientRepository> _mockClientRepository;
        private readonly GetClientsQuery.GetClientsQueryHandler _handler;

        public GetClientsQueryTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockClientRepository = new Mock<IMongoClientRepository>();
            _mockUnitOfWork.Setup(uow => uow.MongoClientRepository).Returns(_mockClientRepository.Object);
            _handler = new GetClientsQuery.GetClientsQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfClients_WhenClientsExist()
        {
            // Arrange
            var clients = new List<Client>
        {
            new Client("John Doe", "Male", "john@example.com", true),
            new Client("Jane Doe", "Female", "jane@example.com", true)
        };

            _mockClientRepository.Setup(repo => repo.GetClientsAsync()).ReturnsAsync(clients);

            var query = new GetClientsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(clients);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoClientsExist()
        {
            // Arrange
            _mockClientRepository.Setup(repo => repo.GetClientsAsync()).ReturnsAsync(new List<Client>());

            var query = new GetClientsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
