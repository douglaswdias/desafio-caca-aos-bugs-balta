using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Extensions;
using Bogus;
using FluentAssertions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    private readonly Faker _faker = new Faker();
    private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();
    
    private const string ExpectedEmailAddress = "test@test.com";
    
    [Fact]
    public void ShouldLowerCaseEmail()
    {
        const string upperCaseEmailAddress = "Test@test.com";
        
        var email = Email.ShouldCreate(upperCaseEmailAddress, _dateTimeProvider.Object);
        
        email.Address.Should().Be(ExpectedEmailAddress);
    }

    [Fact]
    public void ShouldTrimEmail()
    {
        const string whiteSpaceEmailAddress = " Test@test.com ";
        
        var email = Email.ShouldCreate(whiteSpaceEmailAddress, _dateTimeProvider.Object);
        
        email.Address.Should().Be(ExpectedEmailAddress);
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
        Action email = () => Email.ShouldCreate(null, _dateTimeProvider.Object);
        
        email.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        Action email = () => Email.ShouldCreate("", _dateTimeProvider.Object);
        
        email.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void ShouldFailIfEmailIsInvalid()
    {
        var invalidEmailAddress = _faker.Random.Words();
        
        Action email = () => Email.ShouldCreate(invalidEmailAddress, _dateTimeProvider.Object);
        
        email.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void ShouldPassIfEmailIsValid()
    {
        var validEmailAddress = _faker.Internet.Email();
        
        Action email = () => Email.ShouldCreate(validEmailAddress, _dateTimeProvider.Object);

        email.Should().NotThrow();
    }

    [Fact]
    public void ShouldHashEmailAddress()
    {
        var validEmailAddress = _faker.Internet.Email();
        
        var email = Email.ShouldCreate(validEmailAddress, _dateTimeProvider.Object);

        validEmailAddress = validEmailAddress.Trim();
        validEmailAddress = validEmailAddress.ToLower();
        
        var hashEmail = validEmailAddress.ToBase64();
        email.Hash.Should().Be(hashEmail);
    }

    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        var validEmailAddress = _faker.Internet.Email();
        validEmailAddress = validEmailAddress.Trim();
        validEmailAddress = validEmailAddress.ToLower();
        
        var email = Email.FromString(validEmailAddress, _dateTimeProvider.Object);
        
        email.Address.Should().Be(validEmailAddress);
    }

    [Fact]
    public void ShouldExplicitConvertToString()
    {
        var validEmailAddress = _faker.Internet.Email();
        
        var email = Email.FromString(validEmailAddress, _dateTimeProvider.Object);
        var result = (string)email;
        
        email.Address.Should().Be(result);
    }

    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        var validEmailAddress = _faker.Internet.Email();
        
        var email = Email.FromString(validEmailAddress, _dateTimeProvider.Object);
        
        email.Address.Should().Be(email.ToString());
    }
}