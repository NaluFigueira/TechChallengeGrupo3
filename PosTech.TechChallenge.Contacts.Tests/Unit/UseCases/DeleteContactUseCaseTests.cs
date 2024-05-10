using Moq;

using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Tests;

public class DeleteContactUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOk_WhenContactIsDeletedSuccessfully()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var contactId = Guid.NewGuid();
        var requestDto = new DeleteContactDTO(contactId);

        mockRepository
            .Setup(repo => repo.DeleteContactAsync(It.IsAny<Guid>()));

        var useCase = new DeleteContactUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        mockRepository.Verify(repo => repo.DeleteContactAsync(contactId), Times.Once);
    }
}
