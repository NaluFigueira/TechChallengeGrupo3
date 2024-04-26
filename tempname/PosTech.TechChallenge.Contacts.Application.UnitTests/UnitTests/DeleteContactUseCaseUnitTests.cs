using Moq;

using PosTech.TechChallenge.Contacts.Application.Repositories;
using PosTech.TechChallenge.Contacts.Application.UseCases.DeleteContactByIdUseCase;

namespace PosTech.TechChallenge.Contacts.Application.UnitTests;

public class DeleteContactByIdUseCaseUnitTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOk_WhenContactIsDeletedSuccessfully()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var contactId = Guid.NewGuid();
        var requestDto = new DeleteContactRequestDto(Id: contactId);

        mockRepository
            .Setup(repo => repo.DeleteContactAsync(It.IsAny<Guid>()));

        var useCase = new DeleteContactByIdUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        mockRepository.Verify(repo => repo.DeleteContactAsync(contactId), Times.Once);
    }
}
