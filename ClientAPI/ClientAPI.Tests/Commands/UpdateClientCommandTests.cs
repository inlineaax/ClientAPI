using ClientAPI.Application.Members.Commands;
using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ClientAPI.Tests.Commands
{
    public class UpdateClientCommandTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateClient()
        {
            // Arrange
            var mockMessageProducer = new Mock<IMessageProducer>();
            var handler = new UpdateClientCommand.UpdateClientCommandHandler(mockMessageProducer.Object);
            var request = new UpdateClientCommand
            {
                Id = 1,
                Name = "John Doe",
                Gender = "Male",
                Email = "john@example.com",
                IsActive = true
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(request.Id);
            result.Name.Should().Be(request.Name);
            result.Gender.Should().Be(request.Gender);
            result.Email.Should().Be(request.Email);
            result.IsActive.Should().Be(request.IsActive);
        }

        [Fact]
        public void Handle_NullCommand_ThrowsArgumentNullException()
        {
            // Arrange
            var messageProducerMock = new Mock<IMessageProducer>();
            var handler = new UpdateClientCommand.UpdateClientCommandHandler(messageProducerMock.Object);

            // Act & Assert
            handler.Invoking(async x => await x.Handle(null, CancellationToken.None))
                .Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void Handle_ZeroId_ThrowsValidationException()
        {
            // Arrange
            var messageProducerMock = new Mock<IMessageProducer>();
            var handler = new UpdateClientCommand.UpdateClientCommandHandler(messageProducerMock.Object);
            var command = new UpdateClientCommand { Id = 0, Name = "John Doe" };

            // Act & Assert
            handler.Invoking(async x => await x.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<ValidationException>().WithMessage("*Id*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Handle_EmptyName_ThrowsValidationException(string name)
        {
            // Arrange
            var messageProducerMock = new Mock<IMessageProducer>();
            var handler = new UpdateClientCommand.UpdateClientCommandHandler(messageProducerMock.Object);
            var command = new UpdateClientCommand { Id = 1, Name = name };

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
            var handler = new UpdateClientCommand.UpdateClientCommandHandler(messageProducerMock.Object);
            var command = new UpdateClientCommand { Id = 1, Name = "John Doe", Email = email };

            // Act & Assert
            handler.Invoking(async x => await x.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<ValidationException>().WithMessage("*Email*");
        }
    }
}
