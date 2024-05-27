using ClientAPI.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI.Tests.Domain
{
    public class ClientTests
    {
        [Fact]
        public void CreateClient_ShouldCreateClientWithCorrectProperties()
        {
            // Arrange
            const string name = "John Doe";
            const string gender = "Male";
            const string email = "john@example.com";
            const bool isActive = true;

            // Act
            var client = new Client(name, gender, email, isActive);

            // Assert
            client.Name.Should().Be(name);
            client.Gender.Should().Be(gender);
            client.Email.Should().Be(email);
            client.IsActive.Should().Be(isActive);
        }

        [Fact]
        public void UpdateClient_ShouldUpdateClientProperties()
        {
            // Arrange
            var client = new Client("John Doe", "Male", "john@example.com", true);
            const string newName = "Jane Doe";
            const string newGender = "Female";
            const string newEmail = "jane@example.com";
            const bool newIsActive = false;

            // Act
            client.Update(newName, newGender, newEmail, newIsActive);

            // Assert
            client.Name.Should().Be(newName);
            client.Gender.Should().Be(newGender);
            client.Email.Should().Be(newEmail);
            client.IsActive.Should().Be(newIsActive);
        }
    }
}
