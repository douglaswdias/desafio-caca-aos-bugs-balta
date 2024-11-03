using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Extensions;
using Bogus;
using FluentAssertions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    private readonly Faker _faker = new Faker();
    private const int MinLength = 8;
    private const int MaxLength = 48;
    
    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
        Action password = () => Password.ShouldCreate(null);
        
        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        Action password = () => Password.ShouldCreate("");
        
        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        Action password = () => Password.ShouldCreate(" ");
        
        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {
        var shortPassword = _faker.Internet.Password(_faker.Random.Int(1, 7));
        Action password = () => Password.ShouldCreate(shortPassword);

        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        var largePassword = _faker.Internet.Password(_faker.Random.Int(MaxLength, 64));
        Action password = () => Password.ShouldCreate(largePassword);

        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldHashPassword()
    {
        var validPassword = _faker.Internet.Password(_faker.Random.Int(MinLength, MaxLength));
        
        var password = Password.ShouldCreate(validPassword);
        
        password.Hash.Should().NotBeNull();
    }

    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        var validPassword = _faker.Internet.Password(_faker.Random.Int(MinLength, MaxLength));
        
        var password = Password.ShouldCreate(validPassword);
        var matchPasswordHash = Password.ShouldMatch(password.Hash, validPassword, 32, 10000, '.');
        matchPasswordHash.Should().BeTrue();
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        var password = Password.ShouldGenerate();

        password.Should().NotBeNull();
    }

    [Fact]
    public void ShouldImplicitConvertToString()
    {
        var validPassword = _faker.Internet.Password(_faker.Random.Int(MinLength, MaxLength));
        validPassword.Should().Be(validPassword.ToString());
    }

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        var validPassword = _faker.Internet.Password(_faker.Random.Int(MinLength, MaxLength));
        var password = Password.ShouldCreate(validPassword);
        
        password.Hash.Should().Be(password.ToString());
    }

    [Fact]
    public void ShouldMarkPasswordAsExpired()
    {
        var validPassword = _faker.Internet.Password(_faker.Random.Int(MinLength, MaxLength));
        var password = Password.ShouldCreate(validPassword);
        password.MarkAsExpired();
        
        password.Expired.Should().BeTrue();
    }

    [Fact]
    public void ShouldFailIfPasswordIsExpired() => Assert.Fail();

    [Fact]
    public void ShouldMarkPasswordAsMustChange()
    {
        var validPassword = _faker.Internet.Password(_faker.Random.Int(MinLength, MaxLength));
        var password = Password.ShouldCreate(validPassword);
        password.MarkAsMustChange();
        
        password.MustChange.Should().BeTrue();
    }

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange() => Assert.Fail();
}