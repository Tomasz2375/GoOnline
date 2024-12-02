using FluentValidation.TestHelper;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Application.Validators.ToDo;

namespace GoOnline.Application.Tests.Validators;

public class ToDoCompleteDtoValidatorTest
{
    private readonly ToDoCompleteDtoValidator validator = new();

    [Fact]
    public void Validation_WhenDtoIsValid_ShouldNotReturnValidationError()
    {
        // Arrange
        var dto = validDto();

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [MemberData(nameof(CompleteMemberData))]
    public void Validation_WhenCompleteIsNotValid_ShouldReturnValidationError(decimal complete)
    {
        // Arrange
        var dto = validDto();
        dto.Complete = complete;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Complete);
    }

    public static IEnumerable<object[]> CompleteMemberData()
    {
        yield return new object[] { -11.8m };
        yield return new object[] { 100.01 };
    }

    private ToDoCompleteDto validDto() => new()
    {
        Id = 1,
        Complete = 24.5m,
    };
}
