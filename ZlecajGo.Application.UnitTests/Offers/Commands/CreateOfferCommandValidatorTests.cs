using FluentAssertions;
using ZlecajGo.Application.Offers.Commands.CreateOffer;
using ZlecajGo.Application.UnitTests.Helpers.TestData;

namespace ZlecajGo.Application.UnitTests.Offers.Commands;

public class CreateOfferCommandValidatorTests
{
    [Fact]
    public void Validate_TitleTooLong_ReturnsValidationError()
    {
        // Arrange
        var validator = new CreateOfferCommandValidator();
        var command = OfferCommandTestData.CreateValidCreateOfferCommand();
        command.Title = new string('A', 71);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error => error.PropertyName == nameof(CreateOfferCommand.Title));
    }

    [Fact]
    public void Validate_DescriptionTooLong_ReturnsValidationError()
    {
        // Arrange
        var validator = new CreateOfferCommandValidator();
        var command = OfferCommandTestData.CreateValidCreateOfferCommand();
        command.Description = new string('B', 501);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error => error.PropertyName == nameof(CreateOfferCommand.Description));
    }

    [Fact]
    public void Validate_PriceOutOfRange_ReturnsValidationError()
    {
        // Arrange
        var validator = new CreateOfferCommandValidator();
        var command = OfferCommandTestData.CreateValidCreateOfferCommand();
        command.Price = 0m;

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error => error.PropertyName == nameof(CreateOfferCommand.Price));
    }

    [Fact]
    public void Validate_PriceHasTooManyDecimals_ReturnsValidationError()
    {
        // Arrange
        var validator = new CreateOfferCommandValidator();
        var command = OfferCommandTestData.CreateValidCreateOfferCommand();
        command.Price = 10.123m;

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error => error.PropertyName == nameof(CreateOfferCommand.Price));
    }

    [Fact]
    public void Validate_ExpiryDateInPast_ReturnsValidationError()
    {
        // Arrange
        var validator = new CreateOfferCommandValidator();
        var command = OfferCommandTestData.CreateValidCreateOfferCommand();
        command.ExpiryDateTime = DateTime.UtcNow.AddMinutes(-5);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error => error.PropertyName == nameof(CreateOfferCommand.ExpiryDateTime));
    }
}