using ClientAPI.Application.Members.Commands;
using ClientAPI.Domain.Abstractions;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ClientAPI.Tests.Commands
{
    public class CreateClientCommandTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsNewClient()
        {
            // Arrange
            var messageProducerMock = new Mock<IMessageProducer>();
            var handler = new CreateClientCommand.CreateClientCommandHandler(messageProducerMock.Object);
            var command = new CreateClientCommand
            {
                Name = "John Doe",
                Gender = "Male",
                Email = "john@example.com",
                IsActive = true
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(command, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Handle_NullCommand_ThrowsArgumentNullException()
        {
            // Arrange
            var messageProducerMock = new Mock<IMessageProducer>();
            var handler = new CreateClientCommand.CreateClientCommandHandler(messageProducerMock.Object);

            // Act & Assert
            handler.Invoking(async x => await x.Handle(null, CancellationToken.None))
                .Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Handle_EmptyName_ThrowsValidationException(string name)
        {
            // Arrange
            var messageProducerMock = new Mock<IMessageProducer>();
            var handler = new CreateClientCommand.CreateClientCommandHandler(messageProducerMock.Object);
            var command = new CreateClientCommand { Name = name };

            // Act & Assert
            handler.Invoking(async x => await x.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<ValidationException>().WithMessage("*Name*");
        }

        [Theory]
        [InlineData("invalidemail")]
        [InlineData("example@")]
        [InlineData("@example.com")]
        public void Handle_InvalidEmail_ThrowsValidationException(string email)
        {
            // Arrange
            var messageProducerMock = new Mock<IMessageProducer>();
            var handler = new CreateClientCommand.CreateClientCommandHandler(messageProducerMock.Object);
            var command = new CreateClientCommand { Name = "John Doe", Email = email };

            // Act & Assert
            handler.Invoking(async x => await x.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<ValidationException>().WithMessage("*Email*");
        }
    }
}
