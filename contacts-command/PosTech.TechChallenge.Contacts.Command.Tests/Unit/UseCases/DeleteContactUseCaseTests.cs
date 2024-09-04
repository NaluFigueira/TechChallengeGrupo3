using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using PosTech.TechChallenge.Contacts.Command.Application;
using PosTech.TechChallenge.Contacts.Command.Infra;

namespace PosTech.TechChallenge.Contacts.Tests.Unit;

public class DeleteContactUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenContactIsDeletedSuccessfully_ShouldReturnSuccess()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<DeleteContactUseCase>>();
        var contactId = Guid.NewGuid();
        var requestDto = new DeleteContactDTO(contactId);

        var contact = new ContactBuilder().WithId(contactId).Build();
        mockRepository
            .Setup(repo => repo.GetContactByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contact);
        mockRepository
            .Setup(repo => repo.DeleteContactAsync(It.IsAny<Guid>()));

        var useCase = new DeleteContactUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();

        mockRepository.Verify(repo => repo.DeleteContactAsync(contactId), Times.Once);
    }
}
