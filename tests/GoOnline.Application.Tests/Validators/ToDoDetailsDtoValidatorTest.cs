using FluentValidation.TestHelper;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Application.Validators.ToDo;

namespace GoOnline.Application.Tests.Validators;

public class ToDoDetailsDtoValidatorTest
{
    private readonly ToDoDetailsDtoValidator validator = new();

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
    public void Validation_WhenTitleIsNotValid_ShouldReturnValidationError(string title)
    {
        // Arrange
        var dto = validDto();
        dto.Title = title;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    public static IEnumerable<object[]> CompleteMemberData()
    {
        yield return new object[] { string.Empty };
        yield return new object[] { string.Concat(Enumerable.Repeat(".", 101)) };
    }

    private ToDoDetailsDto validDto() => new()
    {
        Id = 1,
        Title = "Title",
        Description = null,
        Complete = 24.5m,
        ExpireDate = new(2024, 12, 01),
    };
}
