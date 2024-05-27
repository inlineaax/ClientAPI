using ClientAPI.Application.Members.Commands;
using ClientAPI.Application.Members.Commands.Validations;
using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using ClientAPI.Infrastructure.RabbitMQ.Exceptions;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ClientAPI.Tests.Commands
{
    public class DeleteClientCommandTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldDeleteClient()
        {
            // Arrange
            var mockMessageProducer = new Mock<IMessageProducer>();
            var handler = new DeleteClientCommand.DeleteClientCommandHandler(mockMessageProducer.Object);
            var request = new DeleteClientCommand
            {
                Id = 1
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(request.Id);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldThrowClientNotFoundException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.MongoClientRepository.GetClientByIdAsync(It.IsAny<int>())).ReturnsAsync((Client)null);

            var command = new DeleteClientCommand { Id = 1 };
            var validator = new DeleteClientCommandValidator(unitOfWorkMock.Object);

            // Act
            Func<Task> act = async () => await validator.ValidateAndThrowAsync(command);

            // Assert
            await Assert.ThrowsAsync<ClientNotFoundException>(act);
        }
    }
}
