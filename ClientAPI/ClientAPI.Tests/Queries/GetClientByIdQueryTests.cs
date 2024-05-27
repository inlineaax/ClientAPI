using ClientAPI.Application.Members.Queries;
using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI.Tests.Queries
{
    public class GetClientByIdQueryTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMongoClientRepository> _mockClientRepository;
        private readonly GetClientByIdQuery.GetClientByIdQueryHandler _handler;

        public GetClientByIdQueryTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockClientRepository = new Mock<IMongoClientRepository>();
            _mockUnitOfWork.Setup(uow => uow.MongoClientRepository).Returns(_mockClientRepository.Object);
            _handler = new GetClientByIdQuery.GetClientByIdQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnClient_WhenClientExists()
        {
            // Arrange
            var client = new Client("John Doe", "Male", "john@example.com", true);
            typeof(Entity).GetProperty(nameof(Entity.Id)).SetValue(client, 1);

            _mockClientRepository.Setup(repo => repo.GetClientByIdAsync(1)).ReturnsAsync(client);

            var query = new GetClientByIdQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(client);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenClientDoesNotExist()
        {
            // Arrange
            _mockClientRepository.Setup(repo => repo.GetClientByIdAsync(2)).ReturnsAsync((Client)null);

            var query = new GetClientByIdQuery { Id = 2 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
